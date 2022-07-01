using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PretzelFrontToBack.Models
{
    public class RomanceCard
    {
        public int Id { get; set; }
        public string CardImage { get; set; }
        [Required]
        public string CardTitle { get; set; }
        [Required]
        public string CardDesc { get; set; }
        [NotMapped]
        [Required]
        public IFormFile RomancePhoto { get; set; }
    }
}
