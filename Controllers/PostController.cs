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

namespace AURA.Controllers
{
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
        public async Task<IActionResult> PostIndex()
        {
            return View(await _context.PostOnes.ToListAsync());
            
        }

        //get: post detail
        public async Task<IActionResult> PostDetail(int? id, string zero)
        {
            ViewBag.OneId = id;

            if (zero == null)
            {
                return NotFound();
            }

            PostOne postOne = _context.PostOnes.Find(id);
            //PostOne postOne = _context.PostOnes.Select(m => m.OneZero == zero); //this is the correct concept, but not sure how to proceed
            var uThr = _context.PostThrs.Where(m => m.ThrZero == zero).ToList();
            var uFou = _context.PostFous.Where(m => m.FouZero == zero).ToList();
            var uFiv = _context.PostFivs.Where(m => m.FivZero == zero).ToList();
            var uSix = _context.PostSixs.Where(m => m.SixZero == zero).ToList();
            var uSev = _context.PostSevs.Where(m => m.SevZero == zero).ToList();
            var uEig = _context.PostEigs.Where(m => m.EigZero == zero).ToList();
            var uNin = _context.PostNins.Where(m => m.NinZero == zero).ToList();

            //var viewModel = new PostDetailVM
            //{

            //}

            return View();

        }
        

    }
}
