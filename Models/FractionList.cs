using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AURA.Models
{
    public class FractionList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string FractionId { get; set; }

        [ForeignKey("BookAccounts")]
        public int AccountNumber { get; set; }

        [ForeignKey("ProductList")]
        public int ProductId { get; set; }

        public string FractionDescription { get; set; }

        public string Note { get; set; }
    }
}
