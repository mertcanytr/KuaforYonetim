using Microsoft.EntityFrameworkCore;

namespace KuaforYonetim1.Models
{
    public class KuaforContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Staff> Staff { get; set; } // Correct DbSet for Staff

        // Constructor to pass DbContext options
        public KuaforContext(DbContextOptions<KuaforContext> options) : base(options)
        {

        }
    }
}