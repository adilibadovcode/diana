using DianaApp.Context;
using DianaApp.ViewModels.HomeVM;
using DianaApp.ViewModels.ProductVM;
using DianaApp.ViewModels.SliderVM;
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
            HomeVM vm = new HomeVM
            {
                Sliders = await _db.Sliders.Select(s => new SliderListItemVM
                {
                    Id = s.Id,
                    ImageUrl = s.ImageUrl,
                    IsLeft = s.IsLeft,
                    Text = s.Text,
                    Title = s.Title
                }).ToListAsync(),

                Products = await _db.Products.Where(p=>p.IsDeleted==false).Select(s => new ProductListItemVM
                {
                    Id = s.Id,
                    About=s.About,
                    ImageUrl=s.ImageUrl,
                    Description = s.Description,
                    Discount = s.Discount,
                    Name = s.Name,
                    Quantity = s.Quantity,
                    SellPrice = s.SellPrice
                }).ToListAsync(),
            };
            return View(vm);
        }
    }
}
