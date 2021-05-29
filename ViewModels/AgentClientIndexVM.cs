using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AURA.ViewModels
{
    public class AgentClientIndexVM
    {
        public string Zero { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime ThrDate { get; set; }

        public string ThrTime { get; set; }

        public string ThrText { get; set; }

        public int EigId { get; set; }

        public string EigAgen { get; set; }

        public string EigRole { get; set; }

        public decimal EigLoad { get; set; }

        public string EigNote { get; set; }

        
    }
}
