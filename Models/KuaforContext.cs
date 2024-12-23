using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace KuaforYonetim1.Models
{
    public class KuaforContext : IdentityDbContext<IdentityUser>
    {
        public KuaforContext(DbContextOptions<KuaforContext> options) : base(options) { }
        

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        
        public DbSet <StaffIsAvailable> StaffAvailabilities { get; set; }
    }

  
}