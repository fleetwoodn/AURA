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
        [DataType(DataType.DateTime)]
        public DateTime SixDate { get; set; }

        [Required]
        [Display(Name = "Payment Type")]
        public string SixType { get; set; }

        [Display(Name = "Payment Detail")]
        public string SixDeta { get; set; }

        [Required]
        [Display(Name = "Payment Amount")]
        public string SixAmou { get; set; }

        [Display(Name = "Payment Status")]
        [StringLength(5, MinimumLength = 4)]
        public string SixStat { get; set; }

        [Display(Name = "Notes")]
        [StringLength(160)]
        public string SixNote { get; set; }
    }
}
