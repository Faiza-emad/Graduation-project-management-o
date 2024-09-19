using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Project2023.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please Enter Email!")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter Password!")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
