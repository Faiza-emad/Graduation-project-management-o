using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project2023.Models;

namespace Project2023.Controllers
{
    public class DeptsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DeptsController(ApplicationDbContext context)
        {
            _context = context;
        }
        private void SessionData()
        {
            TempData["name"] = HttpContext.Session.GetString("name");
            TempData["img"] = HttpContext.Session.GetString("img");
            TempData["dept"] = HttpContext.Session.GetString("dept");
        }
        // GET: Depts
        public async Task<IActionResult> Index(int id)
        {
            SessionData();
            //if (HttpContext.Session.GetString("id") == null)
            //    return RedirectToAction("Login", "DefaultUser");

            var applicationDbContext = _context.ApplicationUsers.Where(col => col.UserType == id).Include(a => a.UserDept);
            ViewBag.id = id;

            return _context.depts != null ? 
                          View(await _context.depts.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.depts'  is null.");
        }

        // GET: Depts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            SessionData();
            if (id == null || _context.depts == null)
            {
                return NotFound();
            }

            var dept = await _context.depts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dept == null)
            {
                return NotFound();
            }

            return View(dept);
        }

        // GET: Depts/Create
        public IActionResult Create()
        {
            SessionData();
            return View();
        }

        // POST: Depts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DeptName,HOD")] Dept dept)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dept);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dept);
        }

        // GET: Depts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            SessionData();
            if (id == null || _context.depts == null)
            {
                return NotFound();
            }

            var dept = await _context.depts.FindAsync(id);
            if (dept == null)
            {
                return NotFound();
            }
            return View(dept);
        }

        // POST: Depts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DeptName,HOD")] Dept dept)
        {
            if (id != dept.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dept);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeptExists(dept.Id))
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
            return View(dept);
        }

        // GET: Depts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            SessionData();
            if (id == null || _context.depts == null)
            {
                return NotFound();
            }

            var dept = await _context.depts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dept == null)
            {
                return NotFound();
            }

            return View(dept);
        }

        // POST: Depts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.depts == null)
            {
                return Problem("Entity set 'ApplicationDbContext.depts'  is null.");
            }
            var dept = await _context.depts.FindAsync(id);
            if (dept != null)
            {
                _context.depts.Remove(dept);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeptExists(int id)
        {
          return (_context.depts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
