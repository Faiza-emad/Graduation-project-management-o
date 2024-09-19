using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project2023.Models;
using System.Diagnostics;

namespace Project2023.Controllers
{
    public class HomeController : Controller
    {
        public ApplicationDbContext _context { get; }
        public HomeController(ApplicationDbContext context)
        {

            _context = context;

        }
        private readonly ILogger<HomeController> _logger;

      

        public async Task<IActionResult> Index()
        {

            
            
            var projects = await _context.projects.Where(p => p.Id!=7)
                  .Include(p => p.ProjectCourse) .Include(p=>p.ProjectCourse.CourseDept)
                  .Include(p => p.ProjectDoctor)
                  .Include(p => p.ProjectStudent1)
                  .Include(p => p.ProjectStudent2)
                  .Include(p => p.ProjectStudent3)
                  .Include(p => p.ProjectStudent4)
                  .ToListAsync();

            return View(projects);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}