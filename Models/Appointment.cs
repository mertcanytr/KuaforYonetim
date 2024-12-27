using System.ComponentModel.DataAnnotations;

namespace KuaforYonetim1.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int CustomerId { get; set; }
        public int StaffId { get; set; }
        public int ServiceId { get; set; }

        public string? UserId { get; set; }

        [Required]
        public DateTime AppointmentTime { get; set; }

        [CustomValidation(typeof(Appointment), nameof(ValidateDate))]
        public Customer Customer { get; set; }
        public Staff Staff { get; set; }
        public Service Service { get; set; }
        public User User { get; set; }

        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;

        public static ValidationResult ValidateDate(DateTime appointmentTime, ValidationContext context)
        {
            if (appointmentTime < DateTime.Now)
            {
                return new ValidationResult("You can't pick a past date.");
            }
            return ValidationResult.Success;
        }
    }
}