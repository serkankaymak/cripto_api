using System.Reflection;

namespace api.Extensions.Configuration;
public static class SwaggerConfigurationExtension
{
    public static void configureSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "Crypto API",
                Version = "v1",
                Description = "Kripto para sorguları için API"
            });

            // XML yorumlama desteği
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        });
    }
}

