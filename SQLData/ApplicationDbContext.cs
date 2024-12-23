using KuaforYonetim1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace KuaforYonetim1.SQLData
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        //SQL Tables

        public DbSet<Customer> Customers  {get; set; }
        public DbSet<Appointment> Appointments {get; set;}
        public DbSet<Service> Services { get; set; }


            
     }
}
