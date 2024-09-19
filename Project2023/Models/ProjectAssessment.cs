using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace Project2023.Models
{
    public class ProjectAssessment
    {
        
        public int? PQ1 { get; set; }
        public int? PQ2 { get; set; }
        public int? PQ3 { get; set;}
        public int? PQ4 { get; set;}
        public int? PQ5 { get; set;}
        public int? PQ6 { get; set;}
        public int? PQ7 { get; set;}
        public int? DoctorApproved { get; set;}
        [Key]
        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public Project? Project_ProjectAss { get; set; }

    }
}
