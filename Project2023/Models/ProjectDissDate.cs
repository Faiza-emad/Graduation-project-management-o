using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project2023.Models
{
    public class ProjectDissDate
    {
       
        public string? GradDate { get; set;}
        public string? GradDay { get; set; }
        public string? GradPlace { get; set; }
        //public string? Status { get; set; }
        public int? Sup1 { get; set; }
        [ForeignKey("Sup1")]
        public ApplicationUser? User_Sup1 { get; set; }
        public int? Sup2 { get; set; }
        [ForeignKey("Sup2")]
        public ApplicationUser? User_Sup2 { get; set; }
        public int? Sup3 { get; set; }
        [ForeignKey("Sup3")]
        public ApplicationUser? User_Sup3 { get; set; }
        [Key]
        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public Project? Project_ProjectDiss { get; set; }
    }
}
