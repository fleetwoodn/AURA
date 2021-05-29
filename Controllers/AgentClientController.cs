using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Data;
using System.Net;
using System.Web;
using System.IO;
using Microsoft.AspNetCore.Http;
using AURA.Data;
using AURA.Models;
using AURA.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace AURA.Controllers
{
    [Authorize(Roles = "Admin,Sale,Ops")]
    public class AgentClientController : Controller
    {

        private readonly PostContext _context;

        public AgentClientController(PostContext context)
        {
            _context = context;
        }

        public IActionResult Index(string searchString)
        {
            //var titles = _context.PostEigs.OrderByDescending(p => p.EigAgen == User.Identity.Name).Take(100);

            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    titles = titles.Where(s => s.EigZero.Contains(searchString)
            //        || s.Contains(searchString)
            //        || s.OnePart.Contains(searchString)
            //        || s.OneTitl.Contains(searchString)
            //        );
            //}

            //return View(await _context.PostOnes.ToListAsync());
            //return View(await titles.ToListAsync());

            var startDate = DateTime.Now.Date.AddDays(-7);

            var jobs = from h in _context.PostThrs
                       join e in _context.PostEigs on h.ThrZero equals e.EigZero
                       where h.ThrDate > startDate && h.ThrText == "SERVICE DATE-"
                          && e.EigAgen == User.Identity.Name

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

            jobs.ToList();

            var viewModel = new AvailableJobListVM
            {
                JobList = jobs.ToList()
            };

            return View(viewModel);

        }

        public IActionResult AvailableJobIndex()
        {
            var today = DateTime.Now.Date;

            //AvailableJobListVM model = new List<AgentClientIndexVM>();
            //    model.JobList = from h in _context.PostThrs
            //   join e in _context.PostEigs on h.ThrZero equals e.EigZero
            //   where h.ThrDate > today && h.ThrText == "SERVICE DATE"
            //      && e.EigAgen == "OPEN"
            //   select new AgentClientIndexVM
            //   {
            //       Zero = h.ThrZero,
            //       ThrDate = h.ThrDate,
            //       ThrTime = h.ThrTime,
            //       ThrText = h.ThrText,
            //       EigAgen = e.EigAgen,
            //       EigRole = e.EigRole,
            //       EigLoad = e.EigLoad,
            //       EigNote = e.EigNote
            //   }.ToList();

           var jobs = from h in _context.PostThrs
           join e in _context.PostEigs on h.ThrZero equals e.EigZero
           where h.ThrDate > today && h.ThrText == "SERVICE DATE-"
              && e.EigAgen == "OPEN"
           
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

            jobs.ToList();

            var viewModel = new AvailableJobListVM
            {
                JobList = jobs.ToList()
            };

            //var viewModel = new AvailableJobListVM
            

            return View(viewModel);             
        }


        //clientside job request
        public IActionResult AssignJob(int? id, PostEig postEig)
        {

            var EigRecord = _context.PostEigs.Find(id);

            EigRecord.EigAgen = User.Identity.Name;

            _context.SaveChanges();

            return RedirectToAction("Index", "AgentClient");
        }

        //clientside job cancel
        public IActionResult ReleaseJob(int? id, PostEig postEig)
        {
            var EigRecord = _context.PostEigs.Find(id);

            EigRecord.EigAgen = "OPEN";

            _context.SaveChanges();


            return RedirectToAction("Index", "AgentClient");
        }


        //get: postindex

        public async Task<IActionResult> PostIndex(string searchString)
        {
            //var titles = from m in _context.PostOnes
            //             select m;

            //var titles = (from p in _context.PostOnes

            var titles = _context.PostOnes.OrderByDescending(p => p.OneId).Take(100);

            if (!String.IsNullOrEmpty(searchString))
            {
                titles = titles.Where(s => s.OneZero.Contains(searchString)
                    || s.OneStag.Contains(searchString)
                    || s.OnePart.Contains(searchString)
                    || s.OneTitl.Contains(searchString)
                    );
            }

            //return View(await _context.PostOnes.ToListAsync());
            return View(await titles.ToListAsync());

        }


        //get: post detail

        [ActionName("PostDetail")]
        public IActionResult PostDetail(string zero)
        {
            //ViewBag.OneId = id;
            //ViewBag.sevtotal = _context.PostSevs.Sum(q => q.SevAmou);
            ViewBag.sevtotal = _context.PostSevs.Where(q => q.SevZero == zero
                && q.SevHidd == "FALSE"
                ).Sum(c => c.SevAmou);

            if (zero == null)
            {
                return NotFound();
            }

            //PostOne postOne = _context.PostOnes.Find(id);
            PostOne postOne = _context.PostOnes.FirstOrDefault(m => m.OneZero == zero);

            var uThr = _context.PostThrs.Where(m => m.ThrZero == zero).ToList();
            var uFou = _context.PostFous.Where(m => m.FouZero == zero).ToList();
            var uFiv = _context.PostFivs.Where(m => m.FivZero == zero).ToList();
            var uSix = _context.PostSixs.Where(m => m.SixZero == zero).ToList();
            var uSev = _context.PostSevs.Where(m => m.SevZero == zero).ToList();
            var uEig = _context.PostEigs.Where(m => m.EigZero == zero).ToList();
            var uNin = _context.PostNins.Where(m => m.NinZero == zero).ToList();

            var viewModel = new PostDetailVM
            {
                OneId = postOne.OneId.ToString(),
                OneZero = postOne.OneZero,
                OneStag = postOne.OneStag,
                OneAgen = postOne.OneAgen,
                OnePart = postOne.OnePart,
                OneTitl = postOne.OneTitl,


                PostThrs = uThr,
                PostFous = uFou,
                PostFivs = uFiv,
                PostSixs = uSix,
                PostSevs = uSev,
                PostEigs = uEig,
                PostNins = uNin,

            };

            return View(viewModel);

        }

    }

    
            
       

}       

