using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KuaforYonetim1.Models
{
    public class Staff
    {
        public int StaffId { get; set; }

        [Required(ErrorMessage = "Ad ve Soyad gereklidir.")]
        [StringLength(100, ErrorMessage = "Ad ve Soyad 100 karakterden uzun olamaz.")]
        public string NameSurname { get; set; }

        public ICollection<StaffService> StaffServices { get; set; } = new List<StaffService>();

        public ICollection<StaffAvailability> Availabilities { get; set; } = new List<StaffAvailability>();
    }
}