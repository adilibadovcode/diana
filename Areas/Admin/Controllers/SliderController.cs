using DianaApp.Context;
using DianaApp.Models;
using DianaApp.ViewModels.SliderVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DianaApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        DianaContext _db { get; }

        public SliderController(DianaContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _db.Sliders.Select(s => new SliderListItemVM
            {
                Text = s.Text,
                ImageUrl = s.ImageUrl,
                IsLeft = s.IsLeft,
                Title = s.Title,
                Id = s.Id
            }).ToListAsync();
            return View(items);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(SliderCreateVM vm)
        {
            if (vm.Position < -1 || vm.Position > 1)
            {
                ModelState.AddModelError("Position", "Wrong input");
            }
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            Slider slider = new Slider
            {
                ImageUrl = vm.ImageUrl,
                Title = vm.Title,
                Text = vm.Text,
                IsLeft = vm.Position switch
                {
                    0 => null,
                    -1 => true,
                    1 => false
                },
            };
            await _db.Sliders.AddAsync(slider);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            var data = await _db.Sliders.FindAsync(id);
            if (data == null) return NotFound();
            _db.Sliders.Remove(data);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id < 0) return BadRequest();
            var data = await _db.Sliders.FindAsync(id);
            if (data == null) return NotFound();
            _db.Sliders.Remove(data);
            return View(new SliderUpdateVM
            {
                ImageUrl = data.ImageUrl,
                Title = data.Title,
                Position = data.IsLeft switch
                {
                    true => -1,
                    null => 0,
                    false => 1,
                },
                Text = data.Text,
            });

        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, SliderUpdateVM vm)
        {
            if (id == null || id < 0) return BadRequest();
            if (vm.Position < -1 || vm.Position > 1)
            {
                ModelState.AddModelError("Position", "Wrong input");
            }
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var data = await _db.Sliders.FindAsync(id);
            if (data == null) return NotFound();
            data.Text = vm.Text;
            data.Title = vm.Title;
            data.ImageUrl = vm.ImageUrl;
            data.IsLeft = vm.Position switch
            {
                0 => null,
                -1 => true,
                1 => false,
            };
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
