namespace KuaforYonetim1.Models
{
    // Models/Appointment.cs
    // Models/Staff.cs
    public class Staff
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Expertise { get; set; }
        public bool IsAvailable { get; set; } = true;
    }


}
