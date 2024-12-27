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
            return View("AppointmentBooking");
        }

        [HttpGet]
        public async Task<IActionResult> GetStaff(int serviceId)
        {
            var staffList = await _dbContext.StaffServices
                .Where(ss => ss.ServiceId == serviceId)
                .Select(ss => ss.Staff)
                .ToListAsync();

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
                appointment.Status = AppointmentStatus.Pending;
                _dbContext.Appointments.Add(appointment);
                await _dbContext.SaveChangesAsync();
                TempData["message"] = "Your appointment has been booked and is pending approval.";
                return RedirectToAction("Index", "Home");
            }

            ViewData["Services"] = _dbContext.Services.ToList();
            return View("AppointmentBooking", appointment);
        }


    }
}