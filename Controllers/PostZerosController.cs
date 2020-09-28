using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AURA.Data;
using AURA.Models;

namespace AURA.Controllers
{
    public class PostZerosController : Controller
    {
        private readonly PostContext _context;

        public PostZerosController(PostContext context)
        {
            _context = context;
        }

        // GET: PostZeroes
        public async Task<IActionResult> Index()
        {
            return View(await _context.PostZeros.ToListAsync());
        }

        // GET: PostZeroes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postZero = await _context.PostZeros
                .FirstOrDefaultAsync(m => m.Zero == id);
            if (postZero == null)
            {
                return NotFound();
            }

            return View(postZero);
        }

        // GET: PostZeroes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PostZeroes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Zero,ZeroDate,ZeroAgen")] PostZero postZero)
        {

            
            postZero.ZeroDate = DateTime.Now;

            string uDate = DateTime.Now.ToString("yyMMdd");
            string uDigit = _context.PostZeros.Count(d => d.ZeroDate == postZero.ZeroDate).ToString();
            postZero.Zero = uDate + "-" + uDigit;

            ///postOne.OneAgent = User.Identity.Name; //we will apply this once the authentication is complete
            postZero.ZeroAgen = "njn-1";

            if (ModelState.IsValid)
            {
                _context.Add(postZero);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(postZero);
        }

        // GET: PostZeroes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postZero = await _context.PostZeros.FindAsync(id);
            if (postZero == null)
            {
                return NotFound();
            }
            return View(postZero);
        }

        // POST: PostZeroes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Zero,ZeroDate,ZeroAgen")] PostZero postZero)
        {
            if (id != postZero.Zero)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postZero);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostZeroExists(postZero.Zero))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(postZero);
        }

        // GET: PostZeroes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postZero = await _context.PostZeros
                .FirstOrDefaultAsync(m => m.Zero == id);
            if (postZero == null)
            {
                return NotFound();
            }

            return View(postZero);
        }

        // POST: PostZeroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var postZero = await _context.PostZeros.FindAsync(id);
            _context.PostZeros.Remove(postZero);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostZeroExists(string id)
        {
            return _context.PostZeros.Any(e => e.Zero == id);
        }
    }
}
