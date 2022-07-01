using Microsoft.AspNetCore.Mvc;
using PretzelFrontToBack.DAL;
using PretzelFrontToBack.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PretzelFrontToBack.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM();
            homeVM.sliders = _context.sliders.ToList();
            homeVM.deliciousTitle = _context.DeliciousTitles.FirstOrDefault();
            homeVM.deliciousCards = _context.deliciousCards.ToList();
            homeVM.statistic = _context.Statistics.FirstOrDefault();
            homeVM.romanceTitle = _context.RomanceTitles.FirstOrDefault();
            homeVM.romanceCards = _context.RomanceCards.ToList();
            return View(homeVM);
        }
    }
}
