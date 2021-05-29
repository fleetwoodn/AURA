using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AURA.ViewModels
{
    public class AvailableJobListVM
    {
        //public List<AgentClientIndexVM> JobList { get; set; }
        public IEnumerable<AURA.ViewModels.AgentClientIndexVM> JobList { get; set; }
    }
}
