using api.Jobs; // TickerJob sınıfını ekledik
using Application;
using Application.Mapping;
using Application.Settings;
using Hangfire;
using Infastructure.EventBus;
using Infastructure;
using Infrastructure.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Events;
using Infastructure.Persistance.Repositories;
using Application.Repositories;
using Microsoft.AspNetCore.Identity;
using Infastructure.Infastructue.Services;
using api.Hubs;
using Application.Services.InternalServices;
using Domain.Domains.IdentityDomain.JwtService;
using Application.Services.ExternalServices;
using Application.Services.InternalServices.CryptoHttpRequestService;
using Newtonsoft.Json;
using api.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Domain.Domains.IdentityDomain.Entities;
using Domain.Domains.ChyriptoDomain.CryptoTechnicalAnalyses;
using Shared.LogCat;
using api.Middlewares;
using Application.Services.InternalServices.MobilePushNotificationService;
using Application.Mapping.abstractions;
using Application.Events;
using Application.Events.EventHandlers;
using Application.Services.InternalServices.EmailService;


var builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<FirebasePushNotificationConfig>(
    builder.Configuration.GetSection("FirebasePushNotificationConfig")
);


// JSON dosyasını oku
string jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "firebasePushNotificationConfig.json");
string jsonString = File.ReadAllText(jsonFilePath);

var serviceAccountKey = JsonConvert.DeserializeObject<FirebasePushNotificationConfig>(jsonString);

if (serviceAccountKey != null)
{
    // (1) Somut config nesnesini IOptionsWrapper şeklinde sar
    var optionsWrapper = new OptionsWrapper<FirebasePushNotificationConfig>(serviceAccountKey);
    // (2) DI'ye IOptions<FirebasePushNotificationConfig> olarak ekle
    builder.Services.AddSingleton<IOptions<FirebasePushNotificationConfig>>(optionsWrapper);
}



/*
 Kestrel Ayarları: API'nizin Kestrel sunucusunun tüm ağ arayüzlerinden gelen bağlantıları dinlemesini sağlayın. appsettings.json dosyanızda veya Program.cs dosyanızda şu şekilde yapılandırabilirsiniz:
 */

if (ApplicationManager.isDeveloping)
{
    builder.WebHost.ConfigureKestrel(options =>
    {
        options.ListenAnyIP(5000); // HTTP için 5000 portunu dinle
        options.ListenAnyIP(5001, listenOptions => // HTTPS için 5001 portunu dinle
        {
            listenOptions.UseHttps();
        });
    });

}



// Add services to the container.
// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.configureSwagger();
// Database Context
builder.configureDbContext();

// Authorization Configuration
builder.Services.AddAuthorization(options => { options.AddPolicy("AdminOnly", policy => policy.RequireClaim("isAdmin", "true")); });

// CORS Configuration
builder.configureCorsSettings();
builder.Services.AddScoped<ICryptoTechnicalAnalyser, CryptoTechnicalAnalyses>();
builder.Services.AddScoped<ICriptoRepository, CriptoRepository>();
builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<ICriptoService, CriptoService>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(CriptoMappingProfile));
builder.Services.AddScoped<ICryptoMapper, CryptoMapper>();
builder.Services.AddScoped<IMapperFacade, MapperFacade>();

// MediatR
builder.Services.AddMediatR(typeof(ApplicationManager).Assembly);

// API Settings Configuration
builder.Services.Configure<CryptoApiConfig>(builder.Configuration.GetSection("CryptoApiSettings"));
builder.Services.AddScoped<ICryptoDataService, CryptoDataService>();

// HttpClientAddCors
builder.Services.AddHttpClient("serkan", config =>
{
    config.Timeout = TimeSpan.FromSeconds(20);
});


// 1) NotificationService'inizi event handler olarak kullanacağınızı varsayarak
//    öncelikle bu sınıfı da DI konteyner'a ekleyin.
//    (Hayati bir state tutmuyorsa Transient veya Scoped olabilir)
//builder.Services.AddTransient<NotificationService>(sp =>
//{
//    // Burada sp, IServiceProvider'ı ifade eder.
//    var emailSender = sp.GetRequiredService<IEmailService>();
//    return new NotificationService(emailSender);
//});
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<NotificationService>();
builder.Services.AddTransient<ICryptoDataService, CryptoDataService>();

// 2) IEventDispatcher'ı singleton olarak kaydedin, ancak Subscribe işlemlerini
//    tip bazında yapın (instance bazında değil).
builder.Services.AddSingleton<IEventDispatcher>(sp =>
{
    var dispatcher = new InMemoryEventBus(sp);

    // Aşağıdaki örnek; NotificationService'in, TickerFetchFailedEvent
    // ve TickersFetchedEvent için handler olduğunu varsayar.
    //dispatcher.Subscribe<TickerFetchFailedEvent, NotificationService>();
    //dispatcher.Subscribe<TickersFetchedEvent, NotificationService>();

    dispatcher.Subscribe<TickersFetchedEvent, TickersFetchedEventHandler>();
    dispatcher.Subscribe<TickerFetchFailedEvent, ApplicationEventHandlerImp>();
    dispatcher.Subscribe<UserMobileNotificationTokenUpdatedEvent, ApplicationEventHandlerImp>();


    return dispatcher;
});

// CryptoService
builder.Services.AddScoped<ICryptoHttpRequestService, CryptoHttpRequestService>();

// TickerJob
builder.Services.AddTransient<FetchCriptosPeriodiclyJob>();

// Initial Data Seeder
builder.Services.AddSingleton<InitialDataSeeder>();

// Hangfire'ı yapılandırın
builder.configureHangfireSettings();
builder.configureEmailSettings();
builder.configureJwtSettings();



if (ApplicationManager.startHangfireHob)
{ builder.Services.AddHangfireServer(options => { options.WorkerCount = 3; }); }


// Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();



// Auth Services
builder.Services.AddScoped<IIdentityService, IdentityService>();

// Identity
builder.Services.AddIdentity<UserIdentity, RoleIdentity>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddSignalR();

builder.Services.AddScoped<IMobilePushNotificationService, AndroidPushNotificationService>();
builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);

var app = builder.Build();

// Hangfire Dashboard
app.UseHangfireDashboard();

// Middleware’i ekle
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Zamanlanmış işi ekleyin (örnek: her saat başı)
if (ApplicationManager.startHangfireHob)
{
    string cronExpression = ApplicationManager.isDeveloping
    ? Cron.Minutely()
    : Cron.Hourly();

    RecurringJob.AddOrUpdate<FetchCriptosPeriodiclyJob>(
        "ticker-job",
        job => job.ExecuteAsync(),
       cronExpression
        );
}



app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Crypto API v1");
    options.RoutePrefix = string.Empty; // Swagger UI'ı kök dizinde çalıştırır (isteğe bağlı)
});



if (!ApplicationManager.isDeveloping) app.UseHttpsRedirection();
LogCat.Configure(ApplicationManager.isDeveloping);


app.UseCors("AllowAllOrigins");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHub<CriptoAnalysesHub>("/hub");

// Data Seeding
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<InitialDataSeeder>();
    await seeder.SeedAsync();
}


// deneme

Console.WriteLine("app started");
app.Run();



// Desktop -> http://192.168.1.196:5000
// Laptop -> http://192.168.1.125:5000/index.html