using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AURA.ViewModels
{
    public class EigCreateVM
    {
        [Display(Name = "0/")]
        public string EigZero { get; set; }

        [Display(Name = "Number")]
        public int EigDigit { get; set; }

        //[ForeignKey("Agents")]
        //[Required]
        //[Display(Name = "Agent ID")]
        //[StringLength(10, MinimumLength = 1)]
        //public string EigAgen { get; set; }
        public List<SelectListItem> ListOfAgents { get; set; }

        [Required]
        [Display(Name = "Agent Role")]
        [StringLength(5, MinimumLength = 1)]
        public string EigRole { get; set; }
    }
}
