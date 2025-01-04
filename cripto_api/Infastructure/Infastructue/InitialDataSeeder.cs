using Application;
using Infastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Infrastructure
{
    public class InitialDataSeeder
    {
        private readonly IServiceProvider serviceProvider;

        public InitialDataSeeder(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task SeedAsync()
        {
            //System.InvalidOperationException: 'Cannot resolve scoped service 'Infrastructure.ApplicationDbContext' from root provider.'
            /**
             * 
             *ApplicationDbContext gibi scoped (kapsamlı) bir servisi root (kök) IServiceProvider üzerinden çözmeye çalıştığınızda oluşur. 
             *ASP.NET Core'da scoped servisler doğrudan root sağlayıcıdan çözümlenemez;
             *bunun yerine bir scope (kapsam) oluşturmanız ve scoped servisleri bu kapsam içinde çözmeniz gerekir. 
             * 
            **/
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                await context.Database.EnsureCreatedAsync();
                try
                {
                    // Veritabanını sıfırlayın ve yeniden oluşturun
                    if (ApplicationManager.removeDatabaseOnProgramRestart) await context.Database.EnsureDeletedAsync();
                    await context.Database.EnsureCreatedAsync();
                }
                catch (Exception) { }


                // İsteğe bağlı: Başlangıç verilerini ekleyin
                // context.SeedData();
                // await context.SaveChangesAsync();
            }
        }
    }
}
