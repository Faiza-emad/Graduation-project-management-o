using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Project2023.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Display(Name = "رقم المادة")]
        public int? CourseId { get; set; }
        [Display(Name = "اسم المادة")]
        public string? CourseName { get; set; }
        [Display(Name = "الدائرة")]
        public int? DeptId { get; set; }

        [ForeignKey("DeptId")]
        public Dept? CourseDept { get; set; }
    }
}
