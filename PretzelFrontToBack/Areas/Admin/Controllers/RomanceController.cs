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
    public class RomanceController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public RomanceController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<RomanceCard> romanceCards = _context.RomanceCards.ToList();
            return View(romanceCards);
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
            RomanceCard dvRomanceCard = await _context.RomanceCards.FindAsync(id);
            if (dvRomanceCard == null) return NotFound();
            return View(dvRomanceCard);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RomanceCard romanceCard)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (ModelState["RomancePhoto"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }
            if (!romanceCard.RomancePhoto.IsImage())
            {
                ModelState.AddModelError("RomancePhoto", "File tipi img olmalidir!");
                return View();
            }
            if (romanceCard.RomancePhoto.LengthImage(5000))
            {
                ModelState.AddModelError("RomancePhoto", "File olcusu 5mbdan yuksek olmamalidir!");
                return View();
            }
            string fileName = await romanceCard.RomancePhoto.SaveImage(_env, "assets", "images");
            RomanceCard newRomanceCard = new RomanceCard();
            newRomanceCard.CardImage = fileName;
            newRomanceCard.CardTitle = romanceCard.CardTitle;
            newRomanceCard.CardDesc = romanceCard.CardDesc;
            await _context.RomanceCards.AddAsync(newRomanceCard);
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
            RomanceCard dbRomanceCard = await _context.RomanceCards.FindAsync(id);
            if (dbRomanceCard == null) return NotFound();
            Helpers.Helper.DeleteFile(_env, "assets", "images", dbRomanceCard.CardImage);
            _context.RomanceCards.Remove(dbRomanceCard);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            RomanceCard dbRomanceCard = await _context.RomanceCards.FindAsync(id);
            if (dbRomanceCard == null) return NotFound();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, RomanceCard romanceCard)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (ModelState["RomancePhoto"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }
            if (!romanceCard.RomancePhoto.IsImage())
            {
                ModelState.AddModelError("RomancePhoto", "File tipi img olmalidir!");
                return View();
            }
            if (romanceCard.RomancePhoto.LengthImage(50000))
            {
                ModelState.AddModelError("RomancePhoto", "File olcusu 5mbdan yuksek olmamalidir!");
                return View();
            }
            RomanceCard dbRomanceCard = await _context.RomanceCards.FindAsync(id);
            if (dbRomanceCard == null) return NotFound();
            string fileName = await romanceCard.RomancePhoto.SaveImage(_env, "assets", "images");
            dbRomanceCard.CardImage = fileName;
            dbRomanceCard.CardTitle = romanceCard.CardTitle;
            dbRomanceCard.CardDesc = romanceCard.CardDesc;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
