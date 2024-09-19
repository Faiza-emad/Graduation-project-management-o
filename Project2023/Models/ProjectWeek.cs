using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project2023.Models
{
    public class ProjectWeek
    {

        [Display(Name = "الاسبوع الاول")]
        public string? Week1 { get; set; } = "";
        [Display(Name = " تاريخ الاسبوع الاول")]
        public string? Date1 { get; set; } = "";
        [Display(Name = "الاسبوع الثاني")]
        public string? Week2 { get; set; } = "";
        [Display(Name = " تاريخ الاسبوع الثاني")]
        public string? Date2 { get; set; } = "";
        [Display(Name = " الاسبوع الثالث")]
        public string? Week3 { get; set; } = "";
        [Display(Name = " تاريخ الاسبوع الثالث")]
        public string? Date3 { get; set; } = "";
        [Display(Name = " الاسبوع الرابع")]
        public string? Week4 { get; set; } = "";
        [Display(Name = " تاريخ الاسبوع الرابع")]
        public string? Date4 { get; set; } = "";
        [Display(Name = " الاسبوع الخامس")]
        public string? Week5 { get; set; } = "";
        [Display(Name = " تاريخ الاسبوع الخامس")]
        public string? Date5 { get; set; } = "";
        [Display(Name = "  الاسبوع السادس")]
        public string? Week6 { get; set; } = "";
        [Display(Name = " تاريخ الاسبوع السادس")]
        public string? Date6 { get; set; } = "";
        [Display(Name = "  الاسبوع السابع")]
        public string? Week7 { get; set; } = "";
        [Display(Name = " تاريخ الاسبوع السابع")]
        public string? Date7 { get; set; } = "";
        [Display(Name = "  الاسبوع الثامن")]
        public string? Week8 { get; set; } = "";
        [Display(Name = " تاريخ الاسبوع الثامن")]
        public string? Date8 { get; set; } = "";
        [Display(Name = "  الاسبوع التاسع")]
        public string? Week9 { get; set; } = "";
        [Display(Name = " تاريخ الاسبوع التاسع")]
        public string? Date9 { get; set; } = "";
        [Display(Name = "  الاسبوع العاشر")]
        public string? Week10 { get; set; } = "";
        [Display(Name = " تاريخ الاسبوع العاشر")]
        public string? Date10{ get; set; } = "";
        [Display(Name = "  الاسبوع الحادي عشر")]
        public string? Week11 { get; set; } = "";
        [Display(Name = " تاريخ الاسبوع الحادي عشر")]
        public string? Date11 { get; set; } = "";
        [Display(Name = "  الاسبوع الثاني عشر")]
        public string? Week12 { get; set; } = "";
        [Display(Name = " تاريخ الاسبوع الثاني عشر ")]
        public string? Date12 { get; set; } = "";
        public int? percentage1 { get; set; } = 0;
        public int? percentage2 { get; set; } = 0;
        public int? percentage3 { get; set; } = 0;
        public int? percentage4 { get; set; } = 0;
        public int? percentage5 { get; set; } = 0;
        public int? percentage6 { get; set; } = 0;
        public int? percentage7 { get; set; } = 0;
        public int? percentage8 { get; set; } = 0;
        public int? percentage9 { get; set; } = 0;
        public int? percentage10 { get; set; } = 0;
        public int? percentage11 { get; set; } = 0;
        public int? percentage12 { get; set; } = 0;
        [Key]
        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public Project? Project_ProjectWeek { get; set; }

    }
}
