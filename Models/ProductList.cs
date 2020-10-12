using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AURA.Models
{
    public class ProductList
    {
        [Key]
        public int ProductId { get; set; }
        
        [ForeignKey("BookAccounts")]
        public int AccountNumber { get; set; }
        
        [ForeignKey("FractionList")]
        public string FractionId { get; set; }
        
        public string ProductDescription { get; set; }
        
        public decimal ListPrice { get; set; }
        
        public DateTime EntryDate { get; set; }
        
        public DateTime ExpiryDate { get; set; }
        
        public string Note { get; set; }
    }
}
