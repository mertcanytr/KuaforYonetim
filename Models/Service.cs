namespace KuaforYonetim1.Models
{
    // Models/Service.cs
    public class Service
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public int Duration { get; set; } // Duration in minutes
        public decimal Price { get; set; }
    }

}
