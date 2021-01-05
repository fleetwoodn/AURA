using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AURA.Models
{
    public class PostSix
    {
        [Key]
        public int SixId { get; set; }

        [ForeignKey("PostZero")]
        [Display(Name = "0/")]
        public string SixZero { get; set; }

        [Display(Name = "Number")]
        public int SixDigit { get; set; }

        //[Required] enter this programmatically, so no need for required tag
        [Display(Name = "Entry Date")]
        [DataType(DataType.Date)]
        public DateTime SixDate { get; set; }

        [Required]
        [Display(Name = "Payment Type")]
        [RegularExpression(@"[a-zA-Z0-9""'\s-|\.\=\+\*\/\\]*$")]
        public string SixType { get; set; }

        [Display(Name = "Payment Detail")]
        [RegularExpression(@"[a-zA-Z0-9""'\s-|\.\=\+\*\/\\]*$")]
        public string SixDeta { get; set; }

        [Required]
        [Display(Name = "Payment Amount")]
        public decimal SixAmou { get; set; }

        [Display(Name = "Payment Status")]
        [RegularExpression(@"[a-zA-Z0-9""'\s-|\.\=\+\*\/\\]*$")]
        [StringLength(5, MinimumLength = 4)]
        public string SixStat { get; set; }

        [Display(Name = "Notes")]
        [RegularExpression(@"[a-zA-Z0-9""'\s-|\.\=\+\*\/\\]*$")]
        [StringLength(160)]
        public string SixNote { get; set; }
    }
}
