namespace KuaforYonetim1.Models
{
    // Models/Service.cs
    public class Service
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public int Duration { get; set; } // Duration in minutes
        public decimal Price { get; set; }

        public int SalonId { get; set; }
        public Salon Salon { get; set; }
        public TimeSpan EstimatedDuration { get; set; }


        public ICollection<StaffService> StaffServices { get; set; } = new List<StaffService>();


    }

}
