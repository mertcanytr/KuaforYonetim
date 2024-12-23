using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace KuaforYonetim1.Models
{
    // Models/Appointment.cs
    // Models/Staff.cs
    public class Staff
    {
        public int StaffId { get; set; }

        [Required(ErrorMessage = "Name and Surname are required.")]
        [StringLength(100, ErrorMessage = "Name and Surname cannot be longer than 100 characters.")]
        public string NameSurname { get; set; }

        [StringLength(100, ErrorMessage = "Expertise cannot be longer than 100 characters.")]
        public string Expertise { get; set; }
        public bool IsAvailable { get; set; } = true;

        public List<StaffIsAvailable> StaffAvailability { get; set; } = new List<StaffIsAvailable>();
        
        public List<Service> Services { get; set; } = new List<Service>();

        public ICollection<StaffService> StaffServices { get; set; } = new List<StaffService>();
    
    }


}
