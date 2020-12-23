using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AURA.Models
{
    public class TermsConditions
    {
        public int ID { get; set; }
        
        public DateTime EntryDate { get; set; }
        
        [Required]
        [StringLength(10000, MinimumLength = 1)]
        public string Text { get; set; }
    }
}
