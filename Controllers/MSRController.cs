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
    [Authorize]
    public class MSRController : Controller
    {
        private readonly PostContext _context;
        public MSRController(PostContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        //search
        public async Task<IActionResult> MSRIndex(string searchString)
        {
            //var titles = from m in _context.PostOnes
            //             select m;

            //var titles = (from p in _context.PostOnes

            var titles = _context.PostOnes.OrderByDescending(p => p.OneId).Take(100);

            if (!String.IsNullOrEmpty(searchString))
            {
                titles = from m in _context.PostOnes
                             select m;

                titles = titles.Where(s => s.OneZero.Contains(searchString)
                    || s.OneStag.Contains(searchString)
                    || s.OnePart.Contains(searchString)
                    || s.OneTitl.Contains(searchString)
                    && s.OneType == "MSR"
                    );
            }

            //return View(await _context.PostOnes.ToListAsync());
            return View(await titles.ToListAsync());

        }

        //detail

        
        public IActionResult MSRDetail(string zero)
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

            var uTwo = _context.PostTwos.Where(m => m.TwoZero == zero).ToList();
            var uThr = _context.PostThrs.Where(m => m.ThrZero == zero).ToList();
            var uFou = _context.PostFous.Where(m => m.FouZero == zero).ToList();
            var uFiv = _context.PostFivs.Where(m => m.FivZero == zero).ToList();
            var uSix = _context.PostSixs.Where(m => m.SixZero == zero).ToList();
            var uSev = _context.PostSevs.Where(m => m.SevZero == zero).ToList();
            var uEig = _context.PostEigs.Where(m => m.EigZero == zero).ToList();
            var uNin = _context.PostNins.Where(m => m.NinZero == zero).ToList();

            var viewModel = new MSRDetailVM
            {
                OneId = postOne.OneId.ToString(),
                OneZero = postOne.OneZero,
                OneStag = postOne.OneStag,
                OneAgen = postOne.OneAgen,
                OnePart = postOne.OnePart,
                OneTitl = postOne.OneTitl,

                PostTwos = uTwo,
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

        //printable page
        [AllowAnonymous]
        public IActionResult PrintPDF(string zero)
        {
            var TandC = _context.TermsConditions.Find(1);
            ViewBag.description = TandC.Description;
            ViewBag.fulltext = TandC.FullText;

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
            var uFiv = _context.PostFivs.Where(m => (m.FivZero == zero) && (
                m.FivCode == "D" ||
                m.FivCode == "M" ||
                m.FivCode == "H")
                ).ToList();
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

        //material change
        public IActionResult MaterialChange(string zero)
        {
            ViewBag.zero = zero;
            return View();
        }

        [HttpPost]
        public IActionResult MaterialChange(string zero, string emailAddress, string MatChgDesc)
        {
            PostFiv postFiv = new PostFiv();

            postFiv.FivZero = zero;
            postFiv.FivCode = "x";
            postFiv.FivPrio = "0";
            postFiv.FivText = "Material Change Determined at " + DateTime.Now.ToString() + ". A warning email has been sent to " + emailAddress + ". Description: " + MatChgDesc;


            _context.Add(postFiv);
            _context.SaveChangesAsync();

            //now send emails
            string message = "There has been a Material Change to your job. A supervisor needs to discuss this with you. Please call 212-787-2636 immediately";

            InternetAddressList list = new InternetAddressList();
        
            list.Add(new MailboxAddress(emailAddress));
            list.Add(new MailboxAddress("info@ryne.co"));
            
            //list.Add(new MailboxAddress(address));
            //list.Add(new MailboxAddress("info@ryne.co"));

            var tmessage = new MimeMessage();
            tmessage.From.Add(new MailboxAddress("Ryne", "info@ryne.co"));
            tmessage.To.AddRange(list);
            tmessage.Subject = "RYNE -- Material Change to Contract";

            var builder = new BodyBuilder();

            builder.TextBody = message;
            // Now we just need to set the message body and we're done
            tmessage.Body = builder.ToMessageBody();

            //send the email
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.office365.com", 587, false);

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate("info@ryne.co", "foobar");
                

                client.Send(tmessage);
                client.Disconnect(true);
            }

            return RedirectToAction("MSRDetail", "MSR", new { zero = zero });
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
        [Authorize(Roles = "Admin,Sale")]
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

                postOne.OneType = "MSR";

                _context.Add(postZero);
                await _context.SaveChangesAsync();
                //now the postone

                postOne.OneAgen = User.Identity.Name;

                _context.Add(postOne);
                await _context.SaveChangesAsync();

                //now for posteig load

                PostEig postEig = new PostEig();

                postEig.EigZero = postZero.Zero;
                postEig.EigDigit = 1;
                postEig.EigAgen = User.Identity.Name;
                postEig.EigRole = "SALE";
                postEig.EigLoad = 0;

                _context.Add(postEig);
                await _context.SaveChangesAsync();


                return RedirectToAction(nameof(MSRDetail), new { zero = postOne.OneZero });
            }
            return View(postOne);
        }

        // GET: PostOnes/Edit/5
        [Authorize(Roles = "Admin,Sale")]
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
        [Authorize(Roles = "Admin,Sale")]
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
                return RedirectToAction(nameof(MSRDetail), new { zero = postOne.OneZero });
            }
            return View(postOne);
        }

        private bool PostOneExists(int id)
        {
            return _context.PostNins.Any(e => e.NinId == id);
        }

        //********************************************************************************************************post 2

        // GET: PostTwos/Create
        [Authorize(Roles = "Admin,Sale")]
        public IActionResult TwoCreate(string zero)
        {
            ViewBag.zero = zero;
            return View();
        }

        // POST: PostTwos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,Sale")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TwoCreate([Bind("TwoId,TwoZero,TwoDig,TwoDate,TwoStag,TwoProd,TwoDepCode,TwoArrCode,TwoDepTime,TwoArrTime,TwoElpdTime,TwoCargoRequirement,TwoStat,TwoNote")] PostTwo postTwo)
        {
            //we think we might leave this out of the 'two' model all together. updates required to use include string to int and other mods
            //if (!(_context.PostTwos.Count(n => n.TwoZero == postTwo.TwoZero) > 0))
            //{
            //    postTwo.TwoDig = 1;
            //}
            //else
            //{
            //    postTwo.TwoDig = _context.PostTwos.Where(m => m.TwoZero == postTwo.TwoZero).Max(x => x.TwoDig) + 1;
            //}
            //modify redirect
            if (ModelState.IsValid)
            {
                _context.Add(postTwo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(MSRDetail), new { zero = postTwo.TwoZero });
            }
            return View(postTwo);
        }

        // GET: PostTwos/Edit/5
        [Authorize(Roles = "Admin,Sale")]
        public async Task<IActionResult> TwoEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postTwo = await _context.PostTwos.FindAsync(id);
            if (postTwo == null)
            {
                return NotFound();
            }
            return View(postTwo);
        }

        // POST: PostTwos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,Sale")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TwoEdit(int id, [Bind("TwoId,TwoZero,TwoDig,TwoDate,TwoStag,TwoProd,TwoDepCode,TwoArrCode,TwoDepTime,TwoArrTime,TwoElpdTime,TwoCargoRequirement,TwoStat,TwoNote")] PostTwo postTwo)
        {
            if (id != postTwo.TwoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postTwo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostTwoExists(postTwo.TwoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(MSRDetail), new { zero = postTwo.TwoZero });
            }
            return View(postTwo);
        }

        // GET: PostTwos/Delete/5

        public async Task<IActionResult> TwoDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postTwo = await _context.PostTwos
                .FirstOrDefaultAsync(m => m.TwoId == id);
            if (postTwo == null)
            {
                return NotFound();
            }

            return View(postTwo);
        }

        // POST: PostTwos/Delete/5
        [Authorize(Roles = "Admin,Sale")]
        [HttpPost, ActionName("TwoDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TwoDeleteConfirmed(int id)
        {
            var postTwo = await _context.PostTwos.FindAsync(id);
            _context.PostTwos.Remove(postTwo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(MSRDetail), new { zero = postTwo.TwoZero });
        }

        private bool PostTwoExists(int id)
        {
            return _context.PostTwos.Any(e => e.TwoId == id);
        }

        //post thr****************************************************************************************************************************************

        [Authorize(Roles = "Admin,Sale")]
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
        [Authorize(Roles = "Admin,Sale")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThrCreate([Bind("ThrId,ThrZero,ThrDigit,ThrDate,ThrTime, ThrEndTime, ThrText")] PostThr postThr)
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

                

                return RedirectToAction(nameof(MSRDetail), new { zero = postThr.ThrZero });
            }
            return View(postThr);
        }

        // GET: PostThrs/Edit/5
        [Authorize(Roles = "Admin,Sale")]
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
        [Authorize(Roles = "Admin,Sale")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThrEdit(int id, [Bind("ThrId,ThrZero,ThrDigit,ThrDate,ThrTime,ThrEndTime,ThrText")] PostThr postThr)
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
                return RedirectToAction(nameof(MSRDetail), new { zero = postThr.ThrZero });
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
        [Authorize(Roles = "Admin,Sale")]
        [HttpPost, ActionName("ThrDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThrDeleteConfirmed(int id)
        {
            var postThr = await _context.PostThrs.FindAsync(id);
            _context.PostThrs.Remove(postThr);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(MSRDetail), new { zero = postThr.ThrZero });
        }

        private bool PostThrExists(int id)
        {
            return _context.PostNins.Any(e => e.NinId == id);
        }

        //post fou****************************************************************************************************************************************

        // GET: PostFous/Create
        [Authorize(Roles = "Admin,Sale")]
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
        [Authorize(Roles = "Admin,Sale")]
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
                return RedirectToAction(nameof(MSRDetail), new { zero = postFou.FouZero });
            }
            return View(postFou);
        }

        // GET: PostFous/Edit/5
        [Authorize(Roles = "Admin,Sale")]
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
        [Authorize(Roles = "Admin,Sale")]
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
                return RedirectToAction(nameof(MSRDetail), new { zero = postFou.FouZero });
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
        [Authorize(Roles = "Admin,Sale")]
        [HttpPost, ActionName("FouDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string zero)
        {
            var postFou = await _context.PostFous.FindAsync(id);
            _context.PostFous.Remove(postFou);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(MSRDetail), new { zero = postFou.FouZero });
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

            //list.Add(new MailboxAddress(address));
            //list.Add(new MailboxAddress("info@ryne.co"));

            var tmessage = new MimeMessage();
            tmessage.From.Add(new MailboxAddress("Ryne", "info@ryne.co"));
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
                client.Authenticate("info@ryne.co", "foobar");
                

                client.Send(tmessage);
                client.Disconnect(true);
            }

            return RedirectToAction("MSRDetail", "MSR", new { zero });

        }

        //esign rqst
        public IActionResult FouEsignARqst(string zero, string EmailAddress)
        {
            ViewBag.zero = zero;
            ViewBag.EmailAddress = EmailAddress;

            return View();
        }

        [HttpPost]
        public IActionResult FouEsignARqst(string zero, string EmailAddress, string Comments)
        {
            PostFiv postFiv = new PostFiv();

            postFiv.FivZero = zero;
            postFiv.FivCode = "x";
            postFiv.FivPrio = "0";
            postFiv.FivText = "Signature Request made at " + DateTime.Now.ToString() + ". An email has been sent to " + EmailAddress + ". Description: " + Comments;


            _context.Add(postFiv);
            _context.SaveChanges();

            var FivId = postFiv.FivId;
            
            //create emails

            string message = "An E-Signature is being requested for this job. Please follow the link here to view the comments and sign -- https://aura20210105160447.azurewebsites.net/MSR/EsignMisc?id="+FivId+"&zero=" + zero +"&EmailAddress="+EmailAddress;

            //text or email

            InternetAddressList list = new InternetAddressList();

            list.Add(new MailboxAddress(EmailAddress));
            list.Add(new MailboxAddress("info@ryne.co"));
            

            //list.Add(new MailboxAddress(address));
            //list.Add(new MailboxAddress("info@ryne.co"));

            var tmessage = new MimeMessage();
            tmessage.From.Add(new MailboxAddress("Ryne", "info@ryne.co"));
            tmessage.To.AddRange(list);
            tmessage.Subject = "RYNE -- ESignature Required";

            var builder = new BodyBuilder();

            builder.TextBody = message;
            // Now we just need to set the message body and we're done
            tmessage.Body = builder.ToMessageBody();

            //send the email
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.office365.com", 587, false);

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate("info@ryne.co", "foobar");
                

                client.Send(tmessage);
                client.Disconnect(true);
            }


            return RedirectToAction("MSRDetail", "MSR", new { zero });
        }
        
        //esign client side

        public IActionResult EsignMisc(int? id, string zero, string EmailAddress, string name)
        {
            if (id == null)
            {
                return NotFound();
            }

            PostFiv postFiv = _context.PostFivs.Find(id);

            ViewBag.zero = zero;
            ViewBag.email = EmailAddress;
            ViewBag.comments = postFiv.FivText;
            ViewBag.name = name;
            ViewBag.date = DateTime.Now.ToString();

            return View();
        }

        [HttpPost]
        public IActionResult EsignMisc(string zero, string GeoLocation, string sDateTime, string Signature, string CustomerComments)
        {

            PostFiv postFiv = new PostFiv();

            postFiv.FivZero = zero;
            postFiv.FivCode = "x";
            postFiv.FivPrio = "0";
            postFiv.FivText = "Customer Esigned. Location = "+ GeoLocation + "| Date/Time = " + sDateTime + "| Signature = " + Signature + "| Comments = " + CustomerComments;

            return RedirectToAction("MSRDetail", "MSR", new { zero });
        }

        private bool PostFouExists(int id)
        {
            return _context.PostNins.Any(e => e.NinId == id);
        }

        //post fiv****************************************************************************************************************************************

        public IActionResult ManifestBuilder(string zero)
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
                return RedirectToAction(nameof(MSRDetail), new { zero = postFiv.FivZero });
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
                return RedirectToAction(nameof(MSRDetail), new { zero = postFiv.FivZero });
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
        [Authorize(Roles = "Admin,Sale")]
        [HttpPost, ActionName("FivDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FivDeleteConfirmed(int id)
        {
            var postFiv = await _context.PostFivs.FindAsync(id);
            _context.PostFivs.Remove(postFiv);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(MSRDetail), new { zero = postFiv.FivZero });
        }

        private bool PostFivExists(int id)
        {
            return _context.PostNins.Any(e => e.NinId == id);
        }

        //post six****************************************************************************************************************************************

        // GET: PostSixes/Create
        [Authorize(Roles = "Admin,Sale")]
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
        [Authorize(Roles = "Admin,Sale")]
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
                return RedirectToAction(nameof(MSRDetail), new { zero = postSix.SixZero });
            }
            return View(postSix);
        }

        // GET: PostSixes/Edit/5
        [Authorize(Roles = "Admin,Sale")]
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
        [Authorize(Roles = "Admin,Sale")]
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
                return RedirectToAction(nameof(MSRDetail), new { zero = postSix.SixZero });
            }
            return View(postSix);
        }

        // GET: PostSixes/Delete/5
        [Authorize(Roles = "Admin,Sale")]
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
        [Authorize(Roles = "Admin,Sale")]
        [HttpPost, ActionName("SixDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SixDeleteConfirmed(int id)
        {
            var postSix = await _context.PostSixs.FindAsync(id);
            _context.PostSixs.Remove(postSix);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(MSRDetail), new { zero = postSix.SixZero });
        }

        private bool PostSixExists(int id)
        {
            return _context.PostNins.Any(e => e.NinId == id);
        }

        //post sev****************************************************************************************************************************************

        // GET: PostSevs/Create
        [Authorize(Roles = "Admin,Sale")]
        public IActionResult SevCreate(string zero, string stage, string partyid, string ac1, string ac2, string acf, string desc, string amount, string hidden)
        {
            
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
            ViewBag.Sign = User.Identity.Name; //after authentication
            //ViewBag.Sign = "njn-1";



            return View();
        }

        // POST: PostSevs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,Sale")]
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
                return RedirectToAction(nameof(MSRDetail), new { zero = postSev.SevZero });
            }
            return View(postSev);
        }

        // get: PostSevProductList
        [Authorize(Roles = "Admin,Sale")]
        public async Task<IActionResult> SevProductList(string zero, string stage, string partyid, string large, string small, string fragile, string box, string other)
        {


            ViewBag.zero = zero;
            ViewBag.stage = stage;
            ViewBag.partyid = partyid;

            return View(await _context.ProductList.ToListAsync());
        }

        // GET: PostSevs/Edit/5
        [Authorize(Roles = "Admin,Sale")]
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
        [Authorize(Roles = "Admin,Sale")]
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
                return RedirectToAction(nameof(MSRDetail), new { zero = postSev.SevZero });
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
        [Authorize(Roles = "Admin,Sale")]
        [HttpPost, ActionName("SevDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SevDeleteConfirmed(int id, string zero)
        {
            var postSev = await _context.PostSevs.FindAsync(id);
            _context.PostSevs.Remove(postSev);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(MSRDetail), new { zero = postSev.SevZero });
        }

        private bool PostSevExists(int id)
        {
            return _context.PostNins.Any(e => e.NinId == id);
        }

        //post eig****************************************************************************************************************************************

        

        // GET: PostEigs/Create
        public IActionResult EigCreate(string zero, string auraid, string phone, string role)
        {
            ViewBag.zero = zero;
            ViewBag.auraid = auraid;
            ViewBag.phone = phone;
            ViewBag.role = role;

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
                return RedirectToAction(nameof(MSRDetail), new { zero = postEig.EigZero });
            }
            return View(postEig);
        }

        // GET: PostEigs/Edit/5
        [Authorize(Roles = "Admin,Sale")]
        public async Task<IActionResult> EigEdit(int? id, string auraid)
        {
            ViewBag.auraid = auraid;

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
        [Authorize(Roles = "Admin,Sale")]
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
                return RedirectToAction(nameof(MSRDetail), new { zero = postEig.EigZero });
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
        [Authorize(Roles = "Admin,Sale")]
        [HttpPost, ActionName("EigDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EigDeleteConfirmed(int id)
        {
            var postEig = await _context.PostEigs.FindAsync(id);
            _context.PostEigs.Remove(postEig);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(MSRDetail), new { zero = postEig.EigZero });
        }

        

        private bool PostEigExists(int id)
        {
            return _context.PostNins.Any(e => e.NinId == id);
        }

        //FIND AGENT
        public async Task<IActionResult> EigAgentList(string zero, string stage, string partyid, string searchString)
        {
            ViewBag.zero = zero;
            var agts = _context.Agents.OrderByDescending(p => p.AuraId).Take(100);

            if (!String.IsNullOrEmpty(searchString))
            {
                agts = from m in _context.Agents
                       select m;

                agts = agts.Where(s => s.AuraId.Contains(searchString)
                    || s.FullName.Contains(searchString)
                    || s.PhoneNumber.Contains(searchString)
                    );
            }
            return View(await agts.ToListAsync());
        }

        //calculate rate for agent using tarifflist *** THIS WONT WORK
        public IActionResult CalculateAgentRate(string zero, int id)
        {
            var postEig = _context.PostEigs.FirstOrDefault(m => m.EigId == id);

            //declare...
            decimal LaborAmt = 0;
            decimal teamCount = 0;
                        
            var AgentRole = postEig.EigRole;

            if (AgentRole == "SALE") //tariff list 3 -- sale is (total amt billed * rate)
            {
                var BilledAmt = _context.PostSixs.Where(m => m.SixZero == zero).Sum(x => x.SixAmou);
                postEig.EigLoad = _context.TariffLists.FirstOrDefault(m => m.Id == 3).Amount * BilledAmt;
            }

            if (AgentRole == "DRVR") //tariff list 4 -- drvr is (laborAmt * rate)/teamCount
            {
                LaborAmt = _context.PostSevs.Where(m => m.SevZero == zero && m.SevAc2 == "4201").Sum(x => x.SevAmou);
                teamCount = _context.PostEigs.Where(m => m.EigZero == zero
                    && (m.EigRole == "DRVR" || m.EigRole == "CREW")).Count();
                postEig.EigLoad = (_context.TariffLists.FirstOrDefault(m => m.Id == 4).Amount * LaborAmt) / teamCount;
            }

            if (AgentRole == "CREW") //tariff list 1 -- crew is (laborAmt * rate)/teamCount
            {
                LaborAmt = _context.PostSevs.Where(m => m.SevZero == zero && m.SevAc2 == "4201").Sum(x => x.SevAmou);
                teamCount = _context.PostEigs.Where(m => m.EigZero == zero
                    && (m.EigRole == "DRVR" || m.EigRole == "CREW")).Count();
                postEig.EigLoad = (_context.TariffLists.FirstOrDefault(m => m.Id == 1).Amount * LaborAmt) / teamCount;
            }

            _context.Update(postEig);
            _context.SaveChanges();

            //return View();

            return RedirectToAction(nameof(MSRDetail), new { zero = postEig.EigZero });
        }

        //fast create open
        public IActionResult EigCreateFast(PostEig postEig, string zero, string role)
        {
            if (!(_context.PostEigs.Count(n => n.EigZero == postEig.EigZero) > 0))
            {
                postEig.EigDigit = 1;
            }
            else
            {
                postEig.EigDigit = _context.PostEigs.Where(m => m.EigZero == postEig.EigZero).Max(x => x.EigDigit) + 1;
            }


            postEig.EigZero = zero;
            postEig.EigAgen = "OPEN";
            postEig.EigRole = role;
            postEig.EigLoad = 0;

            //if (ModelState.IsValid)

            _context.Add(postEig);
            _context.SaveChanges();
            return RedirectToAction(nameof(MSRDetail), new { zero = postEig.EigZero });

            //return View(postEig);
            //return View();
            //return RedirectToAction(nameof(PostDetail), new { zero = postEig.EigZero });
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
                    return RedirectToAction(nameof(MSRDetail), new { zero = postNin.NinZero });
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
                return RedirectToAction(nameof(MSRDetail), new { zero = postNin.NinZero });
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
            return RedirectToAction(nameof(MSRDetail), new { zero = postNin.NinZero });
        }

        private bool PostNinExists(int id)
        {
            return _context.PostNins.Any(e => e.NinId == id);
        }

        //other****************************************************************************************************************************************

    }
}
