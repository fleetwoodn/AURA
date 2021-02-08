using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AURA.Models
{
    public class PostFiv
    {
        [Key]
        public int FivId { get; set; }

        [ForeignKey("PostZero")]
        [Display(Name = "0/")]
        public string FivZero { get; set; }

        [Display(Name = "Number")]
        public int FivDigit { get; set; }

        [Required]
        [Display(Name = "Priority Number")]
        [StringLength(1, MinimumLength = 1)]
        //[RegularExpression(?)] need to have only numbers 1-9
        public string FivPrio { get; set; }

        [Required]
        [Display(Name = "Code Letter")]
        [StringLength(1, MinimumLength = 1)]
        //[RegularExpression(?)] need to have only letters a-z any case
        public string FivCode { get; set; }

        [Required]
        [RegularExpression(@"[a-zA-Z0-9""'\s-|\.\=\+\*\/\\/¥():]*$")]
        [Display(Name = "Remark Text")]
        [StringLength(2000, MinimumLength = 1)]
        public string FivText { get; set; }
    }
}
