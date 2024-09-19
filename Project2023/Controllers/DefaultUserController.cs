using EmailService.Helpers.EmailHelper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project2023.Models;
using Project2023.ViewModels;

namespace Project2023.Controllers
{
    public class DefaultUserController : Controller
    {
        private readonly ApplicationDbContext _context;
         IEmailSender emailSender;

        public DefaultUserController(ApplicationDbContext context, IEmailSender emailSender)
        {
            _context = context;
            this.emailSender = emailSender;
        }

        [HttpGet]
        public IActionResult Login()
        {
            HttpContext.Session.Clear();
            
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel collection)
        {
            try
            {
                var user = _context.ApplicationUsers.Include(tbl => tbl.UserDept)
                    .Where(tbl => tbl.Email == collection.Email && tbl.Password == collection.Password).FirstOrDefault();

                if (user == null)
                {
                    ViewBag.error = "Invalid user";
                    return View();
                }

                else
                {
                    int userType = user.UserType;   // 0  admin , 1 doctor , 2 student
                    //session
                    HttpContext.Session.SetString("id", user.Id + "");
                    HttpContext.Session.SetString("img", user.DImage + "");
                    HttpContext.Session.SetString("name", user.DoctorName + "");
                    HttpContext.Session.SetString("dept", user.UserDept.DeptName + "");


                    if (userType == 0)
                    {
                        return RedirectToAction("Index", "ApplicationUsers", new { id = 1 });
                    }
                    else if (userType == 1)
                    {

                        if (user.HOD) 
                        {
                            
                            var allProjects = await _context.projects.ToListAsync();

                            if (allProjects.Any())
                            {
                                return RedirectToAction("ProjectIndex", "Doctor");
                            }
                            else
                            {
                                return RedirectToAction("ProjectIndex", "Doctor");
                            }
                        }


                        var doctorProjects = await _context.projects
                            .Where(p => p.ProjectDoctor.Id == user.Id)
                            .ToListAsync();

                            if (doctorProjects.Any())
                            {
                                return RedirectToAction("ProjectIndex", "Doctor", new { id = user.Id });
                            }
                            else
                            {

                                return RedirectToAction("ProjectIndex", "Doctor");
                            }
                        }
                        else
                        {
                            var studentProjects = await _context.projects
                                .Where(p => p.Student1 == user.Id || p.Student2 == user.Id || p.Student3 == user.Id || p.Student4 == user.Id)
                                .ToListAsync();

                            if (studentProjects.Any())
                            {

                                var groupProject = studentProjects.FirstOrDefault(p => p.Student1 == user.Id || p.Student2 == user.Id || p.Student3 == user.Id || p.Student4 == user.Id);


                                return RedirectToAction("ProjectIndexStd", "Doctor", new { id = groupProject.Id });
                            }
                            else
                            {

                                return RedirectToAction("ProjectIndexStd", "Doctor");
                            }
                        }
                    }
                
            }
            
            catch
            {
                return View();
            }
        }
        
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                    var userId = HttpContext.Session.GetString("id");
                    var user = await _context.ApplicationUsers.FindAsync(int.Parse(userId));

                    if (user != null)
                    {
                        
                        if (user.Password == collection.CurrentPassword)
                        {
                        
                            user.Password = collection.NewPassword;
                            _context.Update(user);
                            await _context.SaveChangesAsync();

                        
                            return RedirectToAction("Index", "ApplicationUsers");
                        }
                        else
                        {
                            
                            ViewBag.error = "رمز المستخدم خطأ";
                            return View();
                        }
                    }
                    
                }
            }
            catch 
            {
                
                return View();
            }

            
            return View(collection);
        }
        [HttpGet]
        public IActionResult SendEmail()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SendEmail(SendEmail model)
        {
            emailSender.SendFirstEmail(model);
            return RedirectToAction(nameof(SendEmail));


        }
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.ApplicationUsers.FirstOrDefaultAsync(x => x.Email == model.Email);

                if (user != null)
                {
                    var token = Guid.NewGuid().ToString();
                    await _context.SaveChangesAsync();
                    return RedirectToAction("PasswordResetConfirmation");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email not found");
                }
            }
            return View(model);
        }


    }
}
