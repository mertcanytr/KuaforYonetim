namespace KuaforYonetim1.Models
{
    // Models/Service.cs
    public class Service
    {
        public int Id { get; set; } // Primary Key
        public string Name { get; set; } = string.Empty; // Hizmet adı
        public decimal Price { get; set; } // Fiyat bilgisi
        public List<Appointment>? Appointments { get; set; } // Appointment ile ilişki
    }

}
