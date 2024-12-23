using Microsoft.AspNetCore.Identity;

namespace KuaforYonetim1.Models
{
    
    public class User : IdentityUser
    {
        public string NameSurname { get; set; }
    }
    
}
