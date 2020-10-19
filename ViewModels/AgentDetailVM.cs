using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AURA.ViewModels
{
    public class AgentDetailVM
    {
        
        public int UserId { get; set; }

        public string AuraId { get; set; }
 
        public string FullName { get; set; }

        public string NickName { get; set; }

        public DateTime BirthDate { get; set; }

        public string TaxId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string AuraRole { get; set; }


        public IEnumerable<AURA.Models.AgentsPhone> agentsPhones { get; set; }
        public IEnumerable<AURA.Models.AgentsEmail> agentsEmails { get; set; }
        public IEnumerable<AURA.Models.AgentsAddress> agentsAddresses { get; set; }
        public IEnumerable<AURA.Models.AgentsRoles> agentsRoles { get; set; }
        public IEnumerable<AURA.Models.AgentsRemarks> agentsRemarks { get; set; }
        public IEnumerable<AURA.Models.AgentsPayment> agentsPayments { get; set; }

    }
}
