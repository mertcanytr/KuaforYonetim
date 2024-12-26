using KuaforYonetim1.Models;
using KuaforYonetim1.SQLData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

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

        // Çalışanların uygunluk saatlerini listeleme
        public IActionResult StaffAvailabilityList(int staffId)
        {
            var availabilities = _dbContext.StaffAvailabilities
                .Where(sa => sa.StaffId == staffId)
                .Include(sa => sa.Staff)
                .ToList();

            ViewBag.StaffId = staffId;
            return View(availabilities);
        }

        // Uygunluk saati ekleme
        [HttpGet]
        public IActionResult AddStaffAvailability(int staffId)
        {
            var model = new StaffAvailability
            {
                StaffId = staffId
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddStaffAvailability(StaffAvailability model)
        {
            if (ModelState.IsValid)
            {
                _dbContext.StaffAvailabilities.Add(model);
                _dbContext.SaveChanges();
                TempData["SuccessMessage"] = "Uygunluk saati başarıyla eklendi.";
                return RedirectToAction("StaffAvailabilityList", new { staffId = model.StaffId });
            }
            return View(model);
        }

        // Uygunluk saati düzenleme
        [HttpGet]
        public IActionResult EditStaffAvailability(int id)
        {
            var availability = _dbContext.StaffAvailabilities.Find(id);
            if (availability == null)
            {
                return NotFound();
            }
            return View(availability);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditStaffAvailability(StaffAvailability model)
        {
            if (ModelState.IsValid)
            {
                _dbContext.StaffAvailabilities.Update(model);
                _dbContext.SaveChanges();
                TempData["SuccessMessage"] = "Uygunluk saati başarıyla güncellendi.";
                return RedirectToAction("StaffAvailabilityList", new { staffId = model.StaffId });
            }
            return View(model);
        }

        // Uygunluk saati silme
        public IActionResult DeleteStaffAvailability(int id)
        {
            var availability = _dbContext.StaffAvailabilities.Find(id);
            if (availability != null)
            {
                _dbContext.StaffAvailabilities.Remove(availability);
                _dbContext.SaveChanges();
                TempData["SuccessMessage"] = "Uygunluk saati başarıyla silindi.";
            }
            return RedirectToAction("StaffAvailabilityList", new { staffId = availability.StaffId });
        }
    }
}