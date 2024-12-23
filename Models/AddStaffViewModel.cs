using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KuaforYonetim1.Models;

namespace KuaforYonetim1.ViewModels
{
    public class AddStaffViewModel
    {
        [Required(ErrorMessage = "Ad ve Soyad gereklidir.")]
        [StringLength(100, ErrorMessage = "Ad ve Soyad 100 karakterden uzun olamaz.")]
        public string NameSurname { get; set; }

        // Kullanıcının seçtiği hizmetlerin ID'lerini saklar
        public List<int> SelectedServices { get; set; } = new List<int>();

        // Tüm hizmetlerin listesi, formda seçim için kullanılır
        public List<Service> AvailableServices { get; set; } = new List<Service>();
    }
}
