using KuaforYonetim1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KuaforYonetim1.ViewModels
{
    public class AppointmentViewModel
    {
        [Required]
        public int ServiceId { get; set; }

        [Required]
        public int StaffId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime AppointmentTime { get; set; }

        public List<Service> Services { get; set; }
        public List<Staff> Staffs { get; set; }
    }
}