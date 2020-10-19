using AURA.Data;
using AURA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AURA.Controllers
{
    public class PostOnesController : Controller
    {
        private readonly PostContext _context;

        public PostOnesController(PostContext context)
        {
            _context = context;
        }

        // GET: PostOnes
        public async Task<IActionResult> Index()
        {
            return View(await _context.PostOnes.ToListAsync());
        }

        // GET: PostOnes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postOne = await _context.PostOnes
                .FirstOrDefaultAsync(m => m.OneId == id);
            if (postOne == null)
            {
                return NotFound();
            }

            return View(postOne);
        }

        // GET: PostOnes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PostOnes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OneId,OneZero,OneStag,OneAgen,OnePart,OneTitl")] PostOne postOne)
        {
            if (ModelState.IsValid)
            {
                _context.Add(postOne);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(postOne);
        }

        // GET: PostOnes/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
        public async Task<IActionResult> Edit(int id, [Bind("OneId,OneZero,OneStag,OneAgen,OnePart,OneTitl")] PostOne postOne)
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
                return RedirectToAction(nameof(Index));
            }
            return View(postOne);
        }

        // GET: PostOnes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postOne = await _context.PostOnes
                .FirstOrDefaultAsync(m => m.OneId == id);
            if (postOne == null)
            {
                return NotFound();
            }

            return View(postOne);
        }

        // POST: PostOnes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postOne = await _context.PostOnes.FindAsync(id);
            _context.PostOnes.Remove(postOne);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostOneExists(int id)
        {
            return _context.PostOnes.Any(e => e.OneId == id);
        }

        /// <summary>
        /// Downloads the post one comma seperated file.
        /// </summary>
        /// <returns></returns>
        public IActionResult DownloadPostOneCommaSeperatedFile()
        {
            try
            {
                var postOnes = _context.PostOnes.ToList();
                StringBuilder stringBuilder = new StringBuilder();
                StringBuilder exportData = stringBuilder;
                stringBuilder.AppendLine("0/,Stage,Lead Agent,Party ID,Title");
                foreach (var author in postOnes)
                {
                    stringBuilder.AppendLine($"{author.OneZero},{ author.OneStag},{ author.OneAgen},{ author.OnePart},{ author.OneTitl}");
                }
                return File(Encoding.UTF8.GetBytes
                (stringBuilder.ToString()), "text/csv", "PostOne.csv");
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}

