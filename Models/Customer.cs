namespace KuaforYonetim1.Models;

public class Customer
{
    public int Id { get; set; } // Primary Key
    public string FullName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public List<Appointment>? Appointments { get; set; } // İlişki
}