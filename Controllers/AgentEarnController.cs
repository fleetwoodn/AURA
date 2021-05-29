using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AURA.Data;
using AURA.Models;

using System.Data;
using System.Net;
using System.Web;
using System.IO;
using Microsoft.AspNetCore.Http;
using AURA.ViewModels;

using Microsoft.AspNetCore.Authorization;

namespace AURA.Controllers
{
    public class AgentEarnController : Controller
    {
        private readonly PostContext _context;

        public AgentEarnController(PostContext context)
        {
            _context = context;
        }

        //list of agents earn given a specified date/week
        public IActionResult Index(string DateString, string NameString)
        {
            

            if (String.IsNullOrEmpty(DateString))
            {
                //DateTime Today = DateTime.Today;
                //int DaysSinceMonday = ((int)DayOfWeek.Monday - (int)Today.DayOfWeek + 7) % 7;

                //DateString = Today.AddDays(DaysSinceMonday).ToShortDateString();

                DateString = DateTime.Today.AddDays(-7).ToShortDateString();
            }

            if(String.IsNullOrEmpty(NameString))
            {
                
            }
            

            ViewBag.datestring = DateString;

            DateTime StartDate = DateTime.Parse(DateString);
            DateTime EndDate = StartDate.AddDays(7);

            var jobs = from h in _context.PostThrs
                       join e in _context.PostEigs on h.ThrZero equals e.EigZero
                       where h.ThrDate > StartDate
                       && h.ThrDate < EndDate
                       && h.ThrText == "SERVICE DATE-"
                       && e.EigAgen.StartsWith(NameString)

                       select new AgentClientIndexVM
                       {
                           Zero = h.ThrZero,
                           ThrDate = h.ThrDate.Date,
                           ThrTime = h.ThrTime,
                           ThrText = h.ThrText,
                           EigId = e.EigId,
                           EigAgen = e.EigAgen,
                           EigRole = e.EigRole,
                           EigLoad = e.EigLoad,
                           EigNote = e.EigNote
                       };

            if (String.IsNullOrEmpty(NameString))
            {
                    jobs = from h in _context.PostThrs
                           join e in _context.PostEigs on h.ThrZero equals e.EigZero
                           where h.ThrDate > StartDate
                           && h.ThrDate < EndDate
                           && h.ThrText == "SERVICE DATE-"
                           //&& e.EigAgen.Contains(@NameString)

                           select new AgentClientIndexVM
                           {
                               Zero = h.ThrZero,
                               ThrDate = h.ThrDate.Date,
                               ThrTime = h.ThrTime,
                               ThrText = h.ThrText,
                               EigId = e.EigId,
                               EigAgen = e.EigAgen,
                               EigRole = e.EigRole,
                               EigLoad = e.EigLoad,
                               EigNote = e.EigNote
                           };
            }
            
            jobs.ToList();

            

            var viewModel = new AvailableJobListVM
            {
                JobList = jobs.ToList()
            };

            return View(viewModel);
        }



    }
}
