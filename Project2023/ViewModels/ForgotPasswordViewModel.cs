using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Project2023.ViewModels
{
    public class ForgotPasswordViewModel
    {
        public int id { get; set; }

        [Required(ErrorMessage = "!الرجاء ادخال كلمة المرور")]
        [Display(Name = "رمز الدخول الجديد")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "!الرجاء ادخال كلمة المرور")]
        [Display(Name = "تاكيد رمز الدخول الجديد")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "كلمة المرور غير متطابقة")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Please Enter Email!")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }



    }
}
