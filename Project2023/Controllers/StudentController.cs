using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project2023.Models;
using Project2023.ViewModels;

namespace Project2023.Controllers
{
    public class StudentController : Controller
    {
        private void RetrieveSessionData()
        {
            TempData["name"] = HttpContext.Session.GetString("name");
            TempData["img"] = HttpContext.Session.GetString("img");
            TempData["dept"] = HttpContext.Session.GetString("dept");
        }
        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ApplicationDbContext _context { get; }
        public async Task<IActionResult> Index(int id)
        {
            RetrieveSessionData();
            //if (HttpContext.Session.GetString("id") == null)
            //    return RedirectToAction("Login", "DefaultUser");

           
            var applicationDbContext = _context.ApplicationUsers.Where(col => col.UserType == id).Include(a => a.UserDept);
            ViewBag.id = id;
            return View(await applicationDbContext.ToListAsync());



        }
        [HttpGet]
        public IActionResult ApprovalForm()
        {
            RetrieveSessionData();

            ViewBag.CourseId= new SelectList(_context.courses,"Id","CourseName");
            ViewBag.DoctorId = new SelectList(_context.ApplicationUsers.Where(tbl=> tbl.UserType==1), "Id", "DoctorName");
          

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApprovalForm(ProjectViewModel model)
        {
            // .........................................
            Project project = new Project();
         
            var check1 = _context.ApplicationUsers.Where(tbl => tbl.DoctorId == model.Student1 && tbl.UserType == 2).AsNoTracking().FirstOrDefault();
            if (check1 == null)
            {
                ViewBag.error = "الطالب غير مسجَل";
                return View(model);
            }
            else
            {
                project.Student1 = check1.Id;
                //project.StudentName1 = check1.DoctorName ;
            }
            
            var check2 = _context.ApplicationUsers.Where(tbl => tbl.DoctorId == model.Student2 && tbl.UserType == 2).AsNoTracking().FirstOrDefault();
            if (check2 == null)
            {
                ViewBag.error = "الطالب غير مسجَل";
                return View(model);
            }
            else
            {
                project.Student2 = check2.Id;
            }

            if (model.Student3 != null)
            {
                var check3 = _context.ApplicationUsers.Where(tbl => tbl.DoctorId == model.Student3 && tbl.UserType == 2).AsNoTracking().FirstOrDefault();
                if (check3 == null)
                {
                    ViewBag.error = "الطالب غير مسجَل";
                    return View(model);
                }
                else
                {
                    project.Student3 = check3.Id;
                }
            }
            else
            {
                project.Student3 = null;
            }

            if (model.Student4 != null)
            {
                var check4 = _context.ApplicationUsers.Where(tbl => tbl.Id == model.Student4 && tbl.UserType == 2).AsNoTracking().FirstOrDefault();
                if (check4 == null)
                {
                    ViewBag.error = "الطالب غير مسجَل";
                    return View(model);
                }
                else
                {
                    project.Student4 = check4.Id;
                }
            }
            else
            {
                project.Student4 = null;
            }

            //......................................................................................
            string path = UploadImage(model);  // add design
            project.Id = model.Id;
            project.Address = model.Address;
            project.WorkNature = model.WorkNature;
            project.DoctorId = model.DoctorId;
            project.Closed = 0;
            project.Decision = 0;
            project.Tel = model.Tel;
            project.Email = model.Email;
            project.Fax = model.Fax;
            project.CourseId = model.CourseId;  // must be solve 
            project.PImage = path;
            project.Place = model.Place;
            project.Objective=model.Objective;
            project.ProjectTitle = model.ProjectTitle;
         
            switch (DateTime.Now.Month)
            {
                case 10: case 11: case 12: case 1: project.StudySem = 1; break;
                case 2: case 3: case 4: case 5: case 6:  project.StudySem = 2; break;
                default:
                    project.StudySem = 3;
                    break;
            }

            project.StudyYear = DateTime.Now.Year;
            project.Summary = model.Summary;
            project.Target = model.Target;
            //project.percentage = 0;
            _context.Add(project);
            await _context.SaveChangesAsync();
           

            return View( model);
        }

        [HttpGet]
        public async Task<IActionResult> EditApprovalForm(int? id)
        {
            RetrieveSessionData();
            if (id == null || _context.projects == null)
            {
                return NotFound();
            }
            var project = await _context.projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            ViewBag.CourseId = new SelectList(_context.courses, "Id", "CourseName", project.CourseId);
            ViewBag.DoctorId = new SelectList(_context.ApplicationUsers, "Id", "DoctorName", project.DoctorId);
            ViewBag.Student1 = new SelectList(_context.ApplicationUsers, "Id", "DoctorName", project.Student1);
            ViewBag.Student2 = new SelectList(_context.ApplicationUsers, "Id", "DoctorName", project.Student2);
            ViewBag.Student3 = new SelectList(_context.ApplicationUsers, "Id", "DoctorName", project.Student3);
            ViewBag.Student4= new SelectList(_context.ApplicationUsers, "Id", "DoctorName", project.Student4);
            return View("EditApprovalForm", project);

        }
        ///////////////////////////////////////////////////////////////////////////////////////
        
        [HttpGet]
        public async Task<IActionResult> ApprovalFormDoc(int id)
        {
            RetrieveSessionData();

            
            if (id == 0 || _context.projects == null)
            {
                return NotFound();
            }

            
            var project = await _context.projects.FindAsync(id);

            
            if (project == null)
            {
                return NotFound();
            }

            ViewBag.DoctorName = _context.ApplicationUsers.FirstOrDefault(u => u.Id == project.DoctorId)?.DoctorName;
            ViewBag.CourseName = _context.courses.FirstOrDefault(c => c.Id == project.CourseId)?.CourseName;
            ViewBag.StudentId1 = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student1)?.DoctorId;
            ViewBag.StudentId2 = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student2)?.DoctorId;
            ViewBag.StudentId3 = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student3)?.DoctorId;
            ViewBag.StudentId4 = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student4)?.DoctorId;
            ViewBag.Student1Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student1)?.DoctorName;
            ViewBag.Student2Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student2)?.DoctorName;
            ViewBag.Student3Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student3)?.DoctorName;
            ViewBag.Student4Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student4)?.DoctorName;

            
            return View("ApprovalFormDoc", project);
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditApprovalForm(int id, [Bind("Id,ProjectTitle,Place,Address,Tel,Fax,Email,WorkNature,Objective,Target,Summary,Decision,StudyYear,StudySem,PImage,percentage,Closed,CourseId,DoctorId,Student1,Student2,Student3,Student4")] Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
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
            ViewBag.CourseId = new SelectList(_context.courses, "Id", "CourseName", project.CourseId);
            ViewBag.DoctorId = new SelectList(_context.ApplicationUsers, "Id", "DoctorName", project.DoctorId);
            ViewBag.Student1 = new SelectList(_context.ApplicationUsers, "Id", "DoctorName", project.Student1);
            ViewBag.Student2 = new SelectList(_context.ApplicationUsers, "Id", "DoctorName", project.Student2);
            ViewBag.Student3 = new SelectList(_context.ApplicationUsers, "Id", "DoctorName", project.Student3);
            ViewBag.Student4 = new SelectList(_context.ApplicationUsers, "Id", "DoctorName", project.Student4);
            return View(project);

        }
        private bool ProjectExists(int id)
        {
            return (_context.projects?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private string UploadImage(ProjectViewModel model)
        {
            var file = HttpContext.Request.Form.Files;
            if (file.Count() > 0)
            {
                string Imagename = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                var fileStream = new FileStream(Path.Combine(@"wwwroot/", "Images", Imagename), FileMode.Create);
                file[0].CopyTo(fileStream);
                model.PImage = Imagename;
            }
            //else if (model.PImage == null && model.DeptId == null)
            //{
            //    model.PImage = "defaultimage.jpg";
            //}
            else
            {
                //model.PImage = model.PImage;
                model.PImage = "defaultimage.jpg";
            }

            return model.PImage;
        }

        private string UploadImage(Project model)
        {
            var file = HttpContext.Request.Form.Files;
            if (file.Count() > 0)
            {
                string Imagename = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                var fileStream = new FileStream(Path.Combine(@"wwwroot/", "Images", Imagename), FileMode.Create);
                file[0].CopyTo(fileStream);
                model.PImage = Imagename;
            }

            else
            {
                //model.PImage = model.PImage;
                model.PImage = "defaultimage.jpg";
            }

            return model.PImage;
        }

        public async Task<IActionResult> ApprovalFormView(int id)
        {
            RetrieveSessionData();


            if (id == 0 || _context.projects == null)
            {
                return NotFound();
            }


            var project = await _context.projects.FindAsync(id);


            if (project == null)
            {
                return NotFound();
            }

            ViewBag.DoctorName = _context.ApplicationUsers.FirstOrDefault(u => u.Id == project.DoctorId)?.DoctorName;
            ViewBag.CourseName = _context.courses.FirstOrDefault(c => c.Id == project.CourseId)?.CourseName;
            ViewBag.StudentId1 = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student1)?.DoctorId;
            ViewBag.StudentId2 = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student2)?.DoctorId;
            ViewBag.StudentId3 = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student3)?.DoctorId;
            ViewBag.StudentId4 = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student4)?.DoctorId;
            ViewBag.Student1Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student1)?.DoctorName;
            ViewBag.Student2Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student2)?.DoctorName;
            ViewBag.Student3Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student3)?.DoctorName;
            ViewBag.Student4Name = _context.ApplicationUsers.FirstOrDefault(tbl => tbl.Id == project.Student4)?.DoctorName;


            return View("ApprovalFormDoc", project);
        }

    }
}
