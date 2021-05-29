using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AURA.ViewModels
{
    public class WNineVM
    {
        public int UserId { get; set; }

        public string FullName { get; set; }

        public string TaxId { get; set; }

        public IEnumerable<AURA.Models.AgentsAddress> agentsAddresses { get; set; }

    }
}
