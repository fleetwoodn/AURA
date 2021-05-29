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
    [Authorize(Roles = "Admin,Sale,Ops")]
    public class NewAgentClientController : Controller
    {
        private readonly PostContext _context;

        public NewAgentClientController(PostContext context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        //start
        //new create self


        [AllowAnonymous]
        public IActionResult NewICSelf()
        {
            var misctext = _context.MiscText.Find(1);
            ViewBag.description = misctext.Description;
            ViewBag.lastupdate = misctext.LastUpdate;
            ViewBag.fulltext = misctext.FullText;

            return View();
        }

        [HttpPost]
        public IActionResult NewICSelf(Agents agents, AgentsRemarks agentsRemarks, string FirstName, string LastName, string NickName, DateTime Birthdate,
            string TaxId, string PhoneNumber, string CountryCode, string CarrierName, string EmailAddress, string StreetAddress,
            string PostCode, string PaymentType, string PaymentDetail, string ESignature)
        {
            agents.FullName = FirstName + " " + LastName;
            string RawAura = FirstName.Substring(0, 3) + "-" + LastName + "@ryne.co"; //+ DateTime.Now.Year.ToString("yy") + DateTime.Now.Month.ToString("MM") +
            agents.AuraId = RawAura.ToLower();
            agents.NickName = NickName;
            agents.BirthDate = Birthdate;
            agents.TaxId = TaxId;
            agents.PhoneNumber = PhoneNumber;
            agents.EmailAddress = EmailAddress;
            agents.StreetAddress = StreetAddress;
            agents.PostCode = PostCode;
            //test
            agents.TaxType = "1099";
            //if (agents.TaxId.Length < 3 || agents.TaxId == "" || (!agents.TaxId )
            if (!String.IsNullOrEmpty(TaxId))
            {
                agents.BackupWitholding = false;
            }
            else
            {
                agents.BackupWitholding = true;
            }
            agents.PaymentType = PaymentType;
            agents.PaymentDetail = PaymentDetail;

            _context.Add(agents);
            _context.SaveChanges();
            
            //
            //agentsPhone.UserId = agents.UserId;
            //agentsPhone.PhoneNumber = PhoneNumber;
            //agentsPhone.CountryCode = CountryCode;
            //agentsPhone.CarrierName = CarrierName;
            ////
            //agentsEmail.UserId = agents.UserId;
            //agentsEmail.EmailAddress = EmailAddress;
            ////
            //agentsAddress.UserId = agents.UserId;
            //agentsAddress.StreetAddress = StreetAddress;
            //agentsAddress.PostCode = PostCode;
            //
            agentsRemarks.UserId = agents.UserId;
            agentsRemarks.RemarkSubject = "IC Contract Acceptance";
            agentsRemarks.RemarkText = "IC has accepted, understood, and signed contract electronically, here, " + ESignature + ", dated " + DateTime.Now;

            _context.Add(agentsRemarks);
            _context.SaveChanges();
            

            if (String.IsNullOrEmpty(agents.TaxId))
            {
                return RedirectToAction(nameof(BackupWithholding), new { UserId = agents.UserId, Name = agents.FullName, StreetAddress = agents.StreetAddress, Postcode = agents.PostCode });
            }

            return RedirectToAction(nameof(WNineSelf), new { UserId = agents.UserId, Name = agents.FullName, StreetAddress = agents.StreetAddress, Postcode = agents.PostCode });
        }

        [AllowAnonymous]
        public IActionResult BackupWithholding(int? UserId, string Name, string StreetAddress, string Postcode)
        {
            ViewBag.UserId = UserId;
            ViewBag.Name = Name;
            //ViewBag.TaxId = TaxID;
            ViewBag.StreetAddress = StreetAddress;
            ViewBag.PostCode = Postcode;

            return View();
        }

        [AllowAnonymous]
        public IActionResult WNineSelf(int? UserId, string Name, string StreetAddress, string Postcode)
        {
            ViewBag.UserId = UserId;
            ViewBag.Name = Name;
            //ViewBag.TaxId = TaxID;
            ViewBag.StreetAddress = StreetAddress;
            ViewBag.PostCode = Postcode;
            ViewBag.Date = DateTime.Now.Date.ToString("yy-MMM-dd");
            return View();
        }

        [HttpPost]
        public IActionResult WNineSelf(AgentsRemarks agentsRemarks, int UserId, string name, string business,
            string taxclass, string address, string postcode, string taxid, string esign, string edate)
        {
            agentsRemarks.UserId = UserId;
            agentsRemarks.RemarkSubject = "W9 E-Document and Sign";
            agentsRemarks.RemarkText = UserId + "/ " + name + "/ " + business + "/ " + taxclass + "/ " + address + "/ " + postcode + "/ " + taxid + "/ " + esign + "/ " + edate;

            _context.Add(agentsRemarks);
            _context.SaveChanges();

            return RedirectToAction(nameof(NewAgentEndSelf), new { UserId = UserId });
        }

        [AllowAnonymous]
        public IActionResult NewAgentEndSelf(int? UserId)
        {
            ViewBag.UserId = UserId;
            return View();
        }





    }
}
