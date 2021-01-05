using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AURA.Models
{
    public class PostThr
    {
        //would be nice if we could require at least 1 record for each Zero record

        [Key]
        public int ThrId { get; set; }

        [ForeignKey("PostZero")]
        [Display(Name = "0/")]
        public string ThrZero { get; set; }

        //this to set programmatically or in db
        [Display(Name = "Number")]
        public int ThrDigit { get; set; }

        //[Required]
        //[DataType(DataType.DateTime)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH}", ApplyFormatInEditMode = true)]
        //[Display(Name = "TimeFrame")]
        //public DateTime ThrDate { get; set; }

        [Display(Name = "DateFrame")]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ThrDate { get; set; }

        [Display(Name = "HourFrame")]
        //[DataType(DataType.Time)]
        //[DisplayFormat(DataFormatString = "{0:HH}", ApplyFormatInEditMode = true)]
        //public DateTime ThrTime { get; set; }
        public string ThrTime { get; set; }


        [Required]
        [RegularExpression(@"[a-zA-Z0-9""'\s-|\.\=\+\*\/\\]*$")]
        [Display(Name = "Text")]
        [StringLength(160, MinimumLength = 2)]
        public string ThrText { get; set; }
    }
}
