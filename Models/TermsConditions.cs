using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AURA.Models
{
    public class TermsConditions
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string FullText { get; set; }

        public DateTime LastUpdate { get; set; }
    }
}
