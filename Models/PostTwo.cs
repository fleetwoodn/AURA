
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//no posttwo table at this time

namespace AURA.Models
{
    public class PostTwo
    {
        [Key]
        public int TwoId { get; set; }

        [ForeignKey("PostZero")]
        [Display(Name = "0/")]
        public string TwoZero { get; set; }

        [Display(Name = "Number")]
        public string TwoNumber { get; set; }

        [Display(Name = "Service Date")]
        [DataType(DataType.Date)]
        public DateTime TwoDate { get; set; }

        [Display(Name = "Stage")]
        public string TwoStag { get; set; }

        [Display(Name = "Product")]
        public string TwoProd { get; set; }

        [Display(Name = "Depart Code")]
        [RegularExpression(@"[a-zA-Z0-9""'\s-|\.\=\+\*\/\\/¥():]*$")]
        public string TwoDepCode { get; set; }

        [Display(Name = "Arrival Code")]
        [RegularExpression(@"[a-zA-Z0-9""'\s-|\.\=\+\*\/\\/¥():]*$")]
        public string TwoArrCode { get; set; }

        [Display(Name = "Depart Time")]
        [RegularExpression(@"[a-zA-Z0-9""'\s-|\.\=\+\*\/\\/¥():]*$")]
        public string TwoDepTime { get; set; }

        [Display(Name = "Arrival Time")]
        [RegularExpression(@"[a-zA-Z0-9""'\s-|\.\=\+\*\/\\/¥():]*$")]
        public string TwoArrTime { get; set; }

        [Display(Name = "Elapsed Time")]
        [RegularExpression(@"[a-zA-Z0-9""'\s-|\.\=\+\*\/\\/¥():]*$")]
        public string TwoElpdTime { get; set; }

        [Display(Name = "Cargo Requirement")]
        public string CargoRequirement { get; set; }

        [Display(Name = "Status")]
        [RegularExpression(@"[a-zA-Z0-9""'\s-|\.\=\+\*\/\\/¥():]*$")]
        public string TwoStat { get; set; }

        [Display(Name = "Notes")]
        [RegularExpression(@"[a-zA-Z0-9""'\s-|\.\=\+\*\/\\/¥():]*$")]
        public string TwoNote { get; set; }
    }
}
