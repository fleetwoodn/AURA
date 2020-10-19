using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AURA.Models
{
    public class AgentsRoles
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "User ID")]
        [ForeignKey("Agents")]
        public int UserId { get; set; }

        [Required]
        [Display(Name = "Agent Role")]
        public string AgentRole { get; set; }

        [Required]
        [Display(Name = "Role Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Role End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Pay Type")]
        public string PayType { get; set; }

        [Display(Name = "Pay Rate")]
        public string PayRate { get; set; }

        public string Notes { get; set; }
    }
}
