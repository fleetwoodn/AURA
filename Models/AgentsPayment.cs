using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AURA.Models
{
    public class AgentsPayment
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "User ID")]
        [ForeignKey("Agents")]
        public int UserId { get; set; }

        [Display(Name = "Aura Id")]
        public string AuraId { get; set; }

        //[Display(Name = "Tax Type")]
        //public string TaxType { get; set; }

        //[Display(Name = "Withholding Type")]
        //public string WitholdingType { get; set; }

        [Display(Name = "Payment Type")]
        public string PaymentType { get; set; }

        [Display(Name = "Payment Detail")]
        public string PaymentDetail { get; set; }
        
        public string Notes { get; set; }
    }
}
