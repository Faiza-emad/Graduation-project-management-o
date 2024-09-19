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
    public class ProjectGroupsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectGroupsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProjectGroups
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProjectGroups.Include(p => p.Project_ProjectGroup).Include(p => p.Project_Student);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProjectGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProjectGroups == null)
            {
                return NotFound();
            }

            var projectGroup = await _context.ProjectGroups
                .Include(p => p.Project_ProjectGroup)
                .Include(p => p.Project_Student)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (projectGroup == null)
            {
                return NotFound();
            }

            return View(projectGroup);
        }

        // GET: ProjectGroups/Create
        public IActionResult Create()
        {
            ViewData["ProjectId"] = new SelectList(_context.projects, "Id", "Id");
            ViewData["StudentId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View();
        }

        // POST: ProjectGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectId,StudentId,PQ1,PQ2,PQ3,PQ4,PQ5,DoctorGrade,committeGrade")] ProjectGroup projectGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projectGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectId"] = new SelectList(_context.projects, "Id", "Id", projectGroup.ProjectId);
            ViewData["StudentId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", projectGroup.StudentId);
            return View(projectGroup);
        }

        // GET: ProjectGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProjectGroups == null)
            {
                return NotFound();
            }

            var projectGroup = await _context.ProjectGroups.FindAsync(id);
            if (projectGroup == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = new SelectList(_context.projects, "Id", "Id", projectGroup.ProjectId);
            ViewData["StudentId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", projectGroup.StudentId);
            return View(projectGroup);
        }

        // POST: ProjectGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("ProjectId,StudentId,PQ1,PQ2,PQ3,PQ4,PQ5,DoctorGrade,committeGrade")] ProjectGroup projectGroup)
        {
            if (id != projectGroup.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projectGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectGroupExists(projectGroup.StudentId))
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
            ViewData["ProjectId"] = new SelectList(_context.projects, "Id", "Id", projectGroup.ProjectId);
            ViewData["StudentId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", projectGroup.StudentId);
            return View(projectGroup);
        }

        // GET: ProjectGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProjectGroups == null)
            {
                return NotFound();
            }

            var projectGroup = await _context.ProjectGroups
                .Include(p => p.Project_ProjectGroup)
                .Include(p => p.Project_Student)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (projectGroup == null)
            {
                return NotFound();
            }

            return View(projectGroup);
        }

        // POST: ProjectGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.ProjectGroups == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ProjectGroups'  is null.");
            }
            var projectGroup = await _context.ProjectGroups.FindAsync(id);
            if (projectGroup != null)
            {
                _context.ProjectGroups.Remove(projectGroup);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectGroupExists(int? id)
        {
          return (_context.ProjectGroups?.Any(e => e.StudentId == id)).GetValueOrDefault();
        }
    }
}
