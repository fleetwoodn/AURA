using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AURA.ViewModels
{
    public class ForgotPasswordViewModel
    {
        public string Email { get; set; }

        public string Message { get; set; }

        public bool Success { get; set; }
        public string Password { get; set; }
        public bool IsValidEmail { get; set; }
    }
}
