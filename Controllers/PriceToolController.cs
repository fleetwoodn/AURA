using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
using Microsoft.AspNetCore.Authorization;

using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AURA.Controllers
{
    public class PriceToolController : Controller
    {
        private readonly PostContext _context;
        public PriceToolController(PostContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        //mini apps
        //move pricing app

        public IActionResult Routing(string zero)
        {
            ViewBag.zero = zero;
            //change waypoints to js array, then loop array to submit

            return View();
        }

        public IActionResult JobAndInventory(string zero, int TravelTime, int TotalMiles, string NycTruck, string InterState, string Waypoints)
        {
            ViewBag.zero = zero;
            ViewBag.TravelTime = TravelTime;
            ViewBag.TotalMiles = TotalMiles;
            ViewBag.NycTruck = NycTruck;
            ViewBag.InterState = InterState;
            ViewBag.Waypoints = Waypoints;

            return View();
        }

        public IActionResult AvailabilityPricing(string zero, string dateString, decimal TravelTime, int TotalMiles, string NycTruck, string InterState, string Waypoints, int TeamCount, decimal LoadTime, string Drvr, string Hourly, int Stairs, int AddlInsured, string Items)
        {
            //viewbag
            ViewBag.zero = zero;
            ViewBag.TravelTime = TravelTime;
            ViewBag.TotalMiles = TotalMiles;
            ViewBag.NycTruck = NycTruck;
            ViewBag.InterState = InterState;
            ViewBag.Waypoints = Waypoints;
            ViewBag.TeamCount = TeamCount;
            ViewBag.LoadTime = LoadTime;
            ViewBag.Drvr = Drvr;
            ViewBag.Hourly = Hourly;
            ViewBag.Stairs = Stairs;
            ViewBag.AddlInsured = AddlInsured;
            ViewBag.Items = Items;
             

            //rate calculation factors
            //base rate = 36; travel cost is 65 + 2.48/mi; additional insured is 50 each
            //travel cost is 57base + 10toll +(mileage*2.48) + 10Tax
            //NycTruck is flat 135.00
            //InterState is +50
            //flat nyctrk = 135.00

            int BaseRate = 34;
            decimal TotalTimeMin = TravelTime + LoadTime;
            ViewBag.TotalTimeMin = TotalTimeMin;

            decimal TotalTimeDec = Convert.ToDecimal(TotalTimeMin);
            decimal TotalTimeHr = TotalTimeDec / 60;
            decimal TotalTime = Math.Round(TotalTimeHr, 0);
            

            if (TotalTime < 4)
            {
                TotalTime = 4;
            }
            ViewBag.TotalTime = TotalTime;

            //
            decimal TravelCost = (decimal)((TotalMiles * 2.35) + 65);
            if (NycTruck == "true")
            {
                TravelCost = 135;
            }
            if (InterState == "true")
            {
                TravelCost = TravelCost + 50;
            }
            string rateCalc = "";
            decimal TotalAddlInsured = AddlInsured * 50;

            //dates
            if (String.IsNullOrEmpty(dateString))
            {
                dateString = DateTime.Now.AddDays(3).ToString("yyyy-MM-dd");
                //ToString("yyyy-MM-dd");
            }

            DateTime DateToday = DateTime.Now;
            DateTime ServiceDate0 = System.DateTime.Parse(dateString).Date;
            ViewBag.ServiceDate0 = ServiceDate0.ToString("D");
            DateTime ServiceDateZ = ServiceDate0.AddDays(-1).Date;
            ViewBag.ServiceDateZ = ServiceDateZ.ToString("D");
            DateTime ServiceDate1 = ServiceDate0.AddDays(1).Date;
            ViewBag.ServiceDate1 = ServiceDate1.ToString("D");

            //ServiceDate0 ***
            //service date >=25th
            if (Convert.ToInt32(ServiceDate0.Day) >= 25)
            {
                BaseRate = BaseRate + 2;
                rateCalc = rateCalc + "a";
            }
            //service date <=5th
            if (Convert.ToInt32(ServiceDate0.Day) <= 5)
            {
                BaseRate = BaseRate + 2;
                rateCalc = rateCalc + "b";
            }
            //service date < 5 days out
            if ((ServiceDate0 - DateToday).TotalDays < 5)
            {
                BaseRate = BaseRate + 2;
                rateCalc = rateCalc + "c";
            }
            //service date < 3 days out
            if ((ServiceDate0 - DateToday).TotalDays < 3)
            {
                BaseRate = BaseRate + 2;
                rateCalc = rateCalc + "d";
            }
            //service date < 1 day out
            if ((ServiceDate0 - DateToday).TotalDays < 1)
            {
                BaseRate = BaseRate + 2;
                rateCalc = rateCalc + "e";
            }
            // Totaltime over 4 hours
            if ((TravelTime + LoadTime) > 240)
            {
                BaseRate = BaseRate + 1;
                rateCalc = rateCalc + "f";
            }
            //TotalTime over 3 hours
            if ((TravelTime + LoadTime) > 180)
            {
                BaseRate = BaseRate + 1;
                rateCalc = rateCalc + "g";
            }
            //flat
            if (Hourly == "true")
            {
                BaseRate = BaseRate + 1;
                rateCalc = rateCalc + "h";
            }
            //stairs
            if (Stairs <= 3)
            {
                BaseRate = BaseRate + 1;
                rateCalc = rateCalc + "i";
            }
            //stairs over 3
            if (Stairs > 3)
            {
                BaseRate = BaseRate + (Stairs / 2);
                rateCalc = rateCalc + "j";
            }

            //jersey
            if (InterState == "true")
            {
                BaseRate = BaseRate + 2;
                rateCalc = rateCalc + "k";
            }

            //FRI SAT not SUN
            var day0 = ServiceDate0.DayOfWeek.ToString();
            if (day0 == "Friday" || day0 == "Saturday")
            {
                BaseRate = BaseRate + 1;
                rateCalc = rateCalc + "l";
            }

            decimal BaseRate0 = BaseRate;
            decimal LaborRate0 = (TotalTime) * (BaseRate * TeamCount);
            decimal LaborRatePM0 = (TotalTime) * ((BaseRate - 4) * TeamCount);
            decimal TotalRate0 = LaborRate0 + TravelCost + TotalAddlInsured;
            decimal TotalRatePM0 = LaborRatePM0 + TravelCost + TotalAddlInsured;
            string rateCalc0 = rateCalc;


            //ServiceDateZ *******************************************
            //reset
            BaseRate = 36;
            rateCalc = "";
            //service date >=25th
            if (Convert.ToInt32(ServiceDateZ.Day) >= 25)
            {
                BaseRate = BaseRate + 2;
                rateCalc = rateCalc + "a";
            }
            //service date <=5th
            if (Convert.ToInt32(ServiceDateZ.Day) <= 5)
            {
                BaseRate = BaseRate + 2;
                rateCalc = rateCalc + "b";
            }
            //service date < 5 days out
            if ((ServiceDateZ - DateToday).TotalDays < 5)
            {
                BaseRate = BaseRate + 2;
                rateCalc = rateCalc + "c";
            }
            //service date < 3 days out
            if ((ServiceDateZ - DateToday).TotalDays < 3)
            {
                BaseRate = BaseRate + 2;
                rateCalc = rateCalc + "d";
            }
            //service date < 1 day out
            if ((ServiceDateZ - DateToday).TotalDays < 1)
            {
                BaseRate = BaseRate + 2;
                rateCalc = rateCalc + "e";
            }
            // Totaltime over 4 hours
            if ((TravelTime + LoadTime) > 240)
            {
                BaseRate = BaseRate + 1;
                rateCalc = rateCalc + "f";
            }
            //TotalTime over 3 hours
            if ((TravelTime + LoadTime) > 180)
            {
                BaseRate = BaseRate + 1;
                rateCalc = rateCalc + "g";
            }
            //flat
            if (Hourly == "true")
            {
                BaseRate = BaseRate + 1;
                rateCalc = rateCalc + "h";
            }
            //stairs
            if (Stairs <= 3)
            {
                BaseRate = BaseRate + 1;
                rateCalc = rateCalc + "i";
            }
            //stairs over 3
            if (Stairs > 3)
            {
                BaseRate = BaseRate + (Stairs / 2);
                rateCalc = rateCalc + "j";
            }

            //jersey
            if (InterState == "true")
            {
                BaseRate = BaseRate + 2;
                rateCalc = rateCalc + "k";
            }

            //FRI SAT not SUN
            var dayZ = ServiceDate0.DayOfWeek.ToString();
            if (dayZ == "Friday" || day0 == "Saturday")
            {
                BaseRate = BaseRate + 1;
                rateCalc = rateCalc + "l";
            }

            //re-calc everything
            decimal BaseRateZ = BaseRate;
            decimal LaborRateZ = (TotalTime) * (BaseRate * TeamCount);
            decimal LaborRatePMZ = (TotalTime) * ((BaseRate - 4) * TeamCount);
            decimal TotalRateZ = LaborRateZ + TravelCost + TotalAddlInsured;
            decimal TotalRatePMZ = LaborRatePMZ + TravelCost + TotalAddlInsured;
            string rateCalcZ = rateCalc;

            //ServiceDate1 *******************************************
            //reset
            BaseRate = 36;
            rateCalc = "";
            //service date >=25th
            if (Convert.ToInt32(ServiceDate1.Day) >= 25)
            {
                BaseRate = BaseRate + 2;
                rateCalc = rateCalc + "a";
            }
            //service date <=5th
            if (Convert.ToInt32(ServiceDate1.Day) <= 5)
            {
                BaseRate = BaseRate + 2;
                rateCalc = rateCalc + "b";
            }
            //service date < 5 days out
            if ((ServiceDate1 - DateToday).TotalDays < 5)
            {
                BaseRate = BaseRate + 2;
                rateCalc = rateCalc + "c";
            }
            //service date < 3 days out
            if ((ServiceDate1 - DateToday).TotalDays < 3)
            {
                BaseRate = BaseRate + 2;
                rateCalc = rateCalc + "d";
            }
            //service date < 1 day out
            if ((ServiceDate1 - DateToday).TotalDays < 1)
            {
                BaseRate = BaseRate + 2;
                rateCalc = rateCalc + "e";
            }
            // Totaltime over 4 hours
            if ((TravelTime + LoadTime) > 240)
            {
                BaseRate = BaseRate + 1;
                rateCalc = rateCalc + "f";
            }
            //TotalTime over 3 hours
            if ((TravelTime + LoadTime) > 180)
            {
                BaseRate = BaseRate + 1;
                rateCalc = rateCalc + "g";
            }
            //flat
            if (Hourly == "true")
            {
                BaseRate = BaseRate + 1;
                rateCalc = rateCalc + "h";
            }
            //stairs
            if (Stairs <= 3)
            {
                BaseRate = BaseRate + 1;
                rateCalc = rateCalc + "i";
            }
            //stairs over 3
            if (Stairs > 3)
            {
                BaseRate = BaseRate + (Stairs / 2);
                rateCalc = rateCalc + "j";
            }

            //jersey
            if (InterState == "true")
            {
                BaseRate = BaseRate + 2;
                rateCalc = rateCalc + "k";
            }

            //FRI SAT not SUN
            var day1 = ServiceDate0.DayOfWeek.ToString();
            if (day1 == "Friday" || day0 == "Saturday")
            {
                BaseRate = BaseRate + 1;
                rateCalc = rateCalc + "l";
            }

            //re-calc everything

            decimal BaseRate1 = BaseRate;
            decimal LaborRate1 = (TotalTime) * (BaseRate * TeamCount);
            decimal LaborRatePM1 = (TotalTime) * ((BaseRate - 4) * TeamCount);
            decimal TotalRate1 = LaborRate1 + TravelCost + TotalAddlInsured;
            decimal TotalRatePM1 = LaborRatePM1 + TravelCost + TotalAddlInsured;
            string rateCalc1 = rateCalc;

            //NO MORE DATES!!!

            //the viewbag calcs***
            //z
            ViewBag.BaseRateZ = BaseRateZ;
            ViewBag.LaborRateZ = LaborRateZ;
            ViewBag.LaborRatePMZ = LaborRatePMZ;
            ViewBag.TotalRateZ = TotalRateZ;
            ViewBag.TotalRatePMZ = TotalRatePMZ;
            ViewBag.RateCalcZ = rateCalcZ;
            //0
            ViewBag.BaseRate0 = BaseRate0;
            ViewBag.LaborRate0 = LaborRate0;
            ViewBag.LaborRatePM0 = LaborRatePM0;
            ViewBag.TotalRate0 = TotalRate0;
            ViewBag.TotalRatePM0 = TotalRatePM0;
            ViewBag.RateCalc0 = rateCalc0;
            //1
            ViewBag.BaseRate1 = BaseRate1;
            ViewBag.LaborRate1 = LaborRate1;
            ViewBag.LaborRatePM1 = LaborRatePM1;
            ViewBag.TotalRate1 = TotalRate1;
            ViewBag.TotalRatePM1 = TotalRatePM1;
            ViewBag.RateCalc1 = rateCalc1;
            //everything else
            ViewBag.TravelCost = TravelCost;
            ViewBag.TotalAddlInsured = TotalAddlInsured;

            //viewbags for booking
            ViewBag.PT = TravelTime + "-" + TotalMiles + "-" + NycTruck + "-" + InterState + "-" + TeamCount + 
                "-" + LoadTime + "-" + Drvr + "-" + Hourly + "-" + Stairs + "-" + AddlInsured;
            ViewBag.PTW = Waypoints;
            ViewBag.PTM = Items;
            ViewBag.PTP0 = "F" + TotalRate0 + " -B" + BaseRate0 + " -L" + LaborRate0 + " -T" + TravelCost + " -I" + TotalAddlInsured;
            ViewBag.PTP0P = "F" + TotalRate0 + " -B" + BaseRate0 + " -L" + LaborRatePM0 + " -T" + TravelCost + " -I" + TotalAddlInsured;

            //viewmodel

            var viewModel = new PriceToolAvailabilityVM
            {
                ThrZ = _context.PostThrs.Where(m => m.ThrDate == ServiceDateZ),
                Thr0 = _context.PostThrs.Where(m => m.ThrDate == ServiceDate0),
                Thr1 = _context.PostThrs.Where(m => m.ThrDate == ServiceDate1),
            };

            return View(viewModel);
        }

        //book
        public IActionResult PriceToolBook(string zero)
        {
            return View();
        }

        [HttpPost]
        public IActionResult PriceToolBook(string zero, PostZero postZero, PostOne postOne, PostEig postEig, string PT, string PTP, string PTW,
            string PTM, string ServiceDate, int ServiceTime, int TotalTime )
        {
            var zero1 = zero;
            //add postzero and postone first if needed
            if (zero == null)
            {
                postZero.ZeroDate = DateTime.Now;

                string uDate = DateTime.Now.ToString("yyMMdd");
                string uDigit = _context.PostZeros.Count(d => d.ZeroDate.Date == postZero.ZeroDate.Date).ToString(); //awesome
                postZero.Zero = uDate + "-" + uDigit;

                postZero.ZeroAgen = User.Identity.Name;

                zero = postZero.Zero;

                _context.Add(postZero);
                _context.SaveChanges();

                //postone
                //postOne.OneId = "";
                postOne.OneZero = postZero.Zero;
                postOne.OneStag = "TRCK";
                postOne.OneAgen = User.Identity.Name;
                postOne.OnePart = "0001112222";
                postOne.OneTitl = "T:--'CUST HERE'--'DEP ZIP CODE HERE'--" + DateTime.Now.ToShortDateString();

                _context.Add(postOne);
                _context.SaveChanges();

                //posteig
                postEig.EigZero = postZero.Zero;
                postEig.EigDigit = 1;
                postEig.EigAgen = User.Identity.Name;
                postEig.EigRole = "SALE";
                postEig.EigLoad = 0;

                _context.Add(postEig);
                _context.SaveChanges();

            }


            //postthr for service date
            PostThr iPostThr = new PostThr();
            iPostThr.ThrZero = zero;
            if (!(_context.PostThrs.Count(n => n.ThrZero == iPostThr.ThrZero) > 0))
            {
                iPostThr.ThrDigit = 1;
            }
            else
            {
                iPostThr.ThrDigit = _context.PostThrs.Where(m => m.ThrZero == iPostThr.ThrZero).Max(x => x.ThrDigit) + 1;
            }
            DateTime qServiceDate = DateTime.Parse(ServiceDate);
            iPostThr.ThrDate = qServiceDate;
            iPostThr.ThrTime =  ServiceTime.ToString() + ":00";
            iPostThr.ThrEndTime = (ServiceTime + TotalTime + 1).ToString() + ":00";
            iPostThr.ThrText = "PLAN DATE-";
            _context.Add(iPostThr);
            _context.SaveChanges();

            //postthr for dpst
            PostThr jPostThr = new PostThr();
            jPostThr.ThrZero = zero;
            if (!(_context.PostThrs.Count(n => n.ThrZero == jPostThr.ThrZero) > 0))
            {
                jPostThr.ThrDigit = 1;
            }
            else
            {
                jPostThr.ThrDigit = _context.PostThrs.Where(m => m.ThrZero == jPostThr.ThrZero).Max(x => x.ThrDigit) + 1;
            }
            jPostThr.ThrDate = DateTime.Now.AddDays(1);
            jPostThr.ThrTime = "12:00";
            jPostThr.ThrEndTime = "";
            jPostThr.ThrText = "DEPOSIT DUE-";
            _context.Add(jPostThr);
            _context.SaveChanges();

            //postfiv for notations
            PostFiv iPostFiv = new PostFiv();
            if (!(_context.PostFivs.Count(n => n.FivZero == iPostFiv.FivZero) > 0))
            {
                iPostFiv.FivDigit = 1;
            }
            else
            {
                iPostFiv.FivDigit = _context.PostFivs.Where(m => m.FivZero == iPostFiv.FivZero).Max(x => x.FivDigit) + 1;
            }
            iPostFiv.FivZero = zero;
            iPostFiv.FivPrio = "9";
            iPostFiv.FivCode = "J";
            iPostFiv.FivText = "PT--"+ PT; //full pt values together except for manifest, waypoints and pricing
            _context.Add(iPostFiv);
            _context.SaveChanges();

            //postfiv for pricing
            PostFiv kPostFiv = new PostFiv();
            if (!(_context.PostFivs.Count(n => n.FivZero == kPostFiv.FivZero) > 0))
            {
                kPostFiv.FivDigit = 1;
            }
            else
            {
                kPostFiv.FivDigit = _context.PostFivs.Where(m => m.FivZero == kPostFiv.FivZero).Max(x => x.FivDigit) + 1;
            }
            kPostFiv.FivZero = zero;
            kPostFiv.FivPrio = "9";
            kPostFiv.FivCode = "J";
            kPostFiv.FivText = "PTP--"+ PTP; //pricing plus service date
            _context.Add(kPostFiv);
            _context.SaveChanges();

            //postfiv for waypoint
            PostFiv lPostFiv = new PostFiv();;
            if (!(_context.PostFivs.Count(n => n.FivZero == lPostFiv.FivZero) > 0))
            {
                lPostFiv.FivDigit = 1;
            }
            else
            {
                lPostFiv.FivDigit = _context.PostFivs.Where(m => m.FivZero == lPostFiv.FivZero).Max(x => x.FivDigit) + 1;
            }
            lPostFiv.FivZero = zero;
            lPostFiv.FivPrio = "9";
            lPostFiv.FivCode = "J";
            lPostFiv.FivText = "PTW--"+ PTW; //waypoints
            _context.Add(lPostFiv);
            _context.SaveChanges();

            //postfiv for manifest
            PostFiv jPostFiv = new PostFiv();
            if (!(_context.PostFivs.Count(n => n.FivZero == jPostFiv.FivZero) > 0))
            {
                jPostFiv.FivDigit = 1;
            }
            else
            {
                jPostFiv.FivDigit = _context.PostFivs.Where(m => m.FivZero == jPostFiv.FivZero).Max(x => x.FivDigit) + 1;
            }
            jPostFiv.FivZero = zero;
            jPostFiv.FivPrio = "9";
            jPostFiv.FivCode = "M";
            jPostFiv.FivText = "PTM--"+ PTM; //items
            _context.Add(jPostFiv);
            _context.SaveChanges();

            //if zero1 is empty, redirect to oneedit
            if (zero1 == null)
            {
                return RedirectToAction("OneEdit", "MSR", new { id = postOne.OneId, zero });
            }
            //else redirect to detail
            return RedirectToAction("MSRDetail", "MSR", new { id = postOne.OneId, zero });
            //return RedirectToAction("OneEdit", "MSR", new { id = postOne.OneId, zero });

        }

        //manifest builder
        public IActionResult ManifestBuilder(string zero)
        {

            ViewBag.zero = zero;
            if (zero is null)
            {
                ViewBag.zero = "000000-0";
            }

            return View();
        }

        //new tool version
        public IActionResult Manifest(string zero)
        {
            ViewBag.zero = zero;

            var boxsCount = _context.Manifests.Count(m => m.Zero == zero
                && m.Type == "B");
            ViewBag.boxCount = boxsCount;

            var contCount = _context.Manifests.Count(m => m.Zero == zero
                && m.Type == "C");            
            ViewBag.contCount = contCount;

            ViewBag.ItemCount = _context.Manifests.Count(m => m.Zero == zero
                && m.Type == "I");

            ViewBag.FragCount = _context.Manifests.Count(m => m.Zero == zero
                && m.Type == "F");

            ViewBag.LargCount = _context.Manifests.Count(m => m.Zero == zero
                && m.Type == "L");

            ViewBag.SmalCount = _context.Manifests.Count(m => m.Zero == zero
                && m.Type == "S");



            var viewModel = new ManifestVM
            {
                ManifestsI = _context.Manifests.Where(m => m.Zero == zero
                && m.Type == "I"
                ).ToList(),
                ManifestsF = _context.Manifests.Where(m => m.Zero == zero
                && m.Type == "F"
                ).ToList(),
                ManifestsL = _context.Manifests.Where(m => m.Zero == zero
                && m.Type == "L"
                ).ToList(),
                ManifestsS = _context.Manifests.Where(m => m.Zero == zero
                && m.Type == "S"
                ).ToList(),
                ManifestsO = _context.Manifests.Where(m => m.Zero == zero
                && m.Type == "O"
                ).ToList()

            };

            return View(viewModel);
        }

        //manifest update box
        //post
        [HttpPost]
        public IActionResult AdjustBoxs(Manifest manifest, int BoxsCount, string zero)
        {

            if(zero is null)
            {
                zero = "000000-0";
            }

            //manifest.Type;
            var OldBoxsCount = _context.Manifests.Where(m => m.Type == "B").ToArray();

            _context.Manifests.RemoveRange(OldBoxsCount);
            _context.SaveChanges();

            //
            List<Manifest> NewBoxsCount = new List<Manifest>();

            for (int i = 1; i <= BoxsCount; i++)
            {
                NewBoxsCount.Add(new Manifest { Zero = zero, Type = "B", Name = "Box" });
            }

            _context.Manifests.AddRange(NewBoxsCount);
            _context.SaveChanges();

            return RedirectToAction("Manifest", "PriceTool", new { zero });
            
        }

        //cont
        [HttpPost]
        public IActionResult AdjustCont(Manifest manifest, int ContCount, string zero)
        {
            if (zero is null)
            {
                zero = "000000-0";
            }

            //manifest.Type;
            var OldContCount = _context.Manifests.Where(m => m.Type == "C").ToArray();

            _context.Manifests.RemoveRange(OldContCount);
            _context.SaveChanges();

            //
            List<Manifest> NewContCount = new List<Manifest>();

            for (int i = 1; i <= ContCount; i++)
            {
                NewContCount.Add(new Manifest { Zero = zero, Type = "C", Name = "Container" });
            }

            _context.Manifests.AddRange(NewContCount);
            _context.SaveChanges();

            return RedirectToAction("Manifest", "PriceTool", new { zero });

        }

        //items
        [HttpPost]
        public IActionResult AddObject(Manifest manifest, string zero, string type, string NameSelect, string NameWrite, string Wrap, string Assembly )
        {
            if (zero is null)
            {
                zero = "000000-0";
            }

            manifest.Zero = zero;
            manifest.Type = type;
            manifest.Name = NameWrite;
            if(NameWrite == "")
            {
                manifest.Name = NameSelect;
            }
            manifest.Wrap = Wrap;
            manifest.Assembly = Assembly;

            _context.Add(manifest);
            _context.SaveChanges();

            return RedirectToAction("Manifest", "PriceTool", new { zero });
        }

        public IActionResult DeleteObject()
        {
            return View();
        }

        //delete items
        [HttpPost]
        public IActionResult DeleteObject(int id, string zero)
        {
            if (zero is null)
            {
                zero = "000000-0";
            }

            var manifest = _context.Manifests.Find(id);
            _context.Manifests.Remove(manifest);
            _context.SaveChanges();


            return RedirectToAction("Manifest", "PriceTool", new { zero });
            //return RedirectToAction(nameof(Routing));
        }

        //van jobs


    }
}
