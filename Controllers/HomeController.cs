using KuaforYonetim1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KuaforYonetim1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AdminLogin()
        {
            return View();  // Admin giri� sayfas� i�in View d�nd�r�l�r
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Admin login sayfas�na y�nlendirme
        

        // Kullan�c� login sayfas�na y�nlendirme (�u anl�k olarak admini yapaca��z)
        public IActionResult UserLogin()
        {
            return View();
        }

        public IActionResult ValidateAdmin(string username, string password)
        {
            // Admin kullan�c� ad� ve �ifre kontrol�
            // Burada admin ad� ve �ifresi sabit olacak, ilerleyen zamanlarda veritaban�na ba�layabiliriz.
            if (username == "admin" && password == "admin123") // �rnek: admin giri� bilgisi
            {
                return RedirectToAction("AdminDashboard", "Admin"); // Admin paneline y�nlendirme
            }
            else
            {
                TempData["ErrorMessage"] = "Invalid Username or Password";
                return RedirectToAction("AdminLogin"); // Hatal� giri�, tekrar giri� sayfas�na d�n
            }
        }
    }
}
