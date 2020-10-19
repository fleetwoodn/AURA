using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AURA.Models
{
    public class AgentsAddress
    {

        [Key]
        public int Id { get; set; }

        [Display(Name = "User Id")]
        [ForeignKey("Agents")]
        public int UserId { get; set; }

        [StringLength(50, MinimumLength = 2)]
        [Required]
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }

        [StringLength(20, MinimumLength = 4)]
        [Required]
        [Display(Name = "Postal Code")]
        public string PostCode { get; set; }

        [Display(Name = "Notes")]
        public string Notes { get; set; }
    }
}
