using Project2023.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Project2023.ViewModels
{
    public class ApplicationUsersViewModel
    {
        public int Id { get; set; }
        [Display(Name = "الرقم الوظيفي")]
        public int? DoctorId { get; set; }
        [Display(Name = "الاسم")]
        public string? DoctorName { get; set; }
        [Display(Name = "البريد الالكتروني")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Display(Name = "كلمة المرور")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Display(Name = "تأكيد كلمة المرور")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password not Match")]
        public string? ConfirmPassword { get; set; }
        [Display(Name = "الصورة الشخصية")]
        public string? DImage { get; set; }
        //[Display(Name = "نوع المستخدم")]
        //public int UserType { get; set; } //0 :Admin 1:doctor 2:student
        //[Display(Name = "رئيس قسم")]
        //public bool HOD { get; set; }
        [Display(Name = "الدائرة")]
        public int? DeptId { get; set; }

        [ForeignKey("DeptId")]

        public Dept? UserDept { get; set; }
        public List<ProjectGroup>? ProjectStudents { get; set; }

    }
}
