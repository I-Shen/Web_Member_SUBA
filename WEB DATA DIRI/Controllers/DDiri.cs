using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WEB_DATA_DIRI.Data;
using WEB_DATA_DIRI.Models;

namespace WEB_DATA_DIRI.Controllers
{
    public class DDiriController : Controller
    {
        private readonly AppDBContext _context;

        public DDiriController(AppDBContext context)
        {
            _context = context;
        }

        // GET: DDiris
        public async Task<IActionResult> Index()
        {
            return View(await _context.DDiri.ToListAsync());
        }

        // GET: DDiri/Search
        public async Task<IActionResult> Search()
        {
            return View();
        }

        // POST: DDiri/SearchResult
        public async Task<IActionResult> SearchResult(String SearchPhrase)
        {
            return View("Index", await _context.DDiri.Where(cari => cari.Nama.Contains(SearchPhrase)).ToListAsync());
        }

        // GET: DDiris/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dDiri = await _context.DDiri
                .FirstOrDefaultAsync(m => m.ID == id);
            if (dDiri == null)
            {
                return NotFound();
            }

            return View(dDiri);
        }

        // GET: DDiris/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: DDiris/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nama,JK,Role,FavHero")] DDiri dDiri)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dDiri);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dDiri);
        }

        // GET: DDiris/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dDiri = await _context.DDiri.FindAsync(id);
            if (dDiri == null)
            {
                return NotFound();
            }
            return View(dDiri);
        }

        // POST: DDiris/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nama,JK,Role,FavHero")] DDiri dDiri)
        {
            if (id != dDiri.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dDiri);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DDiriExists(dDiri.ID))
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
            return View(dDiri);
        }

        // GET: DDiris/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dDiri = await _context.DDiri
                .FirstOrDefaultAsync(m => m.ID == id);
            if (dDiri == null)
            {
                return NotFound();
            }

            return View(dDiri);
        }

        // POST: DDiris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dDiri = await _context.DDiri.FindAsync(id);
            if (dDiri != null)
            {
                _context.DDiri.Remove(dDiri);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DDiriExists(int id)
        {
            return _context.DDiri.Any(e => e.ID == id);
        }
    }
}
