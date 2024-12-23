namespace KuaforYonetim1.Models
{
    public class StaffIsAvailable
    {
        public int Id { get; set; }
        public int StaffId { get; set; }
        public Staff Staff { get; set; }
        public DateTime AvailableDate { get; set; }
    }
}