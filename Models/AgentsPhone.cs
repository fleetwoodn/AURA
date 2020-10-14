using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AURA.Models
{
    public class AgentsPhone
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "User ID")]
        [ForeignKey("Agents")]
        public string UserId { get; set; }

        [Display(Name = "Phone Number")]
        [Required]
        public string PhoneNumber { get; set; }

        [Display(Name = "Country Code")]
        public string CountryCode { get; set; }

        [Display(Name = "Carrier Name")]
        public string CarrierName { get; set; }

        [Display(Name = "Email-to-Text Gateway Address")]
        public string EmailText { get; set; }

        public string Notes { get; set; }
    }
}
