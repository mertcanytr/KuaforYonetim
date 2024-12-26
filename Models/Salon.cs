namespace KuaforYonetim1.Models
{
    public class Salon
    {
        public int SalonId { get; set; }
        public string SalonName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<Service> Services { get; set; } = new List<Service>();
        public ICollection<Staff> Staffs { get; set; } = new List<Staff>();
    }
}