using KuaforYonetim1.Models;
using KuaforYonetim1.SQLData;
using KuaforYonetim1.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

[Authorize(Roles = "User")]
public class AppointmentController : Controller
{
    private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<User> _userManager;

    public AppointmentController(ApplicationDbContext dbContext, UserManager<User> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult Create()
    {
        // Hizmet ve çalışan listelerini alıp View'a gönderin
        var services = _dbContext.Services.ToList();
        var staffs = _dbContext.Staffs.ToList();
        var model = new AppointmentViewModel
        {
            Services = services,
            Staffs = staffs
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    //public async Task<IActionResult> Create(AppointmentViewModel model)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        // Müsaitlik kontrolü yapın
    //        var isAvailable = true; // Müsaitlik kontrolünü burada yapın

    //        if (isAvailable)
    //        {
    //            var userId = _userManager.GetUserId(User);
    //            var appointment = new Appointment
    //            {
    //                CustomerId = /* Kullanıcıdan veya modelden alın */ ,
    //                StaffId = model.StaffId,
    //                ServiceId = model.ServiceId,
    //                AppointmentTime = model.AppointmentTime,
    //                UserId = userId,
    //                Status = AppointmentStatus.Pending
    //            };

    //            _dbContext.Appointments.Add(appointment);
    //            await _dbContext.SaveChangesAsync();
    //            TempData["SuccessMessage"] = "Randevu talebiniz oluşturuldu.";
    //            return RedirectToAction("MyAppointments");
    //        }
    //        else
    //        {
    //            ModelState.AddModelError(string.Empty, "Seçilen saatlerde çalışan müsait değil.");
    //        }
    //    }
    //    // Hizmet ve çalışan listelerini tekrar alıp View'a gönderin
    //    model.Services = _dbContext.Services.ToList();
    //    model.Staffs = _dbContext.Staffs.ToList();
    //    return View(model);
    //}

    public IActionResult MyAppointments()
    {
        var userId = _userManager.GetUserId(User);
        var appointments = _dbContext.Appointments
            .Include(a => a.Staff)
            .Include(a => a.Service)
            .Where(a => a.UserId == userId)
            .ToList();
        return View(appointments);
    }
}