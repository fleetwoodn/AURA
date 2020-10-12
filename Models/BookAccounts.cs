using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AURA.Models
{
    public class BookAccounts
    {
        [Key]
        public int AccountId { get; set; }
        public int AccountNumber { get; set; }
        public string AccountName { get; set; }
        
    }
}
