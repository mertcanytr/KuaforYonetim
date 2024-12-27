using KuaforYonetim1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace KuaforYonetim1.SQLData
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // SQL Tables
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<StaffService> StaffServices { get; set; }
        public DbSet<StaffAvailability> StaffAvailabilities { get; set; }

        public DbSet<Salon> Salons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Çoka çok ilişki yapılandırması
            modelBuilder.Entity<StaffService>()
                .HasKey(ss => new { ss.StaffId, ss.ServiceId });

            modelBuilder.Entity<StaffService>()
                .HasOne(ss => ss.Staff)
                .WithMany(s => s.StaffServices)
                .HasForeignKey(ss => ss.StaffId);

            modelBuilder.Entity<StaffService>()
                .HasOne(ss => ss.Service)
                .WithMany(s => s.StaffServices)
                .HasForeignKey(ss => ss.ServiceId);
        }
    }
}