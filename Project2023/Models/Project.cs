using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project2023.Models
{
    public class Project
    {
        public int Id { get; set; }
        [Display(Name = "عنوان المشروع ")]
        public string? ProjectTitle { get; set; }
        public string? Place { get; set; }
        public string? Address { get; set; }
        public string? Tel { get; set; }
        public string? Fax { get; set; }
        public string? Email { get; set; }
        public string? WorkNature { get; set; }
        public string? Objective { get; set; }
        public string? Target { get; set; }
        public string? Summary { get; set; }
        [Display(Name = "الحالة ")]
        public int? Decision { get; set; }
        public int? StudyYear { get; set; }
        public int? StudySem { get; set; }
        public string? PImage { get; set; }
        [Display(Name = "نسبة الانجاز ")]
        public int? percentage { get; set; }
        public int? Closed { get; set; } // 0:opened 1:closed
        public int? CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Course? ProjectCourse { get; set; }
        public int? DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public ApplicationUser? ProjectDoctor { get; set; }
        public int? Student1 { get; set; }
        [ForeignKey("Student1")]
        public ApplicationUser? ProjectStudent1 { get; set; }
        public int? Student2 { get; set; }
        [ForeignKey("Student2")]
        public ApplicationUser? ProjectStudent2 { get; set; }
        public int? Student3 { get; set; }
        [ForeignKey("Student3")]
        public ApplicationUser? ProjectStudent3 { get; set; }
        public int? Student4 { get; set; }
        [ForeignKey("Student4")]
        public ApplicationUser? ProjectStudent4 { get; set; }

        public List<ProjectGroup>? ProjectGroups { get; set; }

    }
}
