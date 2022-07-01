using PretzelFrontToBack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PretzelFrontToBack.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Slider> sliders { get; set; }
        public DeliciousTitle deliciousTitle { get; set; }
        public IEnumerable<DeliciousCard> deliciousCards { get; set; }
        public Statistic statistic { get; set; }
        public RomanceTitle romanceTitle { get; set; }
        public IEnumerable<RomanceCard> romanceCards { get; set; }
    }
}
