using System.ComponentModel.DataAnnotations;

namespace Project2023.ViewModels
{
    public class SendEmail
    {
        public int id{ get; set; }
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }
    }
}
