using DianaApp.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DianaApp.Controllers
{
    public class HomeController : Controller
    {
        DianaContext _db { get; }

        public HomeController(DianaContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var sliders=await _db.Sliders.ToListAsync();
            return View(sliders);
        }
    }
}
