using DianaApp.Areas.Admin.ViewModels;
using DianaApp.Context;
using DianaApp.Models;
using DianaApp.ViewModels.ProductVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DianaApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        DianaContext _db { get; }
        IWebHostEnvironment _env { get; }

        public ProductController(DianaContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _db.Products.Select(x => new AdminProductListItemVM
            {
                Id = x.Id,
                Name = x.Name,
                Category = x.Category,
                CostPrice = x.CostPrice,
                Discount = x.Discount,
                ImageUrl = x.ImageUrl,
                Quantity = x.Quantity,
                IsDeleted = x.IsDeleted,
                SellPrice = x.SellPrice
            }).ToListAsync();
            return View(data);
        }
        public IActionResult Create()
        {
            ViewBag.Categories = _db.Categories;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateVM vm)
        {
            if (vm.CostPrice > vm.SellPrice)
            {
                ModelState.AddModelError("CostPrice", "Sell Price must be bigger than Cost Price ");
            };
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _db.Categories;
                return View(vm);
            }
            if (!await _db.Categories.AnyAsync(x => x.Id != vm.CategoryId))
            {
                ModelState.AddModelError("CategoryId", "Category not exsist");
                ViewBag.Categories = _db.Categories;
                return View(vm);
            }
            string FileName = Path.Combine("assets", "ProductImages", Path.GetRandomFileName()+Path.GetExtension(vm.Image.FileName));
            using (FileStream fs = System.IO.File.Create(Path.Combine(_env.WebRootPath, FileName)))
            {
                await vm.Image.CopyToAsync(fs);
            }
            Product product = new Product
            {
                Name = vm.Name,
                SellPrice = vm.SellPrice,
                About = vm.About,
                CategoryId = vm.CategoryId,
                CostPrice = vm.CostPrice,
                Description = vm.Description,
                Discount = vm.Discount,
                ImageUrl = FileName,
                Quantity = vm.Quantity,
            };
            await _db.AddAsync(product);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null || Id < 0) return BadRequest();
            var data = await _db.Products.FindAsync(Id);
            if (data == null) return NotFound();
            _db.Products.Remove(data);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
