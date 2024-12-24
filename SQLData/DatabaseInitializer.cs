using KuaforYonetim1.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace KuaforYonetim1.Data
{
    public static class DatabaseInitializer
    {
        public static async Task Seed(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Admin rolü oluştur
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            // Admin kullanıcısını oluştur
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
        }
    }
}