using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AURA.Models
{
    public class PostFou
    {
        [Key]
        public int FouId { get; set; }

        [ForeignKey("PostZero")]
        [Display(Name = "0/")]
        public string FouZero { get; set; }

        [Display(Name = "Number")]
        public int FouDigit { get; set; }

        [Display(Name = "Full Name")]
        [RegularExpression(@"[a-zA-Z0-9""'\s-|\.\=\+\*\/\\]*$")]
        [StringLength(45)]
        public string FouName { get; set; }

        [Display(Name = "Phone")]
        [StringLength(20)]
        //[DataType(DataType.PhoneNumber)]
        public string FouPhon { get; set; }

        [Display(Name = "Email")]

        [StringLength(45)]
        public string FouEmai { get; set; }

        [Display(Name = "Street Address")]
        [RegularExpression(@"[a-zA-Z0-9""'\s-|\.\=\+\*\/\\]*$")]
        [StringLength(60)]
        public string FouAddr { get; set; }

        [Display(Name = "Postal Code")]
        [RegularExpression(@"[a-zA-Z0-9""'\s-|\.\=\+\*\/\\]*$")]
        [StringLength(10)]
        public string FouPost { get; set; }

        [Display(Name = "Apt/Organization/Comments")]
        [RegularExpression(@"[a-zA-Z0-9""'\s-|\.\=\+\*\/\\]*$")]
        [StringLength(45)]
        public string FouOrg { get; set; }

        [Display(Name = "Notes")]
        [RegularExpression(@"[a-zA-Z0-9""'\s-|\.\=\+\*\/\\]*$")]
        [StringLength(160)]
        public string FouNote { get; set; }
    }
}
