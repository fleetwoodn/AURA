using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AURA.Models
{
    public class PostSev
    {
        //intentional that there is no required field here
        [Key]
        public int SevId { get; set; }

        [ForeignKey("PostZero")]
        [Display(Name = "0/")]
        public string SevZero { get; set; }

        [Display(Name = "Number")]
        public int SevDigit { get; set; }

        [Display(Name = "Invoice")]
        public string SevInvo { get; set; }

        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Entry Date")]
        //[RegularExpression(@"[a-zA-Z0-9""'\s-|\.\=\+\*\/\\@]*$")]
        public DateTime SevDate { get; set; }

        [Display(Name = "Description")]
        [RegularExpression(@"[a-zA-Z0-9""'\s-|\.\=\+\*\/\\@]*$")]
        public string SevDesc { get; set; }

        [Display(Name = "Amount")]
        public decimal SevAmou { get; set; }

        [Display(Name = "Ac1")]
        [RegularExpression(@"[a-zA-Z0-9""'\s-|\.\=\+\*\/\\@]*$")]
        public string SevAc1 { get; set; }

        [Display(Name = "Ac2")]
        [RegularExpression(@"[a-zA-Z0-9""'\s-|\.\=\+\*\/\\@]*$")]
        public string SevAc2 { get; set; }

        [Display(Name = "AcF")]
        [RegularExpression(@"[a-zA-Z0-9""'\s-|\.\=\+\*\/\\@]*$")]
        public string SevAcf { get; set; }

        [Display(Name = "Agent Sign")]
        [RegularExpression(@"[a-zA-Z0-9""'\s-|\.\=\+\*\/\\@]*$")]
        public string SevSign { get; set; }

        [Display(Name = "Stage")]
        [RegularExpression(@"[a-zA-Z0-9""'\s-|\.\=\+\*\/\\@]*$")]
        public string SevStage { get; set; } //make sure you reference this...

        [Display(Name = "Party ID")]
        [RegularExpression(@"[a-zA-Z0-9""'\s-|\.\=\+\*\/\\@]*$")]
        public string SevPart { get; set; }

        [Display(Name = "Customer ID")]
        [RegularExpression(@"[a-zA-Z0-9""'\s-|\.\=\+\*\/\\@]*$")]
        public string SevCust { get; set; }

        [Display(Name = "Status")]
        [RegularExpression(@"[a-zA-Z0-9""'\s-|\.\=\+\*\/\\@]*$")]
        public string SevStat { get; set; }

        [Display(Name = "Is Payment?")]
        public string SevPaym { get; set; } //should this be "boolean"?? entries should be "TRUE" or "FALSE"

        [Display(Name = "Reference")]
        [RegularExpression(@"[a-zA-Z0-9""'\s-|\.\=\+\*\/\\@]*$")]
        public string SevRefe { get; set; }

        [Display(Name = "Is Hidden?")]
        public string SevHidd { get; set; } //should this be "boolean"?? entries should be "TRUE" or "FALSE"

        [Display(Name = "Check")]
        [RegularExpression(@"[a-zA-Z0-9""'\s-|\.\=\+\*\/\\@]*$")]
        public string SevChec { get; set; }

        [Display(Name = "Notes")]
        [RegularExpression(@"[a-zA-Z0-9""'\s-|\.\=\+\*\/\\@]*$")]
        public string SevNote { get; set; }
    }
}
