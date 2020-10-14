using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using AURA.Models;
using AURA.ViewModels;
using System.Data;
using System.Net;
using System.Web;
using System.IO;
using Microsoft.AspNetCore.Http;
using AURA.Data;
using Microsoft.EntityFrameworkCore;

namespace AURA.Controllers
{
    public class AvailabilityController : Controller
    {

        private readonly PostContext _context;

        public AvailabilityController(PostContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            DateTime mDate = DateTime.Now.Date;

            DateTime date0 = mDate;
            ViewBag.date0 = mDate.ToString("yy-MM-dd");

            DateTime date1 = mDate.AddDays(1);
            ViewBag.date1 = date1.ToString("yy-MM-dd");

            DateTime date2 = mDate.AddDays(2);
            ViewBag.date2 = date2.ToString("yy-MM-dd");

            DateTime date3 = mDate.AddDays(3);
            ViewBag.date3 = date3.ToString("yy-MM-dd");

            DateTime date4 = mDate.AddDays(4);
            ViewBag.date4 = date4.ToString("yy-MM-dd");

            DateTime date5 = mDate.AddDays(5);
            ViewBag.date5 = date5.ToString("yy-MM-dd");

            DateTime date6 = mDate.AddDays(6);
            ViewBag.date6 = date6.ToString("yy-MM-dd");

            DateTime date7 = mDate.AddDays(7);
            ViewBag.date7 = date7.ToString("yy-MM-dd");

            //var viewModel = new WeekCalendarVM
            //{
            //    Thr01 = _context
            //}

            var viewModel = new WeekCalendarVM
            {
                Thr00 = _context.PostThrs.Where(m => m.ThrDate.Date == date0),
                Thr01 = _context.PostThrs.Where(m => m.ThrDate.Date == date1),
                Thr02 = _context.PostThrs.Where(m => m.ThrDate.Date == date2),
                Thr03 = _context.PostThrs.Where(m => m.ThrDate.Date == date3),
                Thr04 = _context.PostThrs.Where(m => m.ThrDate.Date == date4),
                Thr05 = _context.PostThrs.Where(m => m.ThrDate.Date == date5),
                Thr06 = _context.PostThrs.Where(m => m.ThrDate.Date == date6),
                Thr07 = _context.PostThrs.Where(m => m.ThrDate.Date == date7),
            };


            return View(viewModel);
        }

        public IActionResult WeekCalendar()
        {

            return View();
        }
    }
}
