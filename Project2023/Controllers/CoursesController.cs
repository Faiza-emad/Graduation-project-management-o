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
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        private void SessionData()
        {
            TempData["name"] = HttpContext.Session.GetString("name");
            TempData["img"] = HttpContext.Session.GetString("img");
            TempData["dept"] = HttpContext.Session.GetString("dept");
        }
        // GET: Courses
        public async Task<IActionResult> Index(int id)
        {

            SessionData();
            var applicationDbContext = _context.ApplicationUsers.Where(col => col.UserType == id).Include(a => a.UserDept);
            ViewBag.id = id;

            var applicationDbContext2 = _context.courses.Include(c => c.CourseDept);
            return View(await applicationDbContext2.ToListAsync());
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.courses == null)
            {
                return NotFound();
            }

            var course = await _context.courses
                .Include(c => c.CourseDept)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            SessionData();
            ViewData["DeptId"] = new SelectList(_context.depts, "Id", "DeptName");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CourseId,DeptId,CourseName")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeptId"] = new SelectList(_context.depts, "Id", "Id", course.DeptId);
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            SessionData();
            if (id == null || _context.courses == null)
            {
                return NotFound();
            }

            var course = await _context.courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            ViewData["DeptId"] = new SelectList(_context.depts, "Id", "DeptName", course.DeptId);
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CourseId,DeptId,CourseName")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
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
            ViewData["DeptId"] = new SelectList(_context.depts, "Id", "DeptName", course.DeptId);
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            SessionData();
            if (id == null || _context.courses == null)
            {
                return NotFound();
            }

            var course = await _context.courses
                .Include(c => c.CourseDept)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.courses == null)
            {
                return Problem("Entity set 'ApplicationDbContext.courses'  is null.");
            }
            var course = await _context.courses.FindAsync(id);
            if (course != null)
            {
                _context.courses.Remove(course);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
          return (_context.courses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
