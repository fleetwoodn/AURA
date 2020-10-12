using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AURA.Models
{
    public class Journal
    {
        [Key]
        public string InvoiceNumber { get; set; }
        
        public DateTime InvoiceDate { get; set; }

        public string Description { get; set; }

        public decimal Amount { get; set; }

        public string Ac1 { get; set; }

        public string Ac2 { get; set; }

        public string Acf { get; set; }

        public string Sign { get; set; }

        public string Stage { get; set; }

        public string PartyId { get; set; }

        public string Source { get; set; }

        public string ForeignKey { get; set; }

        public string PostId { get; set; }

        public string Status { get; set; }

        public bool IsPayment { get; set; }

        public string Reference { get; set; }

        public bool IsHidden { get; set; }

        public string Check { get; set; }

        public string Note { get; set; }
    }
}
