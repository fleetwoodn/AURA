using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AURA.Models
{
    public class Manifest
    {
        public int Id { get; set; }
        
        [Required]
        public string Zero { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Wrap { get; set; }
        public string Assembly { get; set; }
        public string Notes { get; set; }
    }
}
