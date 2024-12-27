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
            // Admin rolünü oluştur
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            // User rolünü oluştur
            if (!await roleManager.RoleExistsAsync("User"))
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }

            // Admin kullanıcısını oluştur ve rol ata
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
                var result = await userManager.CreateAsync(adminUser, "sau");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
            else if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }

            // Normal kullanıcı oluştur ve rol ata
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
            }
            else if (!await userManager.IsInRoleAsync(normalUser, "User"))
            {
                await userManager.AddToRoleAsync(normalUser, "User");
            }

            // Günleri ve hizmetleri ekle
            SeedSalons(context);
            SeedDays(context);
            SeedServices(context);
        }

        private static void SeedSalons(ApplicationDbContext context)
        {
            if (!context.Salons.Any())
            {
                var salons = new[]
                {
                    new Salon { SalonName = "Main Salon", Address = "123 Main St", PhoneNumber = "123-456-7890" }
                };

                context.Salons.AddRange(salons);
                context.SaveChanges();
            }
        }

        private static void SeedDays(ApplicationDbContext context)
        {
            if (!context.Days.Any())
            {
                var days = new[]
                {
                    new Day { DayName = "Pazartesi" },
                    new Day { DayName = "Salı" },
                    new Day { DayName = "Çarşamba" },
                    new Day { DayName = "Perşembe" },
                    new Day { DayName = "Cuma" },
                    new Day { DayName = "Cumartesi" },
                    new Day { DayName = "Pazar" }
                };

                context.Days.AddRange(days);
                context.SaveChanges();
            }
        }

        private static void SeedServices(ApplicationDbContext context)
        {
            if (!context.Services.Any())
            {
                var services = new[]
                {
                    new Service { ServiceName = "Haircut", Duration = 30, Price = 20, SalonId = 1 },
                    new Service { ServiceName = "Shave", Duration = 20, Price = 15, SalonId = 1 },
                    new Service { ServiceName = "Beard Grooming", Duration = 25, Price = 18, SalonId = 1 },
                    new Service { ServiceName = "Hair + Beard", Duration = 50, Price = 35, SalonId = 1 }
                };

                context.Services.AddRange(services);
                context.SaveChanges();
            }
        }
    }
}