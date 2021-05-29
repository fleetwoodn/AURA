using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AURA.Data;
using AURA.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AURA.Controllers
{
    [Authorize(Roles = "Admin,Sale")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> UserManager;
        private readonly PostContext Context;

        public AdminController(UserManager<IdentityUser> userManager)
        {
            UserManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Users()
        {
            var allUsers = UserManager.Users.ToList();
            List<UserDetailViewModel> userList = new List<UserDetailViewModel>();
            foreach (var userDetail in allUsers)
            {
                var userRole = UserManager.GetRolesAsync(userDetail).ConfigureAwait(true).GetAwaiter().GetResult();

                StringBuilder userRoles = new StringBuilder();
                foreach (var userrole in userRole.ToList())
                {
                    userRoles.Append(userrole);
                    userRoles.Append(",");
                }
                UserDetailViewModel user = new UserDetailViewModel()
                {
                    EmailAddres = userDetail.Email,
                    Role = Convert.ToString(userRoles).TrimEnd(','),
                    UserName = userDetail.UserName
                };
                userList.Add(user);
            }
            return View(userList);
        }
    }
}
