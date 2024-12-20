using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KuaforYonetim1.Models
{
    public class KuaforContext : IdentityDbContext<IdentityUser>
    {
        public KuaforContext(DbContextOptions<KuaforContext> options)
            : base(options)
        {
            Options = options;
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Staff> Staffs { get; set; }
    }

    public class IdentityDbContext<T>
    {
        private DbContextOptions<KuaforContext> options;

        public IdentityDbContext(DbContextOptions<KuaforContext> options)
        {
            this.options = options;
        }
        public DbContextOptions<KuaforContext> Options { get; }
    }
}