namespace KuaforYonetim1.Models
{
    // Models/Appointment.cs
    public class Appointment
    {
        public int Id { get; set; } // Primary Key
        public DateTime AppointmentDate { get; set; }
        public int CustomerId { get; set; } // Foreign Key
        public Customer Customer { get; set; } = null!; // Customer ile ilişki
        public int StaffId { get; set; } // Foreign Key
        public Staff Staff { get; set; } = null!; // Staff ile ilişki
        public int ServiceId { get; set; } // Foreign Key
        public Service Service { get; set; } = null!; // Service ile ilişki
    }

}
