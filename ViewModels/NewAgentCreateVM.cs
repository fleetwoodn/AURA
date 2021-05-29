using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AURA.ViewModels
{
    
    public class NewAgentCreateVM
    {
        //basic
        [StringLength(60, MinimumLength = 2)]
        [Required]
        [Display(Name = "Legal First Name")]
        public string FirstName { get; set; }

        [StringLength(60, MinimumLength = 2)]
        [Required]
        [Display(Name = "Full Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Nickname")]
        public string NickName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Birthdate")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Tax Id")]
        public string TaxId { get; set; }

        //phone

        [Display(Name = "Phone Number")]
        [Required]
        public string PhoneNumber { get; set; }

        [Display(Name = "Country Code")]
        public string CountryCode { get; set; }

        [Display(Name = "Carrier Name")]
        public string CarrierName { get; set; }

        //email

        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        //address

        [StringLength(50, MinimumLength = 2)]
        [Required]
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }

        [StringLength(20, MinimumLength = 4)]
        [Required]
        [Display(Name = "Postal Code")]
        public string PostCode { get; set; }

        //PAYMENT
        [Display(Name = "Payment Type")]
        public string PaymentType { get; set; }

        [Display(Name = "Payment Detail")]
        public string PaymentDetail { get; set; }

        //esign
        public string ESignature { get; set; }
    }
}

