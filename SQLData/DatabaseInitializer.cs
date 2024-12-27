using KuaforYonetim1.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Linq;
using KuaforYonetim1.SQLData;

namespace KuaforYonetim1.Data
{
    public static class DatabaseInitializer
    {
        public static async Task Seed(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            // 1. Admin rolünü oluştur
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            // 2. User rolünü oluştur
            if (!await roleManager.RoleExistsAsync("User"))
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }

            // 3. Admin kullanıcısını oluştur
            var adminEmail = "b211210092@sakarya.edu.tr";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new User
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    NameSurname = "Admin Kullanıcı",
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(adminUser, "sau"); // Şifre 'sau'

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
                else
                {
                    // Hata durumunu ele alın
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine(error.Description);
                    }
                }
            }

            // İsteğe bağlı olarak, test amaçlı bir normal kullanıcı oluşturabilirsiniz
            var userEmail = "user@example.com";
            var normalUser = await userManager.FindByEmailAsync(userEmail);

            if (normalUser == null)
            {
                normalUser = new User
                {
                    UserName = userEmail,
                    Email = userEmail,
                    NameSurname = "Normal Kullanıcı",
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(normalUser, "User123!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(normalUser, "User");
                }
                else
                {
                    // Hata durumunu ele alın
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine(error.Description);
                    }
                }
            }

            // Hizmetleri ekle
            SeedServices(context);
        }

        private static void SeedServices(ApplicationDbContext context)
        {
            if (!context.Services.Any())
            {
                var services = new[]
                {
                    new Service { ServiceName = "Haircut", Duration = 30, Price = 20.00m },
                    new Service { ServiceName = "Shave", Duration = 20, Price = 15.00m },
                    new Service { ServiceName = "Beard Grooming", Duration = 25, Price = 18.00m },
                    new Service { ServiceName = "Hair + Beard", Duration = 50, Price = 35.00m }
                };

                context.Services.AddRange(services);
                context.SaveChanges();
            }
        }
    }
}