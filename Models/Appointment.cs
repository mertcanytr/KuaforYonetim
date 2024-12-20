namespace KuaforYonetim1.Models
{
    // Models/Appointment.cs
    public class Appointment
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int StaffId { get; set; }
        public int ServiceId { get; set; }
        public DateTime AppointmentTime { get; set; }

        // Navigation properties
        public Customer Customer { get; set; }
        public Staff Staff { get; set; }
        public Service Service { get; set; }
    }

}
