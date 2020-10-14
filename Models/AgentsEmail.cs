using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AURA.Models
{
    public class AgentsEmail
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "User ID")]
        [ForeignKey("Agents")]
        public string UserId { get; set; }

        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        public string Notes { get; set; }

    }
}
