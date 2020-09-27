using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AURA.Models
{
    public class PostZero
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "0/")]
        public string Zero { get; set; }

        [Display(Name = "0/D")]
        [DataType(DataType.DateTime)]
        public DateTime ZeroDate { get; set; }

        [Display(Name = "0/A")]
        public string ZeroAgen { get; set; }
    }
}
