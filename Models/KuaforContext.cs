using Microsoft.EntityFrameworkCore;

namespace KuaforYonetim1.Models
{
    public class KuaforContext : DbContext
    {
        public DbSet<Personeldene> Personeller { get; set; }

        public KuaforContext(DbContextOptions options) : base(options)
        {

        }
    }
}
