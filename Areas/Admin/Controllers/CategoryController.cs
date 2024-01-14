using DianaApp.Context;
using DianaApp.ViewModels.CategoryVM;
using DianaApp.ViewModels.SliderVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DianaApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        DianaContext _db { get; }

        public CategoryController(DianaContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _db.Categories.Select(s => new CategoryListItemVM
            {
                Name = s.Name,
                Id = s.Id
            }).ToListAsync();
            return View(items);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CategoryCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            if (await _db.Categories.AnyAsync(x => x.Name == vm.Name))
            {
                ModelState.AddModelError("Name", "Category Name Already Exist");
                return View(vm);
            }
            await _db.Categories.AddAsync(new Models.Category { Name = vm.Name });
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id < 0) return BadRequest();
            var data = await _db.Categories.FindAsync(id);
            if (data == null) return NotFound();
            _db.Categories.Remove(data);
            return View(new CategoryUpdateVM
            {
                Name = data.Name,
            });

        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryUpdateVM vm, int? Id)
        {
            if (Id == null || Id < 0) return BadRequest();
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var data = await _db.Categories.FindAsync(Id);
            if (data == null) return NotFound();
            data.Name = vm.Name;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null || Id < 0) return BadRequest();
            var data = await _db.Categories.FindAsync(Id);
            if (data == null) return NotFound();
            _db.Categories.Remove(data);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
