using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PretzelFrontToBack.DAL;
using PretzelFrontToBack.Extentions;
using PretzelFrontToBack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PretzelFrontToBack.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Slider> sliders = _context.sliders.ToList();
            return View(sliders);
        }

        public async Task<IActionResult>Detail(int? id)
        {
            if (id == null) return NotFound();
            Slider dbSlider =await _context.sliders.FindAsync(id);
            if (dbSlider == null) return NotFound();
            return View(dbSlider);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider slider)
        {
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }
            if (!slider.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "File tipi img olmalidir!");
                return View();
            }
            if (slider.Photo.LengthImage(5000))
            {
                ModelState.AddModelError("Photo", "File olcusu 5mbdan yuksek olmamalidir!");
                return View();
            }
            string fileName = await slider.Photo.SaveImage(_env, "assets", "images");
            Slider newSlider = new Slider();
            newSlider.ImageUrl = fileName;
            await _context.sliders.AddAsync(newSlider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Slider dbSlider = await _context.sliders.FindAsync(id);
            if (dbSlider == null) return NotFound();
            Helpers.Helper.DeleteFile(_env, "assets", "images", dbSlider.ImageUrl);
            _context.sliders.Remove(dbSlider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            Slider dbSlider =await _context.sliders.FindAsync(id);
            if (dbSlider == null) return NotFound();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(int? id, Slider slider)
        {
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }
            if (!slider.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "File tipi img olmalidir!");
                return View();
            }
            if (slider.Photo.LengthImage(50000))
            {
                ModelState.AddModelError("Photo", "File olcusu 5mbdan yuksek olmamalidir!");
                return View();
            }
            Slider dbSlider =await _context.sliders.FindAsync(id);
            if (dbSlider == null) return NotFound();
            string fileName = await slider.Photo.SaveImage(_env, "assets", "images");
            dbSlider.ImageUrl = fileName;
            await _context.SaveChangesAsync();
            return View();
        }
    }
}
