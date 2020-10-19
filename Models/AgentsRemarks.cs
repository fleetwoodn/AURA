using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AURA.Models
{
    public class AgentsRemarks
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Agents")]
        public int UserId { get; set; }

        [Display(Name = "Subject")]
        public string RemarkSubject { get; set; }

        [Display(Name = "Text")]
        public string RemarkText { get; set; }
    }
}
