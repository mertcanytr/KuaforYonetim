using KuaforYonetim1.Models;
using KuaforYonetim1.SQLData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KuaforYonetim1.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public AppointmentController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        

        [HttpGet]
        public IActionResult Create()
        {
            var services = _dbContext.Services.ToList();
            ViewData["Services"] = services;
            var staffs = _dbContext.Staffs.ToList(); // Tüm personeli al
            ViewData["Staffs"] = staffs;
            ViewBag.staffs = staffs;
            return View("AppointmentBooking");
        }

        public async Task<IActionResult> GetServices()
        {
            var services = await _dbContext.Services.ToListAsync();
            return Json(services);
        }
        public async Task<IActionResult> GetAllStaff()
        {
            var staffList = await _dbContext.Staffs.ToListAsync(); 
            return Json(staffList);
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailableDays(int staffId)
        {
            var availableDays = await _dbContext.StaffAvailabilities
                .Where(sa => sa.StaffId == staffId)
                .Select(sa => sa.DayOfWeek)
                .ToListAsync();

            return Json(availableDays);
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailableTimes(int staffId, string date, int serviceId)
        {
            var selectedDate = DateTime.Parse(date);
            var appointments = await _dbContext.Appointments
                .Where(a => a.StaffId == staffId && a.AppointmentTime.Date == selectedDate.Date)
                .Select(a => a.AppointmentTime.TimeOfDay)
                .ToListAsync();

            var availability = await _dbContext.StaffAvailabilities
                .FirstOrDefaultAsync(sa => sa.StaffId == staffId && sa.DayOfWeek == selectedDate.DayOfWeek);

            if (availability == null)
            {
                return Json(new List<string>());
            }

            var availableTimes = new List<string>();
            var startTime = availability.StartTime;
            var endTime = availability.EndTime;

            while (startTime < endTime)
            {
                if (!appointments.Contains(startTime))
                {
                    availableTimes.Add(startTime.ToString(@"hh\:mm"));
                }
                startTime = startTime.Add(TimeSpan.FromMinutes(10));
            }

            return Json(availableTimes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmAppointment(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                appointment.Status = AppointmentStatus.Pending; // Randevu durumu beklemede olarak ayarlanır
                _dbContext.Appointments.Add(appointment);
                await _dbContext.SaveChangesAsync();
                TempData["message"] = "Your appointment has been booked and is pending approval.";
                return RedirectToAction("Index", "Home");
            }

            ViewData["Services"] = _dbContext.Services.ToList();
            return View("AppointmentBooking", appointment);
        }

        public IActionResult AdminPanel()
        {
            var pendingAppointments = _dbContext.Appointments
                .Where(a => a.Status == AppointmentStatus.Pending)
                .Include(a => a.Customer)
                .Include(a => a.Service)
                .ToList();

            return View(pendingAppointments);
        }

        [HttpPost]
        public IActionResult ApproveAppointment(int appointmentId)
        {
            var appointment = _dbContext.Appointments.Find(appointmentId);
            if (appointment != null)
            {
                appointment.Status = AppointmentStatus.Confirmed; // Onaylandı olarak ayarlanır
                _dbContext.SaveChanges();
            }

            return RedirectToAction("AdminPanel");
        }


    }
}