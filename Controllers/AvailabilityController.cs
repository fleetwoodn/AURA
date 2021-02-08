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

        public IActionResult Index(string DateString)
        {

            


            if (String.IsNullOrEmpty(DateString))
            {
                DateString = DateTime.Now.AddDays(3).ToString("yyyy-MM-dd");
                //ToString("yyyy-MM-dd");
            }

            DateTime mDate = System.DateTime.Parse(DateString);

            //DateTime mDate = DateTime.Now.Date;

            DateTime dateX = mDate.AddDays(-3);
            ViewBag.dateX = dateX.ToString("D");
            var XCount = _context.PostThrs.Count(m => m.ThrText == "SERVICE DATE"
                && m.ThrDate == dateX);
            
            if (XCount > 0)
            {
                ViewBag.XPrice = 38;
            }


            DateTime dateY = mDate.AddDays(-2);
            ViewBag.dateY = dateY.ToString("D");

            DateTime dateZ = mDate.AddDays(-1);
            ViewBag.dateZ = dateZ.ToString("D");

            DateTime date0 = mDate;
            ViewBag.date0 = mDate.ToString("D");

            DateTime date1 = mDate.AddDays(1);
            ViewBag.date1 = date1.ToString("D");

            DateTime date2 = mDate.AddDays(2);
            ViewBag.date2 = date2.ToString("D");

            DateTime date3 = mDate.AddDays(3);
            ViewBag.date3 = date3.ToString("D");

            DateTime date4 = mDate.AddDays(4);
            ViewBag.date4 = date4.ToString("D");

            DateTime date5 = mDate.AddDays(5);
            ViewBag.date5 = date5.ToString("D");

            DateTime date6 = mDate.AddDays(6);
            ViewBag.date6 = date6.ToString("D");

            DateTime date7 = mDate.AddDays(7);
            ViewBag.date7 = date7.ToString("D");

            DateTime date8 = mDate.AddDays(8);
            ViewBag.date8 = date8.ToString("D");

            DateTime date9 = mDate.AddDays(9);
            ViewBag.date9 = date9.ToString("D");

            DateTime date10 = mDate.AddDays(10);
            ViewBag.date10 = date10.ToString("D");

            DateTime date11 = mDate.AddDays(11);
            ViewBag.date11 = date11.ToString("D");

            DateTime date12 = mDate.AddDays(12);
            ViewBag.date12 = date12.ToString("D");

            DateTime date13 = mDate.AddDays(13);
            ViewBag.date13 = date13.ToString("D");

            DateTime date14 = mDate.AddDays(14);
            ViewBag.date14 = date14.ToString("D");

            var viewModel = new WeekCalendarVM
            {
                Thr0X = _context.PostThrs.Where(m => m.ThrDate.Date == dateX),
                Thr0Y = _context.PostThrs.Where(m => m.ThrDate.Date == dateY),
                Thr0Z = _context.PostThrs.Where(m => m.ThrDate.Date == dateZ),
                Thr00 = _context.PostThrs.Where(m => m.ThrDate.Date == date0),
                Thr01 = _context.PostThrs.Where(m => m.ThrDate.Date == date1),
                Thr02 = _context.PostThrs.Where(m => m.ThrDate.Date == date2),
                Thr03 = _context.PostThrs.Where(m => m.ThrDate.Date == date3),
                Thr04 = _context.PostThrs.Where(m => m.ThrDate.Date == date4),
                Thr05 = _context.PostThrs.Where(m => m.ThrDate.Date == date5),
                Thr06 = _context.PostThrs.Where(m => m.ThrDate.Date == date6),
                Thr07 = _context.PostThrs.Where(m => m.ThrDate.Date == date7),
                Thr08 = _context.PostThrs.Where(m => m.ThrDate.Date == date8),
                Thr09 = _context.PostThrs.Where(m => m.ThrDate.Date == date9),
                Thr10 = _context.PostThrs.Where(m => m.ThrDate.Date == date10),
                Thr11 = _context.PostThrs.Where(m => m.ThrDate.Date == date11),
                Thr12 = _context.PostThrs.Where(m => m.ThrDate.Date == date12),
                Thr13 = _context.PostThrs.Where(m => m.ThrDate.Date == date13),
                Thr14 = _context.PostThrs.Where(m => m.ThrDate.Date == date14),
            };


            return View(viewModel);
        }

        public IActionResult WeekCalendar()
        {

            return View();
        }
    }
}
