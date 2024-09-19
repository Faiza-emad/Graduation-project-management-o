using System.ComponentModel.DataAnnotations;

namespace Project2023.Models
{
    public class Dept
    {
        public int Id { get; set; }
        [Display(Name = "اسم القسم")]
        public string? DeptName { get; set; }
        [Display(Name = "رئيس القسم")]
        public int HOD { get; set; } //The Id of the HOD
    }
}
