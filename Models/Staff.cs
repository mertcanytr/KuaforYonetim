namespace KuaforYonetim1.Models
{
    // Models/Appointment.cs
    // Models/Staff.cs
    public class Staff
    {
        public int Id { get; set; } // Primary Key
        public string FullName { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty; // Örn: Stilist, Asistan
        public List<Appointment>? Appointments { get; set; } // Appointment ile ilişki
    }


}
