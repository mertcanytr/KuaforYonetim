using KuaforYonetim1.Models;
using KuaforYonetim1.SQLData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace KuaforYonetim1.Controllers
{
    public class ServiceController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public ServiceController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Hizmetleri listeleme
        public async Task<IActionResult> Index()
        {
            var services = await _dbContext.Services.ToListAsync();
            return View(services);
        }
    }
}