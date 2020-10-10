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

        [Display(Name = "Entry Date")]
        public DateTime SevDate { get; set; }

        [Display(Name = "Description")]
        public string SevDesc { get; set; }

        [Display(Name = "Amount")]
        public string SevAmou { get; set; }

        [Display(Name = "Ac1")]
        public string SevAc1 { get; set; }

        [Display(Name = "Ac2")]
        public string SevAc2 { get; set; }

        [Display(Name = "AcF")]
        public string SevAcf { get; set; }

        [Display(Name = "Agent Sign")]
        public string SevSign { get; set; }

        [Display(Name = "Stage")]
        public string SevStage { get; set; } //make sure you reference this...

        [Display(Name = "Party ID")]
        public string SevPart { get; set; }

        [Display(Name = "Customer ID")]
        public string SevCust { get; set; }

        [Display(Name = "Status")]
        public string SevStat { get; set; }

        [Display(Name = "Is Payment?")]
        public string SevPaym { get; set; } //should this be "boolean"?? entries should be "TRUE" or "FALSE"

        [Display(Name = "Reference")]
        public string SevRefe { get; set; }

        [Display(Name = "Is Hidden?")]
        public string SevHidd { get; set; } //should this be "boolean"?? entries should be "TRUE" or "FALSE"

        [Display(Name = "Check")]
        public string SevChec { get; set; }

        [Display(Name = "Notes")]
        public string SevNote { get; set; }
    }
}
