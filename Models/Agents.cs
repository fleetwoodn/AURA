using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AURA.Models
{
    public class Agents
    {
        [Display(Name = "UserId")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string UserId { get; set; }

        [Display(Name = "Full Legal Name")]
        public string FullName { get; set; }
        
        [Display(Name = "Nickname")]
        public string NickName { get; set; }
        
        [Display(Name = "Birthdate")]
        public DateTime BirthDate { get; set; }
        
        [Display(Name = "Tax Id")]
        public string TaxId { get; set; }
        
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "AURA Role")]
        public string AuraRole { get; set; }

    }
}
