using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExcelDataReader;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Project2023.Models;
using Project2023.ViewModels;

namespace Project2023.Controllers
{
    public class ApplicationUsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUsersController(ApplicationDbContext context)
        {
            _context = context;
        }
        private void SessionData()
        {
            TempData["name"] = HttpContext.Session.GetString("name");
            TempData["img"] = HttpContext.Session.GetString("img");
            TempData["dept"] = HttpContext.Session.GetString("dept");
        }
        // GET: ApplicationUsers
        public async Task<IActionResult> Index(int id)
        {
            SessionData();
            //if (HttpContext.Session.GetString("id") == null)
            //    return RedirectToAction("Login", "DefaultUser");

            //TempData["name"] = HttpContext.Session.GetString("name");
            //TempData["img"] = HttpContext.Session.GetString("img");
            //TempData["dept"] = HttpContext.Session.GetString("dept");

            var applicationDbContext = _context.ApplicationUsers.Where(col => col.UserType == id).Include(a => a.UserDept);
                 ViewBag.id = id;
                return View(await applicationDbContext.ToListAsync());
            
            
            
        }

        // GET: ApplicationUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            SessionData();
            if (id == null || _context.ApplicationUsers == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUsers
                .Include(a => a.UserDept)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        // GET: ApplicationUsers/Create
        public IActionResult Create()
        {
            SessionData();
            ViewBag.DeptId = new SelectList(_context.depts, "Id", "DeptName");
            return View();
        }

        // POST: ApplicationUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( ApplicationUsersViewModel model)
        {
            string path = UploadImage(model);
            ApplicationUser applicationUser = new ApplicationUser();
            applicationUser.Id = model.Id;
            applicationUser.DoctorId = model.DoctorId;
            applicationUser.DoctorName = model.DoctorName;
            applicationUser.HOD = false;
            applicationUser.Email= model.Email;
            applicationUser.UserType = 1;
            applicationUser.Password=model.Password;
            applicationUser.DeptId=model.DeptId;
            applicationUser.DImage = path;
            if (ModelState.IsValid)
            {
                _context.Add(applicationUser);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "ApplicationUsers",new {id =1});
            }
            ViewBag.DeptId = new SelectList(_context.depts, "Id", "DeptName");
            return View(model);
        }

        

        // GET: ApplicationUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            SessionData();
            if (id == null || _context.ApplicationUsers == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUsers.FindAsync(id);
            if (applicationUser == null)
            {
                return NotFound();
            }
            ViewBag.DeptId = new SelectList(_context.depts, "Id", "DeptName", applicationUser.DeptId);
            return View("Edit",applicationUser);
        }

        // POST: ApplicationUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( ApplicationUser model)
        {
          //  string path = UploadImage(model);
            ApplicationUser applicationUser = await _context.ApplicationUsers.Where(tbl => tbl.Id == model.Id).AsNoTracking().FirstOrDefaultAsync();
            model.Password = applicationUser.Password;
            model.UserType = 1;
            model.HOD = false;
           

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationUserExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "ApplicationUsers", new { id = 1 });
            }
            ViewBag.DeptId = new SelectList(_context.depts, "Id", "DeptName", model.DeptId);
            return View(model);
        }
     
        // GET: ApplicationUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            SessionData();
            if (id == null || _context.ApplicationUsers == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUsers
                .Include(a => a.UserDept)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        // POST: ApplicationUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ApplicationUsers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ApplicationUsers'  is null.");
            }
            var applicationUser = await _context.ApplicationUsers.FindAsync(id);
            if (applicationUser != null)
            {
                _context.ApplicationUsers.Remove(applicationUser);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "ApplicationUsers", new { id = 1 });
        }
        private string  UploadImage(ApplicationUsersViewModel model)
        {
            var file = HttpContext.Request.Form.Files;
            if (file.Count() > 0)
            {
                string Imagename = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                var fileStream = new FileStream(Path.Combine(@"wwwroot/", "Images", Imagename), FileMode.Create);
                file[0].CopyTo(fileStream);
                model.DImage = Imagename;
            }
            else if (model.DImage == null && model.DeptId == null)
            {
                model.DImage = "defaultimage.jpg";
            }
            else
            {
                model.DImage = model.DImage;
            }

            return model.DImage;
        }

        private string UploadImage(ApplicationUser model)
        {
            var file = HttpContext.Request.Form.Files;
            if (file.Count() > 0)
            {
                string Imagename = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                var fileStream = new FileStream(Path.Combine(@"wwwroot/", "Images", Imagename), FileMode.Create);
                file[0].CopyTo(fileStream);
                model.DImage = Imagename;
            }
            else if (model.DImage == null && model.DeptId == null)
            {
                model.DImage = "defaultimage.jpg";
            }
            else
            {
                model.DImage = model.DImage;
            }

            return model.DImage;
        }
        private bool ApplicationUserExists(int id)
        {
          return (_context.ApplicationUsers?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public IActionResult UploadStudents()
        {
            SessionData();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadStudents(IFormFile file)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            if (file != null && file.Length > 0)
            {
                var uploadsFolder = $"{Directory.GetCurrentDirectory()}\\wwwroot\\Uploads\\";

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var filePath = Path.Combine(uploadsFolder, file.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        do
                        {
                            bool isHeaderSkipped = false;

                            while (reader.Read())
                            {
                                if (!isHeaderSkipped)
                                {
                                    isHeaderSkipped = true;
                                    continue;
                                }

                                ApplicationUser s = new ApplicationUser();
                                s.DoctorId = Convert.ToInt32(reader.GetValue(0).ToString());
                                s.DoctorName = reader.GetValue(1).ToString();
                                s.Email = reader.GetValue(2).ToString();
                                s.Password = reader.GetValue(3).ToString();
                                s.UserType =2;
                                s.DeptId = Convert.ToInt32(reader.GetValue(5).ToString());
                                s.DImage = "Unknown.png";
                                s.HOD = false;


                                _context.Add(s);
                                await _context.SaveChangesAsync();
                            }
                        } while (reader.NextResult());

                        return RedirectToAction("Index", "ApplicationUsers", new { id = 2 });

                    }
                }
            }
            else
                ViewBag.Message = "empty";
            return View();
        }
    }
}
