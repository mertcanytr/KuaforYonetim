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
            return View();  // Admin giriþ sayfasý için View döndürülür
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

        // Admin login sayfasýna yönlendirme
        

        // Kullanýcý login sayfasýna yönlendirme (þu anlýk olarak admini yapacaðýz)
        public IActionResult UserLogin()
        {
            return View();
        }

        public IActionResult ValidateAdmin(string username, string password)
        {
            // Admin kullanýcý adý ve þifre kontrolü
            // Burada admin adý ve þifresi sabit olacak, ilerleyen zamanlarda veritabanýna baðlayabiliriz.
            if (username == "admin" && password == "admin123") // Örnek: admin giriþ bilgisi
            {
                return RedirectToAction("AdminDashboard", "Admin"); // Admin paneline yönlendirme
            }
            else
            {
                TempData["ErrorMessage"] = "Invalid Username or Password";
                return RedirectToAction("AdminLogin"); // Hatalý giriþ, tekrar giriþ sayfasýna dön
            }
        }
    }
}
