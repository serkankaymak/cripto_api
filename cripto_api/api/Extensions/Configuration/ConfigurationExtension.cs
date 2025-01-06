using Application.Services.InternalServices.EmailService;
using Application.Settings;
using Domain.Domains.IdentityDomain.JwtService;
using Hangfire;
using Hangfire.SQLite;

namespace api.Extensions.Configuration
{
    public static class ConfigurationExtension
    {
        public static void configureEmailSettings(this WebApplicationBuilder builder)
        {
            // Email Settings Configuration
            builder.Services.Configure<EmailConfig>(builder.Configuration.GetSection("EmailSenderSetings"));
            var emailConfig = builder.Configuration.GetSection("EmailSenderSetings").Get<EmailConfig>();
            if (emailConfig != null)
                builder.Services.AddSingleton(emailConfig);
            builder.Services.AddScoped<EmailService, EmailService>();
        }
        public static void configureJwtSettings(this WebApplicationBuilder builder)
        {
            // JWT Settings Configuration
            builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtSettings"));
            var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtConfig>();
            if (jwtSettings != null)
                builder.Services.AddSingleton(jwtSettings);
        }
        public static void configureHangfireSettings(this WebApplicationBuilder builder)
        {
            // Hangfire'ı yapılandırın
            builder.Services.AddHangfire(config =>
            {
                var connectionString = builder.Configuration.GetConnectionString("HangfireConnection");
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                      .UseSimpleAssemblyNameTypeSerializer()
                      .UseRecommendedSerializerSettings()
                      .UseSQLiteStorage(connectionString, new SQLiteStorageOptions
                      {
                          QueuePollInterval = TimeSpan.FromSeconds(20),
                          JobExpirationCheckInterval = TimeSpan.FromHours(1),
                          CountersAggregateInterval = TimeSpan.FromMinutes(5),
                          SchemaName = "dbo",
                      });
            });

        }
        public static void configureCorsSettings(this WebApplicationBuilder builder)
        {
            // CORS Configuration
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", policy =>
                {
                    policy
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials()
                          .SetIsOriginAllowed((host) => true);

                });
            });

        }



    }
}
