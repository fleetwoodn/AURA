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

    //[Authorize(Roles = "NJUN,SALE")]
    [Authorize]
    public class PostController : Controller
    {
        private readonly PostContext _context;
        public PostController(PostContext context)
        {
            _context = context;
        }

        
        public IActionResult Index()
        {
            return View();
        }

        //get: postindex
        public async Task<IActionResult> PostIndex(string searchString)
        {
            var titles = from m in _context.PostOnes
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                titles = titles.Where(s => s.OneZero.Contains(searchString)
                    ||s.OneStag.Contains(searchString)
                    ||s.OnePart.Contains(searchString)
                    ||s.OneTitl.Contains(searchString)
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
        

        /// <summary>
        /// Downloads the post one comma seperated file.
        /// </summary>
        /// <returns></returns>
        public IActionResult DownloadPostDetailCommaSeperatedFile(string zero)
        {
            var qTotal = _context.PostSevs.Where(q => q.SevZero == zero && q.SevHidd == "FALSE").Sum(c => c.SevAmou);

            try
            {
                var postOnes = _context.PostOnes.Where(m => m.OneZero == zero).ToList();
                //var postTwos = _context.PostTwos.Where(m => m.TwoZero == zero).ToList();
                var postThrs = _context.PostThrs.Where(m => m.ThrZero == zero).ToList();
                var postFous = _context.PostFous.Where(m => m.FouZero == zero).ToList();
                var postFivs = _context.PostFivs.Where(m => m.FivZero == zero).ToList();
                    //&& m.FivCode.Contains("I")
                    //).ToList();
                var postSixs = _context.PostSixs.Where(m => m.SixZero == zero).ToList();
                var postSevs = _context.PostSevs.Where(m => m.SevZero == zero).ToList();
                var postEigs = _context.PostEigs.Where(m => m.EigZero == zero).ToList();
                var postNins = _context.PostNins.Where(m => m.NinZero == zero).ToList();

                StringBuilder stringBuilder = new StringBuilder();
                StringBuilder exportData = stringBuilder;

                stringBuilder.AppendLine("RYNE MOVING");
                stringBuilder.AppendLine("212-787-2636");
                stringBuilder.AppendLine("INFO@RYNE.CO");
                stringBuilder.AppendLine("108 W 81ST STREET NEW YORK NY 10024");
                stringBuilder.AppendLine("");

                stringBuilder.AppendLine("SUMMARY");
                foreach (var author in postOnes)
                {
                    stringBuilder.AppendLine($"JOB ID: {author.OneZero},{author.OneStag}");
                    stringBuilder.AppendLine($"JOB TITLE: {author.OneTitl}");
                    stringBuilder.AppendLine($"LEAD AGENT: {author.OneAgen}");
                    stringBuilder.AppendLine($"CUSTOMER ID: {author.OnePart}");
                    
                }
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine("TIMEFRAMES");
                foreach (var author in postThrs)
                {
                    stringBuilder.AppendLine($"{author.ThrDigit}.,{author.ThrDate},{author.ThrText}");
                    
                }
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine("CONTACTS");
                foreach (var author in postFous)
                {
                    stringBuilder.AppendLine($"{author.FouDigit}.,{author.FouName},,{author.FouPhon},,{author.FouEmai}");
                    stringBuilder.AppendLine($",{author.FouAddr},{author.FouPost},{author.FouOrg},{author.FouNote}");
                    
                }
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine("REMARKS");
                foreach (var author in postFivs)
                {
                    stringBuilder.AppendLine($"{author.FivDigit}.,{author.FivPrio}-{author.FivCode},{author.FivText}");
                    //stringBuilder.AppendLine($"{author.FivDigit}    {author.FivPrio}    {author.FivCode}    {author.FivText}");
                    
                }
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"BILLING");
                stringBuilder.AppendLine($"TOTAL DUE:" + qTotal);
                foreach (var author in postSevs)
                {
                    stringBuilder.AppendLine($"{author.SevDigit}.,{author.SevDate.Date},{author.SevDesc},{author.SevAmou}");
                    
                }
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine("AGENTS");
                foreach (var author in postEigs)
                {
                    stringBuilder.AppendLine($"{author.EigDigit}.,{author.EigAgen},{author.EigRole},{author.EigLoad},{author.EigNote}");
                    
                }
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine("ATTACHMENTS");
                foreach (var author in postNins)
                {
                    stringBuilder.AppendLine($"{author.NinDigit}.,{author.NinFile},{author.NinCapt},{author.NinNote}");
                }

                

                return File(Encoding.UTF8.GetBytes
                (stringBuilder.ToString()), "text/csv", "PostDetail"+ zero + "-" + DateTime.Now.ToString("yyyy-MM-dd-hhmm") +  ".csv");
            }
            catch
            {
                return RedirectToAction(nameof(PostIndex));
            }
        }

        protected void CreateSelectPdf(object sender, EventArgs e, string zero)
        //public IActionResult CreateSelectPdf(string zero)
        {
            try
            {
                SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();
                SelectPdf.PdfDocument doc = converter.ConvertUrl("https://selectpdf.com");
                doc.Save("test.pdf");

                //return File(doc);

                doc.Close();
                
            }
            catch
            {
                RedirectToAction(nameof(PostIndex));
            }
        }

        [AllowAnonymous]
        public IActionResult PrintPDF(string zero)
        {

            var paym = _context.PostSixs.Where(q => q.SixZero == zero).Sum(c => c.SixAmou);

            var bill = _context.PostSevs.Where(q => q.SevZero == zero
                && q.SevHidd != "TRUE"
                && q.SevPaym != "TRUE"
                ).Sum(c => c.SevAmou);

            var total = -paym + bill;

            ViewBag.paym = paym;
            ViewBag.bill = bill;
            ViewBag.total = total;

            ////ViewBag.sevtotal = _context.PostSevs.Where(q => q.SevZero == zero
            ////    && q.SevHidd == "FALSE"
            ////    ).Sum(c => c.SevAmou);

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

            var uSev = _context.PostSevs.Where(m => m.SevZero == zero
                && m.SevHidd != "TRUE"
                && m.SevPaym != "TRUE").ToList();
            
            var uEig = _context.PostEigs.Where(m => m.EigZero == zero).ToList();
            var uNin = _context.PostNins.Where(m => m.NinZero == zero).ToList();

            var viewModel = new PostPDFVM
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

        

        //post zero****************************************************************************************************************************************

        // GET: PostZeroes/Create
        public IActionResult ZeroCreate()
        {
            return View();
        }

        // POST: PostZeroes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ZeroCreate([Bind("Zero,ZeroDate,ZeroAgen")] PostZero postZero)
        {


            postZero.ZeroDate = DateTime.Now;

            string uDate = DateTime.Now.ToString("yyMMdd");
            string uDigit = _context.PostZeros.Count(d => d.ZeroDate == postZero.ZeroDate).ToString();
            postZero.Zero = uDate + "-" + uDigit;

            ///postOne.OneAgent = User.Identity.Name; //we will apply this once the authentication is complete
            //postZero.ZeroAgen = "njn-1";
            postZero.ZeroAgen = User.Identity.Name;

            if (ModelState.IsValid)
            {
                _context.Add(postZero);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(OneCreate), new { zero = postZero.Zero });
            }
            return View(postZero);
        }

        //post one****************************************************************************************************************************************

        // GET: PostOnes/Create
        public IActionResult OneCreate(string zero)
        {
           
            return View();
        }

        // POST: PostOnes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OneCreate([Bind("OneId,OneZero,OneStag,OneAgen,OnePart,OneTitl")] PostOne postOne, string zero, PostZero postZero)
        {
            //postOne.OneZero = zero;

            if (ModelState.IsValid)
            {
                //add post zero first
                postZero.ZeroDate = DateTime.Now;

                string uDate = DateTime.Now.ToString("yyMMdd");
                string uDigit = _context.PostZeros.Count(d => d.ZeroDate.Date == postZero.ZeroDate.Date).ToString(); //awesome
                postZero.Zero = uDate + "-" + uDigit;



                postZero.ZeroAgen = User.Identity.Name; //we will apply this once the authentication is complete
                //postZero.ZeroAgen = "njn-1";

                postOne.OneZero = postZero.Zero;

                _context.Add(postZero);
                await _context.SaveChangesAsync();
                //now the postone
                
                _context.Add(postOne);
                await _context.SaveChangesAsync();

                //now for posteig load


                return RedirectToAction(nameof(PostDetail), new { zero = postOne.OneZero });
            }
            return View(postOne);
        }

        // GET: PostOnes/Edit/5
        public async Task<IActionResult> OneEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postOne = await _context.PostOnes.FindAsync(id);
            if (postOne == null)
            {
                return NotFound();
            }
            return View(postOne);
        }

        // POST: PostOnes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OneEdit(int id, [Bind("OneId,OneZero,OneStag,OneAgen,OnePart,OneTitl")] PostOne postOne)
        {
            if (id != postOne.OneId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postOne);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostOneExists(postOne.OneId)) 
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(PostDetail), new { zero = postOne.OneZero });
            }
            return View(postOne);
        }

        private bool PostOneExists(int id)
        {
            return _context.PostNins.Any(e => e.NinId == id);
        }

        //post thr****************************************************************************************************************************************

        // GET: PostThrs/Create
        //public IActionResult ThrCreate()
        //{
        //    return View();
        //}

        
        public IActionResult ThrCreate(string zero, string dateString)
        {


            ViewBag.zero = zero;

            //PostThr postThr = new PostThr { ThrDate = DateTime.Now.AddDays(1) };


            if (String.IsNullOrEmpty(dateString))
            {
                dateString = DateTime.Now.AddDays(3).ToString("yyyy-MM-dd");
                //ToString("yyyy-MM-dd");
            }

            DateTime mDate = System.DateTime.Parse(dateString);

            DateTime dateX = mDate.AddDays(-3);
            ViewBag.dateX = dateX.ToString("yy-MM-dd");

            DateTime dateY = mDate.AddDays(-2);
            ViewBag.dateY = dateY.ToString("yy-MM-dd");

            DateTime dateZ = mDate.AddDays(-1);
            ViewBag.dateZ = dateZ.ToString("yy-MM-dd");

            DateTime date0 = mDate;
            ViewBag.date0 = mDate.ToString("yy-MM-dd");

            DateTime date1 = mDate.AddDays(1);
            ViewBag.date1 = date1.ToString("yy-MM-dd");

            DateTime date2 = mDate.AddDays(2);
            ViewBag.date2 = date2.ToString("yy-MM-dd");

            DateTime date3 = mDate.AddDays(3);
            ViewBag.date3 = date3.ToString("yy-MM-dd");

            var viewModel = new ThrAvailabilityVM
            {
                ThrDate = DateTime.Now,

                Thr0X = _context.PostThrs.Where(m => m.ThrDate.Date == dateX),
                Thr0Y = _context.PostThrs.Where(m => m.ThrDate.Date == dateY),
                Thr0Z = _context.PostThrs.Where(m => m.ThrDate.Date == dateZ),
                Thr00 = _context.PostThrs.Where(m => m.ThrDate.Date == date0),
                Thr01 = _context.PostThrs.Where(m => m.ThrDate.Date == date1),
                Thr02 = _context.PostThrs.Where(m => m.ThrDate.Date == date2),
                Thr03 = _context.PostThrs.Where(m => m.ThrDate.Date == date3),
            };

            return View(viewModel);
        }

        // POST: PostThrs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThrCreate([Bind("ThrId,ThrZero,ThrDigit,ThrDate,ThrTime,ThrText")] PostThr postThr)
        //public async Task<IActionResult> ThrCreate([Bind("ThrId,ThrZero,ThrDigit,ThrDate,ThrText")] PostThr postThr, string zero)
        {
            if (!(_context.PostThrs.Count(n => n.ThrZero == postThr.ThrZero) > 0))
            {
                postThr.ThrDigit = 1;
            }
            else
            {
                postThr.ThrDigit = _context.PostThrs.Where(m => m.ThrZero == postThr.ThrZero).Max(x => x.ThrDigit) + 1;
            }

            if (ModelState.IsValid)
            {
                _context.Add(postThr);
                await _context.SaveChangesAsync();

                //send calendar invite

                //decode date and time
                string timeframe = postThr.ThrDate.ToString("yyyy-MM-dd") + "T" + postThr.ThrTime + ":00.0000000-5:00"; //2018-08-18T07:22:16.0000000Z
                //string timeframe = "2021-02-12T10:00:00.0000000-5:00";
                DateTime calDate = System.DateTime.Parse(timeframe);

                //construct mail
                var message = new MimeMessage();
                //message.From.Add(new MailboxAddress("Halie", "info@ryne.co"));
                message.From.Add(new MailboxAddress("Ryne Calendar", "nic-fleetwood@behr.travel"));
                message.To.Add(new MailboxAddress("info@ryne.co"));
                message.Subject = "Ryne Moving Calendar - " + postThr.ThrDate + "-"+ postThr.ThrText;
                message.Headers.Add("Content-class", "urn:content-classes:calendarmessage");
                
                var emailBody = new BodyBuilder();
                var ics = new StringBuilder();

                //ics file -- use yyyyMMddTHHmmssZ format
                
                ics.AppendLine("BEGIN:VCALENDAR");
                ics.AppendLine("PRODID:-//RYNE MOVING//EN");
                ics.AppendLine("VERSION:2.0");
                ics.AppendLine("METHOD:PUBLISH");
                //THE EVENT
                ics.AppendLine("BEGIN:VEVENT");
                ics.AppendLine("DTSTART:"+ calDate.ToString("yyyyMMddTHHmmss"));
                ics.AppendLine("DTEND:"+ calDate.AddHours(1).ToString("yyyyMMddTHHmmss"));
                ics.AppendLine("DTSTAMP:" + DateTime.Now);
                ics.AppendLine("UID:" + Guid.NewGuid());
                ics.AppendLine("CREATED:" + DateTime.Now);
                ics.AppendLine("X-ALT-DESC;FMTTYPE=text/html:");
                ics.AppendLine("DESCRIPTION:"+ postThr.ThrText);
                ics.AppendLine("LAST-MODIFIED:" + DateTime.Now);
                ics.AppendLine("LOCATION:NYC");
                ics.AppendLine("SEQUENCE:0");
                ics.AppendLine("STATUS:CONFIRMED");
                ics.AppendLine("SUMMARY:"+ postThr.ThrText);
                ics.AppendLine("TRANSP:OPAQUE");
                ics.AppendLine("END:VEVENT");
                ics.AppendLine("END:VCALENDAR");

                using (var stream = new MemoryStream())
                {
                    using (var writer = new StreamWriter(stream))
                    {
                        writer.Write(ics.ToString());

                        writer.Flush();
                        stream.Position = 0;

                        emailBody.Attachments.Add("RyneCalendar.ics", stream);
                    }
                }

                //set message body
                message.Body = emailBody.ToMessageBody();

                //send the email
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.office365.com", 587, false);

                    // Note: only needed if the SMTP server requires authentication
                    //client.Authenticate("info@ryne.co", "foobar");
                    client.Authenticate("nic-fleetwood@behr.travel", "foobar");

                    client.Send(message);
                    client.Disconnect(true);
                }

                //end calendar invite

                return RedirectToAction(nameof(PostDetail), new { zero = postThr.ThrZero });
            }
            return View(postThr);
        }

        // GET: PostThrs/Edit/5
        public async Task<IActionResult> ThrEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postThr = await _context.PostThrs.FindAsync(id);
            if (postThr == null)
            {
                return NotFound();
            }
            return View(postThr);
        }

        // POST: PostThrs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThrEdit(int id, [Bind("ThrId,ThrZero,ThrDigit,ThrDate,ThrTime,ThrText")] PostThr postThr)
        {
            if (id != postThr.ThrId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postThr);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostThrExists(postThr.ThrId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(PostDetail), new { zero = postThr.ThrZero });
            }
            return View(postThr);
        }

        // GET: PostThrs/Delete/5
        public async Task<IActionResult> ThrDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postThr = await _context.PostThrs
                .FirstOrDefaultAsync(m => m.ThrId == id);
            if (postThr == null)
            {
                return NotFound();
            }

            return View(postThr);
        }

        // POST: PostThrs/Delete/5
        [HttpPost, ActionName("ThrDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThrDeleteConfirmed(int id)
        {
            var postThr = await _context.PostThrs.FindAsync(id);
            _context.PostThrs.Remove(postThr);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(PostDetail), new { zero = postThr.ThrZero });
        }

        private bool PostThrExists(int id)
        {
            return _context.PostNins.Any(e => e.NinId == id);
        }

        //post fou****************************************************************************************************************************************

        // GET: PostFous/Create
        public IActionResult FouCreate(string zero)
        {
            ViewBag.zero = zero;

            return View();
        }

        // POST: PostFous/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FouCreate([Bind("FouId,FouZero,FouDigit,FouName,FouPhon,FouEmai,FouAddr,FouPost,FouOrg,FouNote")] PostFou postFou, string zero)
        {

            if (!(_context.PostFous.Count(n => n.FouZero == postFou.FouZero) > 0))
            {
                postFou.FouDigit = 1;
            }
            else
            {
                postFou.FouDigit = _context.PostFous.Where(m => m.FouZero == postFou.FouZero).Max(x => x.FouDigit) + 1;
            }

            if (ModelState.IsValid)
            {
                _context.Add(postFou);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(PostDetail), new { zero = postFou.FouZero });
            }
            return View(postFou);
        }

        // GET: PostFous/Edit/5
        public async Task<IActionResult> FouEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postFou = await _context.PostFous.FindAsync(id);
            if (postFou == null)
            {
                return NotFound();
            }
            return View(postFou);
        }

        // POST: PostFous/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FouEdit(int id, [Bind("FouId,FouZero,FouDigit,FouName,FouPhon,FouEmai,FouAddr,FouPost,FouOrg,FouNote")] PostFou postFou)
        {
            if (id != postFou.FouId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postFou);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostFouExists(postFou.FouId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(PostDetail), new { zero = postFou.FouZero });
            }
            return View(postFou);
        }

        // GET: PostFous/Delete/5
        public async Task<IActionResult> FouDelete(int? id, string zero, string phone, string email, string type)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postFou = await _context.PostFous
                .FirstOrDefaultAsync(m => m.FouId == id);
            if (postFou == null)
            {
                return NotFound();
            }


                return View(postFou);
        }

        // POST: PostFous/Delete/5
        [HttpPost, ActionName("FouDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string zero)
        {
            var postFou = await _context.PostFous.FindAsync(id);
            _context.PostFous.Remove(postFou);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(PostDetail), new { zero = postFou.FouZero });
        }

        public IActionResult FouSendMessage(string zero, string message)
        {


            return View();
        }

        public IActionResult SendContract(int? id, string zero, string type, string address)
        {
            string message = "Please follow the link for you contract -- https://aura20210105160447.azurewebsites.net/Post/PrintPDF?zero=" + zero;

            //text or email

            InternetAddressList list = new InternetAddressList();

            if (type == "phone")
            {
                
                list.Add(new MailboxAddress(address + "@txt.att.net"));
                list.Add(new MailboxAddress(address + "@vtext.com"));
                list.Add(new MailboxAddress(address + "@tmomail.net"));
                list.Add(new MailboxAddress("info@ryne.co"));
            }

            if (type == "email")
            {
                list.Add(new MailboxAddress(address));
                list.Add(new MailboxAddress("info@ryne.co"));
            }

            var tmessage = new MimeMessage();
                tmessage.From.Add(new MailboxAddress("Ryne", "nic-fleetwood@behr.travel"));
                tmessage.To.AddRange(list);
                tmessage.Subject = "RYNE -- Contract";

                var builder = new BodyBuilder();

                builder.TextBody = message;
                // Now we just need to set the message body and we're done
                tmessage.Body = builder.ToMessageBody();

                //send the email
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.office365.com", 587, false);

                    // Note: only needed if the SMTP server requires authentication
                    //client.Authenticate("info@ryne.co", "foobar");
                    client.Authenticate("nic-fleetwood@behr.travel", "foobar");

                    client.Send(tmessage);
                    client.Disconnect(true);
                }

            return RedirectToAction("FouDelete", "Post", new { id });

        }

        public IActionResult MaterialChange(int? id, string zero, string address)
        {
            ViewBag.id = id;
            ViewBag.zero = zero;
            ViewBag.address = address;



            return View();
        }

        public IActionResult MaterialChange(int?id, string zero, string address, string message)
        {



            return RedirectToAction("FouDelete", "Post", new { id });
        }

        private bool PostFouExists(int id)
        {
            return _context.PostNins.Any(e => e.NinId == id);
        }

        //post fiv****************************************************************************************************************************************

        public IActionResult FivInventory(string zero)
        {
            ViewBag.zero = zero;
            return View();
        }

        // GET: PostFivs/Create
        public IActionResult FivCreate(string zero, string code, string priority, string remark)
        {
            ViewBag.zero = zero;
            ViewBag.code = code;
            ViewBag.priority = priority;
            ViewBag.remark = remark;

            PostFiv postFiv = new PostFiv { FivText = remark };

            //PostThr postThr = new PostThr { ThrDate = DateTime.Now.AddDays(1) };

            return View();
        }

        

        // POST: PostFivs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FivCreate([Bind("FivId,FivZero,FivDigit,FivPrio,FivCode,FivText")] PostFiv postFiv, string zero)
        {
            if (!(_context.PostFivs.Count(n => n.FivZero == postFiv.FivZero) > 0))
            {
                postFiv.FivDigit = 1;
            }
            else
            {
                postFiv.FivDigit = _context.PostFivs.Where(m => m.FivZero == postFiv.FivZero).Max(x => x.FivDigit) + 1;
            }
            

            if (ModelState.IsValid)
            {
                _context.Add(postFiv);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(PostDetail), new { zero = postFiv.FivZero });
            }
            //return View(postFiv, new { zero = postFiv.FivZero, code = postFiv.FivCode, priority = postFiv.FivPrio, remark = postFiv.FivText });
            return View(postFiv);
        }

        // GET: PostFivs/Edit/5
        public async Task<IActionResult> FivEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postFiv = await _context.PostFivs.FindAsync(id);
            if (postFiv == null)
            {
                return NotFound();
            }
            return View(postFiv);
        }

        // POST: PostFivs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FivEdit(int id, [Bind("FivId,FivZero,FivDigit,FivPrio,FivCode,FivText")] PostFiv postFiv)
        {
            if (id != postFiv.FivId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postFiv);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostFivExists(postFiv.FivId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(PostDetail), new { zero = postFiv.FivZero });
            }
            return View(postFiv);
        }

        // GET: PostFivs/Delete/5
        public async Task<IActionResult> FivDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postFiv = await _context.PostFivs
                .FirstOrDefaultAsync(m => m.FivId == id);
            if (postFiv == null)
            {
                return NotFound();
            }

            return View(postFiv);
        }

        // POST: PostFivs/Delete/5
        [HttpPost, ActionName("FivDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FivDeleteConfirmed(int id)
        {
            var postFiv = await _context.PostFivs.FindAsync(id);
            _context.PostFivs.Remove(postFiv);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(PostDetail), new { zero = postFiv.FivZero });
        }

        private bool PostFivExists(int id)
        {
            return _context.PostNins.Any(e => e.NinId == id);
        }

        //post six****************************************************************************************************************************************

        // GET: PostSixes/Create
        public IActionResult SixCreate(string zero, string stage, string partyid)
        {
            ViewBag.zero = zero;
            ViewBag.stage = stage;
            ViewBag.partyid = partyid;

            return View();
        }

        // POST: PostSixes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SixCreate([Bind("SixId,SixZero,SixDigit,SixDate,SixType,SixDeta,SixAmou,SixStat,SixNote")] PostSix postSix, PostSev postSev, string stage, string partyid)
        {
            if (!(_context.PostSixs.Count(n => n.SixZero == postSix.SixZero) > 0))
            {
                postSix.SixDigit = 1;
            }
            else
            {
                postSix.SixDigit = _context.PostSixs.Where(m => m.SixZero == postSix.SixZero).Max(x => x.SixDigit) + 1;
            }
            
            
            if (ModelState.IsValid)
            {
                //SevId,SevZero,SevDigit,SevInvo,SevDate,SevDesc,SevAmou,SevAc1,SevAc2,SevAcf,SevSign,SevStage,SevPart,SevCust,SevStat,SevPaym,SevRefe,SevHidd,SevChec,SevNote
                postSev.SevZero = postSix.SixZero;
                if (!(_context.PostSevs.Count(n => n.SevZero == postSev.SevZero) > 0))
                {
                    postSev.SevDigit = 1;
                }
                else
                {
                    postSev.SevDigit = _context.PostSevs.Where(m => m.SevZero == postSev.SevZero).Max(x => x.SevDigit) + 1;
                }
                postSev.SevDate = DateTime.Now;
                postSev.SevDesc = "6PAYM " + postSix.SixNote;
                postSev.SevAmou = postSix.SixAmou * -1; 
                postSev.SevAc1 = "1100";
                postSev.SevAc2 = "1103";
                postSev.SevAcf = "";
                postSev.SevSign = "njn-1"; //should be user.identity.name
                postSev.SevStage = stage;
                postSev.SevPart = partyid;
                postSev.SevCust = partyid;
                postSev.SevStat = "OPEN";
                postSev.SevPaym = "TRUE";
                postSev.SevRefe = "";
                postSev.SevHidd = "FALSE";
                postSev.SevChec = "";
                postSev.SevNote = "";
                _context.Add(postSev);

                _context.Add(postSix);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(PostDetail), new { zero = postSix.SixZero });
            }
            return View(postSix);
        }

        // GET: PostSixes/Edit/5
        public async Task<IActionResult> SixEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postSix = await _context.PostSixs.FindAsync(id);
            if (postSix == null)
            {
                return NotFound();
            }
            return View(postSix);
        }

        // POST: PostSixes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SixEdit(int id, [Bind("SixId,SixZero,SixDigit,SixDate,SixType,SixDeta,SixAmou,SixStat,SixNote")] PostSix postSix)
        {
            if (id != postSix.SixId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postSix);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostSixExists(postSix.SixId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(PostDetail), new { zero = postSix.SixZero });
            }
            return View(postSix);
        }

        // GET: PostSixes/Delete/5
        public async Task<IActionResult> SixDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postSix = await _context.PostSixs
                .FirstOrDefaultAsync(m => m.SixId == id);
            if (postSix == null)
            {
                return NotFound();
            }

            return View(postSix);
        }

        // POST: PostSixes/Delete/5
        [HttpPost, ActionName("SixDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SixDeleteConfirmed(int id)
        {
            var postSix = await _context.PostSixs.FindAsync(id);
            _context.PostSixs.Remove(postSix);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(PostDetail), new { zero = postSix.SixZero });
        }

        private bool PostSixExists(int id)
        {
            return _context.PostNins.Any(e => e.NinId == id);
        }

        //post sev****************************************************************************************************************************************

        // GET: PostSevs/Create
        public IActionResult SevCreate(string zero, string stage, string partyid, string ac1, string ac2, string acf, string desc, string amount, string hidden)
        {
            //asp-route-zero="@ViewBag.zero" asp-route-stage="@ViewBag.stage" asp-route-partyid="@ViewBag.partyid" 
            //asp-route-accountNumber="@item.AccountNumber" asp-route-fractionId="@item.FractionId" asp-route-productDescription="@item.ProductDescription" 
            //asp-route-listPrice="@item.ListPrice"

            ViewBag.zero = zero;
            ViewBag.stage = stage;
            ViewBag.partyid = partyid;
            ViewBag.ac1 = ac1;
            ViewBag.ac2 = ac2;
            ViewBag.acf = acf;
            ViewBag.desc = desc;
            ViewBag.amount = amount;
            ViewBag.hidd = hidden;

            //ViewBag.Ac1 = "1100";
            //ViewBag.Sign = User.Identity; //after authentication
            ViewBag.Sign = "njn-1";
            


            return View();
        }

        // POST: PostSevs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SevCreate([Bind("SevId,SevZero,SevDigit,SevInvo,SevDate,SevDesc,SevAmou,SevAc1,SevAc2,SevAcf,SevSign,SevStage,SevPart,SevCust,SevStat,SevPaym,SevRefe,SevHidd,SevChec,SevNote")] PostSev postSev)
        {
            postSev.SevDate = DateTime.Now.Date;

            if (!(_context.PostSevs.Count(n => n.SevZero == postSev.SevZero) > 0))
            {
                postSev.SevDigit = 1;
            }
            else
            {
                postSev.SevDigit = _context.PostSevs.Where(m => m.SevZero == postSev.SevZero).Max(x => x.SevDigit) + 1;
            }
            

            if (ModelState.IsValid)
            {
                _context.Add(postSev);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(PostDetail), new { zero = postSev.SevZero });
            }
            return View(postSev);
        }

        // get: PostSevProductList

        public async Task<IActionResult> SevProductList(string zero, string stage, string partyid, string large, string small, string fragile, string box, string other)
        {


            ViewBag.zero = zero;
            ViewBag.stage = stage;
            ViewBag.partyid = partyid;

            return View(await _context.ProductList.ToListAsync());
        }

        //get: SevBuilder

        //public async Task<IActionResult> SevBuilder(int? id, string zero, string stage, string partyid)
        //{
        //    ViewBag.zero = zero;

        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var productList = await _context.ProductList.FindAsync(id);
        //    if (productList == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(productList);

        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> SevBuilder(PostSev postSev, string SevZero, string SevDesc, string SevAmou, string SevAc1, string SevAc2, string SevAcf, string SevSign, string SevStage, string SevPart, string SevCust, string SevNote)
        //{

        //    return View();
        //}

        // GET: PostSevs/Edit/5
        public async Task<IActionResult> SevEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postSev = await _context.PostSevs.FindAsync(id);
            if (postSev == null)
            {
                return NotFound();
            }
            return View(postSev);
        }

        // POST: PostSevs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SevEdit(int id, [Bind("SevId,SevZero,SevDigit,SevInvo,SevDate,SevDesc,SevAmou,SevAc1,SevAc2,SevAcf,SevSign,SevStage,SevPart,SevCust,SevStat,SevPaym,SevRefe,SevHidd,SevChec,SevNote")] PostSev postSev)
        {
            if (id != postSev.SevId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postSev);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostSevExists(postSev.SevId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(PostDetail), new { zero = postSev.SevZero });
            }
            return View(postSev);
        }

        // GET: PostSevs/Delete/5
        public async Task<IActionResult> SevDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postSev = await _context.PostSevs
                .FirstOrDefaultAsync(m => m.SevId == id);
            if (postSev == null)
            {
                return NotFound();
            }

            return View(postSev);
        }

        // POST: PostSevs/Delete/5
        [HttpPost, ActionName("SevDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SevDeleteConfirmed(int id, string zero)
        {
            var postSev = await _context.PostSevs.FindAsync(id);
            _context.PostSevs.Remove(postSev);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(PostDetail), new { zero = postSev.SevZero });
        }

        private bool PostSevExists(int id)
        {
            return _context.PostNins.Any(e => e.NinId == id);
        }

        //post eig****************************************************************************************************************************************

        //FIND AGENT
        public async Task<IActionResult> EigAgentList(string zero)
        {

            return View(await _context.Agents.ToListAsync());
        }

        // GET: PostEigs/Create
        public IActionResult EigCreate(string zero)
        {
            ViewBag.zero = zero;

            

            return View();
        }

        // POST: PostEigs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EigCreate([Bind("EigId,EigZero,EigDigit,EigAgen,EigCont,EigRole,EigLoad,EigNote")] PostEig postEig)
        {
            if (!(_context.PostEigs.Count(n => n.EigZero == postEig.EigZero) > 0))
            {
                postEig.EigDigit = 1;
            }
            else
            {
                postEig.EigDigit = _context.PostEigs.Where(m => m.EigZero == postEig.EigZero).Max(x => x.EigDigit) + 1;
            }

            

            if (ModelState.IsValid)
            {
                _context.Add(postEig);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(PostDetail), new { zero = postEig.EigZero });
            }
            return View(postEig);
        }

        // GET: PostEigs/Edit/5
        public async Task<IActionResult> EigEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postEig = await _context.PostEigs.FindAsync(id);
            if (postEig == null)
            {
                return NotFound();
            }
            return View(postEig);
        }

        // POST: PostEigs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EigEdit(int id, [Bind("EigId,EigZero,EigDigit,EigAgen,EigCont,EigRole,EigLoad,EigNote")] PostEig postEig)
        {
            if (id != postEig.EigId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postEig);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostEigExists(postEig.EigId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(PostDetail), new { zero = postEig.EigZero });
            }
            return View(postEig);
        }

        // GET: PostEigs/Delete/5
        public async Task<IActionResult> EigDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postEig = await _context.PostEigs
                .FirstOrDefaultAsync(m => m.EigId == id);
            if (postEig == null)
            {
                return NotFound();
            }

            return View(postEig);
        }

        // POST: PostEigs/Delete/5
        [HttpPost, ActionName("EigDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EigDeleteConfirmed(int id)
        {
            var postEig = await _context.PostEigs.FindAsync(id);
            _context.PostEigs.Remove(postEig);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(PostDetail), new { zero = postEig.EigZero });
        }

        private bool PostEigExists(int id)
        {
            return _context.PostNins.Any(e => e.NinId == id);
        }

        //post nin****************************************************************************************************************************************

        // GET: PostNins/Create
        public IActionResult NinCreate(string zero)
        {
            ViewBag.zero = zero;

            return View();
        }

        // POST: PostNins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NinCreate([Bind("NinId,NinZero,NinDigit,NinFile,NinCapt,NinNote")] PostNin postNin, IFormFile uploadFile)
        {
            
            if (!(_context.PostNins.Count(n => n.NinZero == postNin.NinZero) > 0))
            {
                postNin.NinDigit = 1;
            }
            else
            {
                postNin.NinDigit = _context.PostNins.Count(d => d.NinZero == postNin.NinZero);
            }
            

            if (uploadFile != null && uploadFile.Length > 0)
            {
                var fileTime = DateTime.UtcNow.ToString("yyMMddHHmmss");
                var fileName = fileTime + Path.GetFileName(uploadFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/NinFileUploads", fileName);

                postNin.NinFile = fileName;
                if (ModelState.IsValid)
                {

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await uploadFile.CopyToAsync(fileStream);
                    }


                    _context.Add(postNin);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(PostDetail), new { zero = postNin.NinZero });
                }

            }
            return View(postNin);
        }

        // GET: PostNins/Edit/5
        public async Task<IActionResult> NinEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postNin = await _context.PostNins.FindAsync(id);
            if (postNin == null)
            {
                return NotFound();
            }
            return View(postNin);
        }

        // POST: PostNins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NinEdit(int id, [Bind("NinId,NinZero,NinDigit,NinFile,NinCapt,NinNote")] PostNin postNin)
        {
            if (id != postNin.NinId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postNin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostNinExists(postNin.NinId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(PostDetail), new { zero = postNin.NinZero });
            }
            return View(postNin);
        }

        // GET: PostNins/Delete/5
        public async Task<IActionResult> NinDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postNin = await _context.PostNins
                .FirstOrDefaultAsync(m => m.NinId == id);
            if (postNin == null)
            {
                return NotFound();
            }

            return View(postNin);
        }

        // POST: PostNins/Delete/5
        [HttpPost, ActionName("NinDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NinDeleteConfirmed(int id)
        {
            var postNin = await _context.PostNins.FindAsync(id);
            _context.PostNins.Remove(postNin);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(PostDetail), new { zero = postNin.NinZero });
        }

        private bool PostNinExists(int id)
        {
            return _context.PostNins.Any(e => e.NinId == id);
        }

        //other****************************************************************************************************************************************


    }
}
