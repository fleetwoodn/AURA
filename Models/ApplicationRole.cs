using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AURA.Models
{
    public class ApplicationRole : IdentityRole
    {
        public string Discriminator { get; set; }
        
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
