using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AURA.Models
{
    public class PostEig
    {
        [Key]
        public int EigId { get; set; }

        [ForeignKey("PostZero")]
        [Display(Name = "0/")]
        public string EigZero { get; set; }

        [Display(Name = "Number")]
        public int EigDigit { get; set; }

        //[ForeignKey("Agents")] to come later
        [Required]
        [Display(Name = "Agent ID")]
        [StringLength(10, MinimumLength = 1)]
        public string EigAgen { get; set; }

        [Required]
        [Display(Name = "Agent Role")]
        [StringLength(5, MinimumLength = 1)]
        public string EigRole { get; set; }

        [Display(Name = "Agent Load")]
        [StringLength(3)]
        public string EigLoad { get; set; }

        [Display(Name = "Notes")]
        [RegularExpression(@"[a-zA-Z0-9""'\s-|\.\=\+\*\/\\]*$")]
        [StringLength(160)]
        public string EigNote { get; set; }
    }
}
