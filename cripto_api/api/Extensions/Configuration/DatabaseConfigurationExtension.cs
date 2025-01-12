using Application;
using Infastructure;
using Microsoft.EntityFrameworkCore;

namespace api.Extensions.Configuration;

public static class DatabaseConfigurationExtension
{
    public static void configureDbContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            if (ApplicationManager.isDeveloping)
            {
                string pth = AppContext.BaseDirectory;
                string dbPath = Path.Combine(pth, "../../../Crypto.db");
                options.UseSqlite($"Data Source={dbPath}");
            }
            else
            {
                string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            }
        });


        if (ApplicationManager.enableSensitiveDataLogging)
        {
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.EnableSensitiveDataLogging());  // For debugging only
        }



    }
}
