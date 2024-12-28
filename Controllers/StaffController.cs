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
    public class StaffController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public StaffController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Çalışanları listeleme
        public async Task<IActionResult> Index()
        {
            var staffList = await _dbContext.Staffs.ToListAsync();
            return View(staffList);
        }

        // Yeni çalışan ekleme sayfası
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Days = _dbContext.Days.ToList(); // Çalışma günleri
            ViewBag.Services = _dbContext.Services.ToList(); // Hizmetler
            return View();
        }

        // Yeni çalışan ekleme işlemi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Staff staff, int[] SelectedDays, int[] SelectedServices)
        {
            if (ModelState.IsValid)
            {
                // Çalışan ekleme işlemleri
                _dbContext.Staffs.Add(staff);
                await _dbContext.SaveChangesAsync();
                TempData["SuccessMessage"] = "Çalışan başarıyla eklendi.";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Days = _dbContext.Days.ToList(); // Yeniden yükleme
            ViewBag.Services = _dbContext.Services.ToList(); // Yeniden yükleme
            return View(staff);
        }

        // Çalışan düzenleme sayfası
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var staff = await _dbContext.Staffs.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }
            return View(staff);
        }

        // Çalışan düzenleme işlemi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Staff staff)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Staffs.Update(staff);
                await _dbContext.SaveChangesAsync();
                TempData["SuccessMessage"] = "Çalışan bilgileri başarıyla güncellendi.";
                return RedirectToAction(nameof(Index));
            }
            return View(staff);
        }

        // Çalışan silme işlemi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var staff = await _dbContext.Staffs.FindAsync(id);
            if (staff != null)
            {
                _dbContext.Staffs.Remove(staff);
                await _dbContext.SaveChangesAsync();
                TempData["SuccessMessage"] = "Çalışan başarıyla silindi.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}