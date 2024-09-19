using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project2023.Models
{
    public class ProjectCommDecision
    {
       
        public string? AssDate { get; set; }
        public int? Q1 { get; set; }
        public int? Q2 { get; set;}
        public int? Q3 { get; set;}
        public int? Q4 { get; set;}
        public int? Q5 { get; set;}
        public int? Q6 { get; set;}
        public int? Q7 { get; set;}
        public int? Q8 { get; set;}
        public int? S1 { get; set;}
        public int? S2 { get; set;}
        public int? S3 { get; set;}
        public int? S4 { get; set;}
        public int? S5 { get; set;}
        public int? S6 { get; set;}
        [Key]
        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public Project? Project_ProjectComm { get; set; }
    }
}
