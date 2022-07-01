using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PretzelFrontToBack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PretzelFrontToBack.DAL
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
        public DbSet<Slider> sliders { get; set; }
        public DbSet<DeliciousTitle> DeliciousTitles { get; set; }
        public DbSet<DeliciousCard> deliciousCards { get; set; }
        public DbSet<Statistic> Statistics { get; set; }
        public DbSet<RomanceTitle> RomanceTitles { get; set; }
        public DbSet<RomanceCard> RomanceCards { get; set; }
    }
}
