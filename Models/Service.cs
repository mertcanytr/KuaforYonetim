using KuaforYonetim1.Models;


namespace KuaforYonetim1.Models
{
    public class Service
    {
        public int ServiceId { get; set; }
        public string? ServiceName { get; set; }
        public int Duration { get; set; } // Duration in minutes
        public decimal Price { get; set; }
        public int? SalonId { get; set; } // Nullable yapıldı
        public ICollection<StaffService> StaffServices { get; set; } = new List<StaffService>();

    }
}