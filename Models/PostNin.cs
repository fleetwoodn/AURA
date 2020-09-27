using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AURA.Models
{
    public class PostNin
    {
        [Key]
        public int NinId { get; set; }

        [ForeignKey("PostZero")]
        [Display(Name = "0/")]
        public string NinZero { get; set; }

        [Display(Name = "Number")]
        public string NinDigit { get; set; }

        [Display(Name = "File Upload")]
        public string NinFile { get; set; }

        [Required]
        [Display(Name = "File Caption")]
        [StringLength(160, MinimumLength = 2)]
        public string NinCapt { get; set; }

        [Display(Name = "Notes")]
        [StringLength(160)]
        public string NinNote { get; set; }
    }
}
