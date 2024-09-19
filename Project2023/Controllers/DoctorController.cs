using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Project2023.Models;
using Project2023.ViewModels;
using System.Xml.Schema;

namespace Project2023.Controllers
{
    public class DoctorController : Controller
    {
        public ApplicationDbContext _context { get; }

        public DoctorController(ApplicationDbContext context)
        {
            _context = context;
        }
        private void RetrieveSessionData()
        {
            TempData["name"] = HttpContext.Session.GetString("name");
            TempData["img"] = HttpContext.Session.GetString("img");
            TempData["dept"] = HttpContext.Session.GetString("dept");
        }


        private bool isClose(int id)
        {
            var project = _context.projects.AsNoTracking().FirstOrDefault(tbl => tbl.Id == id);
            if (project.Closed == 1)
                return true;
            else
                return false;
        }
        [HttpGet]
        public async Task<IActionResult> ProjectIndex(int id)
        {
            RetrieveSessionData();
            if (HttpContext.Session.GetString("id") == null)
                return RedirectToAction("Login", "DefaultUser");

            var loggedInUserId = HttpContext.Session.GetString("id");
            var loggedInUserIdInt = int.Parse(loggedInUserId);

            var user = await _context.ApplicationUsers.FindAsync(loggedInUserIdInt);
            if (user == null)
            {
                
                return RedirectToAction("Login", "DefaultUser");
            }

            
            if (user.HOD)
            {
                
                var projects = await _context.projects
                    .Where(p => p.ProjectCourse.DeptId == user.DeptId)
                    .Include(p => p.ProjectDoctor)
                    .Include(p => p.ProjectStudent1)
                    .Include(p => p.ProjectStudent2)
                    .Include(p => p.ProjectStudent3)
                    .Include(p => p.ProjectStudent4)
                    .ToListAsync();

                return View(projects);
            }
            else
            {
                
                var projects = await _context.projects
                    .Where(p => p.ProjectDoctor.Id == loggedInUserIdInt || p.Student1 == loggedInUserIdInt || p.Student2 == loggedInUserIdInt || p.Student3 == loggedInUserIdInt || p.Student4 == loggedInUserIdInt)
                    .Include(p => p.ProjectCourse)
                    .Include(p => p.ProjectDoctor)
                    .Include(p => p.ProjectStudent1)
                    .Include(p => p.ProjectStudent2)
                    .Include(p => p.ProjectStudent3)
                    .Include(p => p.ProjectStudent4)
                    .ToListAsync();

                return View(projects);
            }
        }
        [HttpGet]
        public async Task<IActionResult> ProjectIndexStd(int id)
        {
            RetrieveSessionData();
            if (HttpContext.Session.GetString("id") == null)
                return RedirectToAction("Login", "DefaultUser");

            var loggedInUserId = HttpContext.Session.GetString("id");
            var loggedInUserIdInt = int.Parse(loggedInUserId);

            var user = await _context.ApplicationUsers.FindAsync(loggedInUserIdInt);
            if (user == null)
            {
         
                return RedirectToAction("Login", "DefaultUser");
            }

           
            if (user.HOD)
            {
              
                var projects = await _context.projects
                    .Where(p => p.ProjectCourse.DeptId == user.DeptId)
                    .Include(p => p.ProjectDoctor)
                    .Include(p => p.ProjectStudent1)
                    .Include(p => p.ProjectStudent2)
                    .Include(p => p.ProjectStudent3)
                    .Include(p => p.ProjectStudent4)
                    .ToListAsync();

                return View(projects);
            }
            else
            {
                
                var projects = await _context.projects
                    .Where(p => p.ProjectDoctor.Id == loggedInUserIdInt || p.Student1 == loggedInUserIdInt || p.Student2 == loggedInUserIdInt || p.Student3 == loggedInUserIdInt || p.Student4 == loggedInUserIdInt)
                    .Include(p => p.ProjectCourse)
                    .Include(p => p.ProjectDoctor)
                    .Include(p => p.ProjectStudent1)
                    .Include(p => p.ProjectStudent2)
                    .Include(p => p.ProjectStudent3)
                    .Include(p => p.ProjectStudent4)
                    .ToListAsync();

                return View(projects);
            }
        }


      

        [HttpGet]
        //تقييم اللجنة
        public async Task<IActionResult> ProjectCommDecision(int id)
        {


            RetrieveSessionData();


            ProjectCommiteViewModel model = new ProjectCommiteViewModel();
            model.project = await _context.projects.FindAsync(id);
            ViewBag.proID = id;
            if (model.project.StudySem == 1)
            {
                ViewBag.sem = "الاول";
            }
            else if (model.project.StudySem == 2)
            {
                ViewBag.sem = "الثاني";
            }
            else
            {
                ViewBag.sem = "الصيفي";
            }
            model.projectDissDate = await _context.ProjectDissDates .Where(tbl => tbl.ProjectId == id).FirstOrDefaultAsync();
            if (model.projectDissDate== null)
            {
                TempData["error"] = "ارجو اضافة موعد المناقشة";
                return RedirectToAction("ProjectIndex", "Doctor");
            }
            model.projectCommDecision = await _context.ProjectCommDecisions.Where(tbl => tbl.ProjectId == id).FirstOrDefaultAsync();
            if (model.projectCommDecision == null)
            {
                ViewBag.op = 1;
                
            }
            else
            {
                ViewBag.op = 2;
                
            }
            List<ProjectGroup> group = await _context.ProjectGroups.Where(tbl => tbl.ProjectId == id).ToListAsync();
            if (group.Count > 0)
                model.projectGroup = group;
            else
            {
                group = new List<ProjectGroup>();
                group.Add(new ProjectGroup { ProjectId = id, StudentId = model.project.Student1, committeGrade = 0, DoctorGrade = 0, PQ1 = 0, PQ2 = 0, PQ3 = 0, PQ4 = 0, PQ5 = 0 });
                group.Add(new ProjectGroup { ProjectId = id, StudentId = model.project.Student2, committeGrade = 0, DoctorGrade = 0, PQ1 = 0, PQ2 = 0, PQ3 = 0, PQ4 = 0, PQ5 = 0 });
                if (model.project.Student3 != null)
                    group.Add(new ProjectGroup { ProjectId = id, StudentId = model.project.Student3, committeGrade = 0, DoctorGrade = 0, PQ1 = 0, PQ2 = 0, PQ3 = 0, PQ4 = 0, PQ5 = 0 });

                if (model.project.Student4 != null)
                    group.Add(new ProjectGroup { ProjectId = id, StudentId = model.project.Student4, committeGrade = 0, DoctorGrade = 0, PQ1 = 0, PQ2 = 0, PQ3 = 0, PQ4 = 0, PQ5 = 0 });

                model.projectGroup = group;
            }
            ViewBag.supName1 = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == model.projectDissDate.Sup1)?.DoctorName;
            ViewBag.supName2 = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == model.projectDissDate.Sup2)?.DoctorName;
            ViewBag.supName3 = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == model.projectDissDate.Sup3)?.DoctorName;
            ViewBag.Student1Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == model.project.Student1)?.DoctorName;
            ViewBag.Student2Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == model.project.Student2)?.DoctorName;
            ViewBag.Student3Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == model.project.Student3)?.DoctorName;
            ViewBag.Student4Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == model.project.Student4)?.DoctorName;

            //ViewBag.ProjectId = new SelectList(_context.projects, "Id", "ProjectTitle");

            //----------------------
            ViewBag.read = isClose(id);
            
            return View(model);

        }




        [HttpPost]
        public async Task<IActionResult> ProjectCommDecision( string btn, int pid , int op , int c1 , int c2 , int c3, int c4 ,ProjectCommiteViewModel model)
        {
            

            model.project = await _context.projects.FindAsync(pid);
            if (btn == "اغلاق")
            {
                model.project.Closed = 1;
                _context.Update(model.project);
                await _context.SaveChangesAsync();
                return RedirectToAction("ProjectIndex", "Doctor");
            }

                model.projectGroup = new List<ProjectGroup>();

            ProjectGroup group1 =await  _context.ProjectGroups.FirstOrDefaultAsync(tbl => tbl.ProjectId == pid && tbl.StudentId == model.project.Student1);
            group1.committeGrade = c1;
            _context.Update(group1);

            await _context.SaveChangesAsync();


            ProjectGroup group2 = await _context.ProjectGroups.FirstOrDefaultAsync(tbl => tbl.ProjectId == pid && tbl.StudentId == model.project.Student2);
            group2.committeGrade = c2;
            _context.Update(group2);

            await _context.SaveChangesAsync();
            model.projectGroup.Add(group1);
            model.projectGroup.Add(group2);


            ProjectGroup group3 = await _context.ProjectGroups.FirstOrDefaultAsync(tbl => tbl.ProjectId == pid && tbl.StudentId == model.project.Student3);
            if (group3 != null)
            {
                group3.committeGrade = c3;
                _context.Update(group3);

                await _context.SaveChangesAsync();
                model.projectGroup.Add(group3);
            }
            ProjectGroup group4 = await _context.ProjectGroups.FirstOrDefaultAsync(tbl => tbl.ProjectId == pid && tbl.StudentId == model.project.Student4);
            if (group4 != null)
            {
                group4.committeGrade = c4;
                _context.Update(group4);

                await _context.SaveChangesAsync();
                model.projectGroup.Add(group4);
            }


            model.projectCommDecision.ProjectId = pid;
            model.projectDissDate =await  _context.ProjectDissDates.FirstOrDefaultAsync(tbl => tbl.ProjectId == pid);
            model.projectCommDecision.AssDate =model.projectDissDate. GradDate;
            if (op == 1)
                _context.Add(model.projectCommDecision);
            else
                _context.Update(model.projectCommDecision);
            await _context.SaveChangesAsync();



            ViewBag.Total = model.projectCommDecision.Q1 + model.projectCommDecision.Q2 + model.projectCommDecision.Q3 
                + model.projectCommDecision.Q4 + model.projectCommDecision.Q5 + 
                model.projectCommDecision.S1+ model.projectCommDecision.S2+ model.projectCommDecision.S3+ model.projectCommDecision.S4+ model.projectCommDecision.S5+ model.projectCommDecision.S6;
            ViewBag.op = 2;

            //------------------------------------
            ViewBag.supName1 = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == model.projectDissDate.Sup1)?.DoctorName;
            ViewBag.supName2 = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == model.projectDissDate.Sup2)?.DoctorName;
            ViewBag.supName3 = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == model.projectDissDate.Sup3)?.DoctorName;
            ViewBag.Student1Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == model.project.Student1)?.DoctorName;
            ViewBag.Student2Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == model.project.Student2)?.DoctorName;
            ViewBag.Student3Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == model.project.Student3)?.DoctorName;
            ViewBag.Student4Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == model.project.Student4)?.DoctorName;
            return View(model);
        }

     
        private bool ProjectCommDecisionExists(int id)
        {
            return (_context.ProjectCommDecisions?.Any(e => e.ProjectId == id)).GetValueOrDefault();
        }
        [HttpGet]
        public async Task<IActionResult> ProjectCommDecisionStd(int id)
        {


            RetrieveSessionData();


            ProjectCommiteViewModel model = new ProjectCommiteViewModel();
            model.project = await _context.projects.FindAsync(id);
            ViewBag.proID = id;
            if (model.project.StudySem == 1)
            {
                ViewBag.sem = "الاول";
            }
            else if (model.project.StudySem == 2)
            {
                ViewBag.sem = "الثاني";
            }
            else
            {
                ViewBag.sem = "الصيفي";
            }
            model.projectDissDate = await _context.ProjectDissDates.Where(tbl => tbl.ProjectId == id).FirstOrDefaultAsync();
            if (model.projectDissDate == null)
            {
                TempData["error"] = "ارجو اضافة موعد المناقشة";
                return RedirectToAction("ProjectIndexStd", "Doctor");
            }
            model.projectCommDecision = await _context.ProjectCommDecisions.Where(tbl => tbl.ProjectId == id).FirstOrDefaultAsync();
            if (model.projectCommDecision == null)
            {
                ViewBag.op = 1;

            }
            else
            {
                ViewBag.op = 2;

            }
            List<ProjectGroup> group = await _context.ProjectGroups.Where(tbl => tbl.ProjectId == id).ToListAsync();
            if (group.Count > 0)
                model.projectGroup = group;
            else
            {
                group = new List<ProjectGroup>();
                group.Add(new ProjectGroup { ProjectId = id, StudentId = model.project.Student1, committeGrade = 0, DoctorGrade = 0, PQ1 = 0, PQ2 = 0, PQ3 = 0, PQ4 = 0, PQ5 = 0 });
                group.Add(new ProjectGroup { ProjectId = id, StudentId = model.project.Student2, committeGrade = 0, DoctorGrade = 0, PQ1 = 0, PQ2 = 0, PQ3 = 0, PQ4 = 0, PQ5 = 0 });
                if (model.project.Student3 != null)
                    group.Add(new ProjectGroup { ProjectId = id, StudentId = model.project.Student3, committeGrade = 0, DoctorGrade = 0, PQ1 = 0, PQ2 = 0, PQ3 = 0, PQ4 = 0, PQ5 = 0 });

                if (model.project.Student4 != null)
                    group.Add(new ProjectGroup { ProjectId = id, StudentId = model.project.Student4, committeGrade = 0, DoctorGrade = 0, PQ1 = 0, PQ2 = 0, PQ3 = 0, PQ4 = 0, PQ5 = 0 });

                model.projectGroup = group;
            }
            ViewBag.Total = model.projectCommDecision.Q1 + model.projectCommDecision.Q2 + model.projectCommDecision.Q3
               + model.projectCommDecision.Q4 + model.projectCommDecision.Q5 +
               model.projectCommDecision.S1 + model.projectCommDecision.S2 + model.projectCommDecision.S3 + model.projectCommDecision.S4 + model.projectCommDecision.S5 + model.projectCommDecision.S6;
            ViewBag.supName1 = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == model.projectDissDate.Sup1)?.DoctorName;
            ViewBag.supName2 = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == model.projectDissDate.Sup2)?.DoctorName;
            ViewBag.supName3 = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == model.projectDissDate.Sup3)?.DoctorName;
            ViewBag.Student1Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == model.project.Student1)?.DoctorName;
            ViewBag.Student2Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == model.project.Student2)?.DoctorName;
            ViewBag.Student3Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == model.project.Student3)?.DoctorName;
            ViewBag.Student4Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == model.project.Student4)?.DoctorName;

            //ViewBag.ProjectId = new SelectList(_context.projects, "Id", "ProjectTitle");

            //----------------------
            ViewBag.read = isClose(id);

            return View(model);

        }
        [HttpGet]
        public async Task<IActionResult> ProjectWeeksForm(int id)
        {
            RetrieveSessionData();
            if (id == 0 || _context.projects == null)
            {
                return NotFound();
            }


            var projectWeek = await _context.ProjectWeeks.Where(tbl=> tbl.ProjectId == id).FirstOrDefaultAsync ();
            ViewBag.proID = id;

            if (projectWeek == null)
            {
                ViewBag.op = 1;  // insert
            }
            else
            {
                ViewBag.op = 2;  // edit
            }
            Project project =  await _context.projects.Where(col => col.Id == id).FirstOrDefaultAsync();
            ViewBag.ProjectTitle = project.ProjectTitle.ToString();
            ViewBag.DoctorName = _context.ApplicationUsers.FirstOrDefault(u => u.Id == project.DoctorId)?.DoctorName;
          
            ViewBag.Student1Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student1)?.DoctorName;
            ViewBag.Student2Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student2)?.DoctorName;
            ViewBag.Student3Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student3)?.DoctorName;
            ViewBag.Student4Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student4)?.DoctorName;
            //ViewBag.date = projectWeek.Date1;
            ViewBag.read = isClose(id);

            return View("ProjectWeeksForm", projectWeek);
        
        
            
        
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProjectWeeksForm( int op , int pid , [Bind("Week1,Date1,Week2,Date2,Week3,Date3,Week4,Date4,Week5,Date5,Week6,Date6,Week7,Date7,Week8,Date8,Week9,Date9,Week10,Date10,Week11,Date11,Week12,Date12,percentage1,percentage2,percentage3,percentage4,percentage5,percentage6,percentage7,percentage8,percentage9,percentage10,percentage11,percentage12,ProjectId")] ProjectWeek projectWeek)
        {
            RetrieveSessionData();
            projectWeek.ProjectId = pid;
            if (ModelState.IsValid)
            {
                if (op == 1)
                {
                    _context.Add(projectWeek);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(ProjectIndexStd));
                }
                else
                {
                    _context.Update(projectWeek);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(ProjectIndexStd));
                }
                
            }
            else
            {
                foreach (var modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        ViewBag.error = error;
                    }
                }

                ViewBag.ProjectId = new SelectList(_context.projects, "Id", "Id", projectWeek.ProjectId);
                return View(projectWeek);
            }
            ViewBag.ProjectId = new SelectList(_context.projects, "Id", "Id", projectWeek.ProjectId);
            return View(projectWeek);
        }

        [HttpGet]

        public async Task<IActionResult> ProjectWeeksFormDoc(int id)
        {
            RetrieveSessionData();
            if (id == 0 || _context.projects == null)
            {
                return NotFound();
            }


            var projectWeek = await _context.ProjectWeeks.Where(tbl => tbl.ProjectId == id).FirstOrDefaultAsync();
            ViewBag.proID = id;

            if (projectWeek == null)
            {
                ViewBag.op = 1;  // insert
            }
            else
            {
                ViewBag.op = 2;  // edit
            }
            Project project = await _context.projects.Where(col => col.Id == id).FirstOrDefaultAsync();
            ViewBag.ProjectTitle = project.ProjectTitle.ToString();
            ViewBag.DoctorName = _context.ApplicationUsers.FirstOrDefault(u => u.Id == project.DoctorId)?.DoctorName;
           
            ViewBag.Student1Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student1)?.DoctorName;
            ViewBag.Student2Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student2)?.DoctorName;
            ViewBag.Student3Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student3)?.DoctorName;
            ViewBag.Student4Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student4)?.DoctorName;
            ViewBag.read = isClose(id);
            return View("ProjectWeeksFormDoc", projectWeek);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProjectWeeksFormDoc(int op, int pid, [Bind("Week1,Date1,Week2,Date2,Week3,Date3,Week4,Date4,Week5,Date5,Week6,Date6,Week7,Date7,Week8,Date8,Week9,Date9,Week10,Date10,Week11,Date11,Week12,Date12,percentage1,percentage2,percentage3,percentage4,percentage5,percentage6,percentage7,percentage8,percentage9,percentage10,percentage11,percentage12,ProjectId")] ProjectWeek projectWeek)
        {
            //if (projectWeek.percentage1 == null || projectWeek.percentage1 > 100) projectWeek.percentage1 = projectWeek.percentage1 == null ? 0 : 100;
            //if (projectWeek.percentage2 == null || projectWeek.percentage2 > 100) projectWeek.percentage2 = projectWeek.percentage2 == null ? 0 : 100;
            //if (projectWeek.percentage3 == null || projectWeek.percentage3 > 100) projectWeek.percentage3 = projectWeek.percentage3 == null ? 0 : 100;
            //if (projectWeek.percentage4 == null || projectWeek.percentage4 > 100) projectWeek.percentage4 = projectWeek.percentage4 == null ? 0 : 100;
            //if (projectWeek.percentage5 == null || projectWeek.percentage5 > 100) projectWeek.percentage5 = projectWeek.percentage5 == null ? 0 : 100;
            //if (projectWeek.percentage6 == null || projectWeek.percentage6 > 100) projectWeek.percentage6 = projectWeek.percentage6 == null ? 0 : 100;
            //if (projectWeek.percentage7 == null || projectWeek.percentage7 > 100) projectWeek.percentage7 = projectWeek.percentage7 == null ? 0 : 100;
            //if (projectWeek.percentage8 == null || projectWeek.percentage8 > 100) projectWeek.percentage8 = projectWeek.percentage8 == null ? 0 : 100;
            //if (projectWeek.percentage9 == null || projectWeek.percentage9 > 100) projectWeek.percentage9 = projectWeek.percentage9 == null ? 0 : 100;
            //if (projectWeek.percentage10 == null || projectWeek.percentage10 > 100) projectWeek.percentage10 = projectWeek.percentage10 == null ? 0 : 100;
            //if (projectWeek.percentage11 == null || projectWeek.percentage11 > 100) projectWeek.percentage11 = projectWeek.percentage11 == null ? 0 : 100;
            //if (projectWeek.percentage12 == null || projectWeek.percentage12 > 100) projectWeek.percentage12 = projectWeek.percentage12 == null ? 0 : 100;


            //if (projectWeek.percentage1 == null) projectWeek.percentage1 = 0;

            //if (projectWeek.percentage2 == null) projectWeek.percentage2 = 0;
            //if (projectWeek.percentage3 == null) projectWeek.percentage3 = 0;
            //if (projectWeek.percentage4 == null) projectWeek.percentage4 = 0;
            //if (projectWeek.percentage5 == null) projectWeek.percentage5 = 0;

            //if (projectWeek.percentage6 == null) projectWeek.percentage6 = 0;
            //if (projectWeek.percentage7 == null) projectWeek.percentage7 = 0;
            //if (projectWeek.percentage8 == null) projectWeek.percentage8 = 0;
            //if (projectWeek.percentage9 == null) projectWeek.percentage9 = 0;

            //if (projectWeek.percentage10 == null) projectWeek.percentage10 = 0;
            //if (projectWeek.percentage11 == null) projectWeek.percentage11 = 0;
            //if (projectWeek.percentage12 == null) projectWeek.percentage12 = 0;
            projectWeek.percentage1 ??= 0;
            projectWeek.percentage2 ??= 0;
            projectWeek.percentage3 ??= 0;
            projectWeek.percentage4 ??= 0;
            projectWeek.percentage5 ??= 0;
            projectWeek.percentage6 ??= 0;
            projectWeek.percentage7 ??= 0;
            projectWeek.percentage8 ??= 0;
            projectWeek.percentage9 ??= 0;
            projectWeek.percentage10 ??= 0;
            projectWeek.percentage11 ??= 0;
            projectWeek.percentage12 ??= 0;

            // Validate percentages
            ValidatePercentage(projectWeek.percentage1, "percentage1");
            ValidatePercentage(projectWeek.percentage2, "percentage2");
            ValidatePercentage(projectWeek.percentage3, "percentage3");
            ValidatePercentage(projectWeek.percentage4, "percentage4");
            ValidatePercentage(projectWeek.percentage5, "percentage5");
            ValidatePercentage(projectWeek.percentage6, "percentage6");
            ValidatePercentage(projectWeek.percentage7, "percentage7");
            ValidatePercentage(projectWeek.percentage8, "percentage8");
            ValidatePercentage(projectWeek.percentage9, "percentage9");
            ValidatePercentage(projectWeek.percentage10, "percentage10");
            ValidatePercentage(projectWeek.percentage11, "percentage11");
            ValidatePercentage(projectWeek.percentage12, "percentage12");

            // Check if model state is valid
            if (!ModelState.IsValid)
            {
                foreach (var modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        ViewBag.errors = error.ErrorMessage;
                    }
                }

                ViewBag.ProjectId = new SelectList(_context.projects, "Id", "Id", projectWeek.ProjectId);
                return View("ProjectWeeksFormDoc", "Doctor");
            }

            RetrieveSessionData();
            projectWeek.ProjectId = pid;
            if (ModelState.IsValid)
            {
                
                if (op == 1)
                {
                    _context.Add(projectWeek);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(ProjectIndex));
                }
                else
                {
                    _context.Update(projectWeek);
                    await _context.SaveChangesAsync();
                    

                    Project project =await  _context.projects.FindAsync(pid);
                    
                    if (project != null)
                    {



                        project.percentage = (projectWeek.percentage1 + projectWeek.percentage2 + projectWeek.percentage3 + projectWeek.percentage4 + projectWeek.percentage5 + projectWeek.percentage6 + projectWeek.percentage7 + projectWeek.percentage8 + projectWeek.percentage9 + projectWeek.percentage10 + projectWeek.percentage11 + projectWeek.percentage12) / 12;
                        _context.Update(project);
                        await _context.SaveChangesAsync();
                    }
                    return RedirectToAction(nameof(ProjectIndex));
                }

            }
            else
            {
                foreach (var modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        ViewBag.error = error;
                    }
                }

                ViewBag.ProjectId = new SelectList(_context.projects, "Id", "Id", projectWeek.ProjectId);
                return View(projectWeek);
            }
           
        }
        private void ValidatePercentage(int? percentage, string fieldName)
        {
            if (percentage > 100)
            {
                ModelState.AddModelError(fieldName, "لا يمكن تعيين قيمة أكبر من 100");
            }
        }
        private bool ProjectWeekExists(int id)
        {
            return (_context.ProjectWeeks?.Any(e => e.ProjectId == id)).GetValueOrDefault();
        }

        #region "Diss Date For Doctor"

       

        [HttpGet]
        public async Task<IActionResult> DissDateForm(int id)
        {
            RetrieveSessionData();
            var projectAsse = await _context.ProjectAssessments.Where(tbl => tbl.ProjectId == id).FirstOrDefaultAsync();
            if (projectAsse == null)
            {
                TempData["error"] = "ارجو تقييم المشروع من قبل المشرف  ";
                return RedirectToAction("ProjectIndex", "Doctor");
            }
            var project = await _context.projects.FindAsync(id);
            var projectDiss = await _context.ProjectDissDates.Where(tbl => tbl.ProjectId == id).FirstOrDefaultAsync();
            ViewBag.ProjectTitle = project.ProjectTitle.ToString();
            ViewBag.StudentId1 = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student1)?.DoctorId;
            ViewBag.StudentId2 = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student2)?.DoctorId;
            ViewBag.StudentId3 = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student3)?.DoctorId;
            ViewBag.StudentId4 = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student4)?.DoctorId;
            ViewBag.Student1Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student1)?.DoctorName;
            ViewBag.Student2Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student2)?.DoctorName;
            ViewBag.Student3Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student3)?.DoctorName;
            ViewBag.Student4Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student4)?.DoctorName;
            ViewBag.DoctorName = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.DoctorId)?.DoctorName;
            //ViewBag.ProjectId = new SelectList(_context.projects, "Id", "ProjectTitle");


           
            ViewBag.proID = id;



            if (projectDiss != null)
            {
                ViewBag.op = 2;  // edit
                ViewBag.Sup1 =new SelectList( _context.ApplicationUsers.Where(tbl => tbl.UserType == 1), "Id","DoctorName",projectDiss.Sup1);
                ViewBag.Sup2 = new SelectList(_context.ApplicationUsers.Where(tbl => tbl.UserType == 1), "Id", "DoctorName", projectDiss.Sup2);
                ViewBag.Sup3 = new SelectList(_context.ApplicationUsers.Where(tbl => tbl.UserType == 1), "Id", "DoctorName", projectDiss.Sup3);

            }
            else
            {
                ViewBag.op = 1;  // insert
                ViewBag.Sup1 = new SelectList(_context.ApplicationUsers.Where(tbl => tbl.UserType == 1), "Id", "DoctorName",project.DoctorId);
                ViewBag.Sup2 = new SelectList(_context.ApplicationUsers.Where(tbl => tbl.UserType == 1), "Id", "DoctorName");
                ViewBag.Sup3 = new SelectList(_context.ApplicationUsers.Where(tbl => tbl.UserType == 1), "Id", "DoctorName");
            }


            return View(projectDiss);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DissDateForm(int op , int pid , [Bind("GradDate,GradDay,GradPlace,Sup1,Sup2,Sup3,ProjectId")] ProjectDissDate projectDissDate)
        {
            Project project=null;
            if (projectDissDate.Sup1 == projectDissDate .Sup2 || projectDissDate.Sup1 == projectDissDate.Sup3|| projectDissDate.Sup2== projectDissDate.Sup3)
            {
                ViewBag.op = op;
                ViewBag.proID = pid;
                ViewBag.ProjectId = new SelectList(_context.projects, "Id", "DoctorName", projectDissDate.ProjectId);
                ViewBag.Sup1 = new SelectList(_context.ApplicationUsers, "Id", "DoctorName", projectDissDate.Sup1);
                ViewBag.Sup2 = new SelectList(_context.ApplicationUsers, "Id", "DoctorName", projectDissDate.Sup2);
                ViewBag.Sup3 = new SelectList(_context.ApplicationUsers, "Id", "DoctorName", projectDissDate.Sup3);
                 project = await _context.projects.FindAsync(pid);
                ViewBag.ProjectTitle = project.ProjectTitle.ToString();
                ViewBag.StudentId1 = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student1)?.DoctorId;
                ViewBag.StudentId2 = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student2)?.DoctorId;
                ViewBag.StudentId3 = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student3)?.DoctorId;
                ViewBag.StudentId4 = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student4)?.DoctorId;
                ViewBag.Student1Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student1)?.DoctorName;
                ViewBag.Student2Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student2)?.DoctorName;
                ViewBag.Student3Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student3)?.DoctorName;
                ViewBag.Student4Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student4)?.DoctorName;
                ViewBag.DoctorName = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.DoctorId)?.DoctorName;
                return View(projectDissDate);
            }
            
            projectDissDate.ProjectId = pid;
            

            if (ModelState.IsValid)
            {
                if (op == 1)
                {

                    _context.Add(projectDissDate);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(ProjectIndex));
                }
                else
                {
                    _context.Update(projectDissDate);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(ProjectIndex));
                }
            }
            foreach (var modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    ViewBag.error = error;
                }
            }

            ViewBag.op = op;
            ViewBag.pid = pid;
            ViewBag.ProjectId = new SelectList(_context.projects, "Id", "DoctorName", projectDissDate.ProjectId);
            ViewBag.Sup1 = new SelectList(_context.ApplicationUsers, "Id", "DoctorName", projectDissDate.Sup1);
            ViewBag.Sup2 = new SelectList(_context.ApplicationUsers, "Id", "DoctorName", projectDissDate.Sup2);
            ViewBag.Sup3 = new SelectList(_context.ApplicationUsers, "Id", "DoctorName", projectDissDate.Sup3);
             project = await _context.projects.FindAsync(pid);
            ViewBag.ProjectTitle = project.ProjectTitle.ToString();
            ViewBag.StudentId1 = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student1)?.DoctorId;
            ViewBag.StudentId2 = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student2)?.DoctorId;
            ViewBag.StudentId3 = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student3)?.DoctorId;
            ViewBag.StudentId4 = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student4)?.DoctorId;
            ViewBag.Student1Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student1)?.DoctorName;
            ViewBag.Student2Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student2)?.DoctorName;
            ViewBag.Student3Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student3)?.DoctorName;
            ViewBag.Student4Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student4)?.DoctorName;
            ViewBag.DoctorName = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.DoctorId)?.DoctorName;
            return View(projectDissDate);
        }
        #endregion



        [HttpGet]
        public async Task<IActionResult> DissDateFormStd(int id)
        {
           

            RetrieveSessionData();
            var projectdiss = await _context.ProjectDissDates.Where(tbl => tbl.ProjectId == id).FirstOrDefaultAsync();
            var project = await _context.projects.FindAsync(id);
            var projectWeek = await _context.ProjectWeeks.Where(tbl => tbl.ProjectId == id).FirstOrDefaultAsync();

            if (project == null || projectWeek == null)
            {
                return NotFound();
            }

            ViewBag.ProjectTitle = project.ProjectTitle.ToString();
           
            ViewBag.Student1Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student1)?.DoctorName;
            ViewBag.Student2Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student2)?.DoctorName;
            ViewBag.Student3Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student3)?.DoctorName;
            ViewBag.Student4Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student4)?.DoctorName;
            ViewBag.DoctorName = _context.ApplicationUsers.FirstOrDefault(u => u.Id == project.DoctorId)?.DoctorName;
            ViewBag.Sup1 = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == projectdiss.Sup1)?.DoctorName;
            ViewBag.Sup2 = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == projectdiss.Sup2)?.DoctorName;
            ViewBag.Sup3 = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == projectdiss.Sup3)?.DoctorName;
            //ViewBag.GradDate = _context.pro;
            //ViewBag.date = DateTime.Parse(projectdiss.GradDate.ToString());
            //ViewBag.date = projectdiss.GradDate;
            ViewBag.day = projectdiss.GradDay;
            //ViewBag.GradDay = projectDissDate.GradDay;
            ViewBag.read = isClose(id);
            return View("DissDateFormStd", projectdiss);

        }
        [HttpGet]
       
        private bool ProjectDissDateExists(int id)
        {
            return (_context.ProjectDissDates?.Any(e => e.ProjectId == id)).GetValueOrDefault();
        }

        [HttpGet]
        public async Task<IActionResult> ProjectAssessmentForm(int id)
        {
            RetrieveSessionData();


            ProjectFinalAssessViewModel model = new ProjectFinalAssessViewModel();
            model.project = await _context.projects.FindAsync(id);
            model.projectAssessment = await _context.ProjectAssessments.Where(tbl => tbl.ProjectId == id).FirstOrDefaultAsync();
            if (model.projectAssessment ==null)
            {
                ViewBag.op = 1;
                ViewBag.rate1 = new SelectList(_context.Ratings, "Id", "Rate");
                ViewBag.rate2 = new SelectList(_context.Ratings, "Id", "Rate");
                ViewBag.rate3 = new SelectList(_context.Ratings, "Id", "Rate");
                ViewBag.rate4 = new SelectList(_context.Ratings, "Id", "Rate");
                ViewBag.rate5 = new SelectList(_context.Ratings, "Id", "Rate");
                ViewBag.rate6 = new SelectList(_context.Ratings, "Id", "Rate");
                ViewBag.rate7 = new SelectList(_context.Ratings, "Id", "Rate");
            }
            else
            {
                ViewBag.op = 2;
                ViewBag.rate1 = new SelectList(_context.Ratings, "Id", "Rate" , model.projectAssessment .PQ1 );
                ViewBag.rate2 = new SelectList(_context.Ratings, "Id", "Rate", model.projectAssessment.PQ2);
                ViewBag.rate3 = new SelectList(_context.Ratings, "Id", "Rate", model.projectAssessment.PQ3);
                ViewBag.rate4 = new SelectList(_context.Ratings, "Id", "Rate", model.projectAssessment.PQ4);
                ViewBag.rate5 = new SelectList(_context.Ratings, "Id", "Rate", model.projectAssessment.PQ5);
                ViewBag.rate6 = new SelectList(_context.Ratings, "Id", "Rate", model.projectAssessment.PQ6);
                ViewBag.rate7 = new SelectList(_context.Ratings, "Id", "Rate", model.projectAssessment.PQ7);
            }
           List<ProjectGroup> group = await _context.ProjectGroups.Where(tbl => tbl.ProjectId == id).ToListAsync();
            if (group.Count > 0)
                model.projectGroup = group;
            else
            {
                group = new List<ProjectGroup>();
                group.Add( new ProjectGroup { ProjectId =id , StudentId = model.project.Student1 , committeGrade =0 , DoctorGrade =0, PQ1=0, PQ2=0,PQ3=0,PQ4=0,PQ5=0  });
               group.Add(new ProjectGroup { ProjectId = id, StudentId = model.project.Student2, committeGrade = 0, DoctorGrade = 0, PQ1 = 0, PQ2 = 0, PQ3 = 0, PQ4 = 0, PQ5 = 0 });
                if (model .project .Student3 != null)
               group.Add(new ProjectGroup { ProjectId = id, StudentId = model.project.Student3, committeGrade = 0, DoctorGrade = 0, PQ1 = 0, PQ2 = 0, PQ3 = 0, PQ4 = 0, PQ5 = 0 });

                if (model.project.Student4 != null)
                    group.Add(new ProjectGroup { ProjectId = id, StudentId = model.project.Student4, committeGrade = 0, DoctorGrade = 0, PQ1 = 0, PQ2 = 0, PQ3 = 0, PQ4 = 0, PQ5 = 0 });

                model.projectGroup = group;
            }
            
           
            ViewBag.Student1Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == model.project.Student1)?.DoctorName;
            ViewBag.Student2Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == model.project.Student2)?.DoctorName;
            ViewBag.Student3Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == model.project.Student3)?.DoctorName;
            ViewBag.Student4Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == model.project.Student4)?.DoctorName;
            ViewBag.DoctorName = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == model.project.DoctorId)?.DoctorName;
            //ViewBag.ProjectId = new SelectList(_context.projects, "Id", "ProjectTitle");
            ViewBag.read = isClose(id);
            return View(model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProjectAssessmentForm(ProjectFinalAssessViewModel model , int op , int pid , int QSt11, int QSt12, int QSt13, int QSt21, int QSt22, int QSt23, int QSt31, int QSt32, int QSt33, int QSt41, int QSt42, int QSt43, int QSt51, int QSt52, int QSt53, int QSt14, int QSt24, int QSt34, int QSt44, int QSt54)
        {
            model.project = await _context.projects.FindAsync(pid);
           
            
                model.projectGroup = new List<ProjectGroup>();

                ProjectGroup group1 = new ProjectGroup();
                group1.ProjectId = pid;
                group1.StudentId = model.project.Student1;
                group1.PQ1 = QSt11;
                group1.PQ2 = QSt21;
                group1.PQ3 = QSt31;
                group1.PQ4 = QSt41;
                group1.PQ5 = QSt51;
                group1.DoctorGrade = QSt11 + QSt21 + QSt31 + QSt41 + QSt51;
                group1.committeGrade = 0;

                if (op == 1)
                    _context.Add(group1);
                else
                    _context.Update(group1);

                await _context.SaveChangesAsync();
                ProjectGroup group2 = new ProjectGroup();
                group2.ProjectId = pid;
                group2.StudentId = model.project.Student2;
                group2.PQ1 = QSt12;
                group2.PQ2 = QSt22;
                group2.PQ3 = QSt32;
                group2.PQ4 = QSt42;
                group2.PQ5 = QSt52;
                group2.committeGrade = 0;
                group2.DoctorGrade = QSt12 + QSt22 + QSt32 + QSt42 + QSt52;

                if (op == 1)
                    _context.Add(group2);
                else
                    _context.Update(group2);
                await  _context.SaveChangesAsync();

                model.projectGroup.Add(group1);
                model.projectGroup.Add(group2);
               

                    if (model.project.Student3 != null)
                {
                    ProjectGroup group3 = new ProjectGroup();
                    group3.ProjectId = pid;
                    group3.StudentId = model.project.Student3;
                    group3.PQ1 = QSt13;
                    group3.PQ2 = QSt23;
                    group3.PQ3 = QSt33;
                    group3.PQ4 = QSt43;
                    group3.PQ5 = QSt53;
                    group3.committeGrade = 0;
                    group3.DoctorGrade = QSt13 + QSt23 + QSt33 + QSt43 + QSt53;

                    if (op == 1)
                        _context.Add(group3);
                    else
                        _context.Update(group3);
                    await  _context.SaveChangesAsync();
                    model.projectGroup.Add(group3);
                }
                if (model.project.Student4 != null)
                {
                    ProjectGroup group4 = new ProjectGroup();
                    group4.ProjectId = pid;
                    group4.StudentId = model.project.Student4;
                    group4.PQ1 = QSt14;
                    group4.PQ2 = QSt24;
                    group4.PQ3 = QSt34;
                    group4.PQ4 = QSt44;
                    group4.PQ5 = QSt54;
                    group4.committeGrade = 0;
                    group4.DoctorGrade = QSt14 + QSt24 + QSt34 + QSt44 + QSt54;

                    if (op == 1)
                        _context.Add(group4);
                    else
                        _context.Update(group4);
                    await _context.SaveChangesAsync();
                    model.projectGroup.Add(group4);
                }
                model.projectAssessment.ProjectId = pid;
                model.projectAssessment.DoctorApproved = model.project.DoctorId;
                if (op == 1)
                    _context.Add(model.projectAssessment);
                else
                    _context.Update(model.projectAssessment);
                 await _context.SaveChangesAsync();
                
               
                
               
            

            //if (ModelState.IsValid)
            //{
            //    _context.Add(projectAssessment);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["ProjectId"] = new SelectList(_context.projects, "Id", "ProjectTitle", projectAssessment.ProjectId);
            ViewBag.StudentId1 = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == model.project.Student1)?.DoctorId;
            ViewBag.StudentId2 = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == model.project.Student2)?.DoctorId;
            ViewBag.StudentId3 = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == model.project.Student3)?.DoctorId;
            ViewBag.StudentId4 = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == model.project.Student4)?.DoctorId;
            ViewBag.Student1Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == model.project.Student1)?.DoctorName;
            ViewBag.Student2Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == model.project.Student2)?.DoctorName;
            ViewBag.Student3Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == model.project.Student3)?.DoctorName;
            ViewBag.Student4Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == model.project.Student4)?.DoctorName;
            ViewBag.DoctorName = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == model.project.DoctorId)?.DoctorName;
            ViewBag.rate1 = new SelectList(_context.Ratings, "Id", "Rate", model.projectAssessment.PQ1);
            ViewBag.rate2 = new SelectList(_context.Ratings, "Id", "Rate", model.projectAssessment.PQ2);
            ViewBag.rate3 = new SelectList(_context.Ratings, "Id", "Rate", model.projectAssessment.PQ3);
            ViewBag.rate4 = new SelectList(_context.Ratings, "Id", "Rate", model.projectAssessment.PQ4);
            ViewBag.rate5 = new SelectList(_context.Ratings, "Id", "Rate", model.projectAssessment.PQ5);
            ViewBag.rate6 = new SelectList(_context.Ratings, "Id", "Rate", model.projectAssessment.PQ6);
            ViewBag.rate7 = new SelectList(_context.Ratings, "Id", "Rate", model.projectAssessment.PQ7);

            return View(model);
        }
         [HttpGet]
        public async Task<IActionResult> ProjectAssessmentFormStd(int id)
        {
            RetrieveSessionData();
            var project = await _context.projects.FindAsync(id);
            var projectAssess = await _context.ProjectAssessments.Where(tbl => tbl.ProjectId == id).FirstOrDefaultAsync();
            ViewBag.ProjectTitle = project.ProjectTitle.ToString();

            switch (projectAssess.PQ1)
            {
                case 0: ViewBag.AssessPQ10 = projectAssess.PQ1; break;

                case 1: ViewBag.AssessPQ11 = projectAssess.PQ1; break;
                case 2: ViewBag.AssessPQ12 = projectAssess.PQ1; break;
                case 3: ViewBag.AssessPQ13 = projectAssess.PQ1; break;
                default:
                    ViewBag.Assess4 = projectAssess.PQ1;
                    break;
            }
            switch (projectAssess.PQ2)
            {
                case 4: ViewBag.AssessPQ20 = projectAssess.PQ2; break;
                case 1: ViewBag.AssessPQ21 = projectAssess.PQ2; break;
                case 2: ViewBag.AssessPQ22 = projectAssess.PQ2; break;
                case 3: ViewBag.AssessPQ23 = projectAssess.PQ2; break;
                default:
                    ViewBag.AssessPQ24 = projectAssess.PQ2;
                    break;
            }
           
            ViewBag.Student1Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student1)?.DoctorName;
            ViewBag.Student2Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student2)?.DoctorName;
            ViewBag.Student3Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student3)?.DoctorName;
            ViewBag.Student4Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student4)?.DoctorName;
            ViewBag.DoctorId = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.DoctorId)?.DoctorName;
            //ViewBag.ProjectId = new SelectList(_context.projects, "Id", "ProjectTitle");
            ViewBag.read = isClose(id);
            return View(projectAssess);

        }

    }
}
