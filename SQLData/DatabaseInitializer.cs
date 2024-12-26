using KuaforYonetim1.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace KuaforYonetim1.Data
{
    public static class DatabaseInitializer
    {
        public static async Task Seed(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
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
        }
    }
}