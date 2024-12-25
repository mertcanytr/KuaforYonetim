using KuaforYonetim1.SQLData;
using KuaforYonetim1.Models;
using KuaforYonetim1.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace KuaforYonetim1.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public AdminController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult StaffList()
        {
            var staffMembers = _dbContext.Staffs
                .Include(s => s.StaffServices)
                .ThenInclude(ss => ss.Service)
                .ToList();

            return View(staffMembers);
        }

        [HttpGet]
        public IActionResult AddStaff()
        {
            var model = new AddStaffViewModel
            {
                AvailableServices = _dbContext.Services.ToList()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddStaff(AddStaffViewModel model)
        {
            if (ModelState.IsValid)
            {
                var staff = new Staff
                {
                    NameSurname = model.NameSurname
                };

                foreach (var serviceId in model.SelectedServices)
                {
                    var service = _dbContext.Services.Find(serviceId);
                    if (service != null)
                    {
                        staff.StaffServices.Add(new StaffService
                        {
                            Staff = staff,
                            Service = service
                        });
                    }
                }

                _dbContext.Staffs.Add(staff);
                _dbContext.SaveChanges();
                TempData["SuccessMessage"] = "Çalışan başarıyla eklendi.";
                return RedirectToAction("StaffList");
            }

            model.AvailableServices = _dbContext.Services.ToList();
            return View(model);
        }

        public IActionResult RemoveStaff(int id)
        {
            var staff = _dbContext.Staffs.Find(id);
            if (staff != null)
            {
                _dbContext.Staffs.Remove(staff);
                _dbContext.SaveChanges();
                TempData["SuccessMessage"] = "Çalışan başarıyla silindi.";
            }
            return RedirectToAction("StaffList");
        }

        public IActionResult AppointmentList()
        {
            var pendingAppointments = _dbContext.Appointments
                .Include(a => a.Staff)
                .Include(a => a.Service)
                .Include(a => a.Customer)
                .Where(a => a.Status == AppointmentStatus.Pending)
                .ToList();

            var confirmedAppointments = _dbContext.Appointments
                .Include(a => a.Staff)
                .Include(a => a.Service)
                .Include(a => a.Customer)
                .Where(a => a.Status == AppointmentStatus.Confirmed)
                .ToList();

            ViewBag.ConfirmedAppointments = confirmedAppointments;

            return View(pendingAppointments);
        }

        public IActionResult ApproveAppointment(int id)
        {
            var appointment = _dbContext.Appointments.Find(id);
            if (appointment != null)
            {
                appointment.Status = AppointmentStatus.Confirmed;
                _dbContext.SaveChanges();
                TempData["SuccessMessage"] = "Randevu başarıyla onaylandı.";
            }
            return RedirectToAction("AppointmentList");
        }

        public IActionResult RejectAppointment(int id)
        {
            var appointment = _dbContext.Appointments.Find(id);
            if (appointment != null)
            {
                appointment.Status = AppointmentStatus.Rejected;
                _dbContext.SaveChanges();
                TempData["SuccessMessage"] = "Randevu başarıyla reddedildi.";
            }
            return RedirectToAction("AppointmentList");
        }
    }
}