using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AURA.Models;

namespace AURA.Models
{
    public class PostOne
    {
        //the PostOne has to be a required entry
        //as in there MUST BE a PostOne record for each PostZero record
        //They must exist in a one-to-one relationship
        //not sure how to pull that off

        [Key]
        public int OneId { get; set; }

        [Display(Name = "0/")]
        [ForeignKey("PostZero")]
        public string OneZero { get; set; }

        [Required]
        [StringLength(5, MinimumLength = 3)]
        [Display(Name = "Stage")]
        public string OneStag { get; set; }

        [Required]
        [StringLength(35, MinimumLength = 2)]
        [Display(Name = "Lead Agent")]
        public string OneAgen { get; set; }

        [Required]
        [StringLength(35, MinimumLength = 4)]
        [Display(Name = "Party ID")]
        public string OnePart { get; set; }

        [StringLength(160)]
        [Display(Name = "Title")]
        public string OneTitle { get; set; }
    }
}
