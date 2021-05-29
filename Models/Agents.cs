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
        public int UserId { get; set; }

        [Display(Name = "AURA ID")]
        public string AuraId { get; set; }

        [StringLength(60, MinimumLength = 2)]
        [Required]
        [Display(Name = "Full Legal Name")]
        public string FullName { get; set; }

        [Display(Name = "Nickname")]
        public string NickName { get; set; }

        //stuff to add
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        //end other stuff

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Birthdate")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Tax Id")]
        public string TaxId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "AURA Role")]
        public string AuraRole { get; set; }

        [Display(Name = "Tax Type")]
        public string TaxType { get; set; }

        [Display(Name = "Backup Witholding")]
        public bool BackupWitholding { get; set; }

        [Display(Name = "Payment Type")]
        public string PaymentType { get; set; }

        [Display(Name = "Payment Detail")]
        public string PaymentDetail { get; set; }

        [Required]
        [Display(Name = "Street Address, City, and State")]
        public string StreetAddress { get; set; }

        [Required]
        [Display(Name = "Postal Code")]
        public string PostCode { get; set; }

    }
}
