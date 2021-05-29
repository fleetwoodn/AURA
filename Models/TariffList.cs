using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AURA.Models
{
    public class TariffList
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public decimal Amount { get; set; }

        public DateTime Updated { get; set; }

        public string Notes { get; set; }

        
    }
}
