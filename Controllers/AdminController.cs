using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Data;
using System.Net;
using System.Web;
using System.IO;
using Microsoft.AspNetCore.Http;
using AURA.Data;
using AURA.Models;
using AURA.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Text;

//using System.Web.UI;
//using System.Web.UI.WebControls;
using SelectPdf;


namespace AURA.Controllers
{
    [Authorize]

    

    public class AdminController : Controller
    {


        private readonly PostContext _context;
        public AdminController(PostContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SqlPage()
        {
            
            return Content("Hello World !");
        }
    }
}
