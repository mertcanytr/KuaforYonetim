using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace KuaforYonetim1.Models
{


    

    // Models/Appointment.cs
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int CustomerId { get; set; }
        public int StaffId { get; set; }
        public int ServiceId { get; set; }
        
        public string UserId { get; set; }
        public DateTime AppointmentTime { get; set; }

        [CustomValidation(typeof(Appointment),nameof(ValidateDate))]

        // Navigation properties
        public Customer Customer { get; set; }
        public Staff Staff { get; set; }
        public Service Service { get; set; }

        public User user { get; set; }


        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;
        public static ValidationResult ValidateDate(DateTime AppointmentTime, ValidationContext context)
        {
            if (AppointmentTime < DateTime.Now)
            {
                return new ValidationResult("you can't pick a past date\r\n");
            }
            return ValidationResult.Success;
        }
    }
}
