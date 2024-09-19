namespace Project2023.Models
{
    public class ProjectGroup
    {
        public int ProjectId { get; set; }
        public Project? Project_ProjectGroup { get; set; }
        public int? StudentId { get; set; }
        public ApplicationUser? Project_Student { get; set; }
        public int? PQ1 { get; set;}
        public int? PQ2 { get; set; }
        public int? PQ3 { get; set; }
        public int? PQ4 { get; set; }
        public int? PQ5 { get; set; }
        public int? DoctorGrade { get; set;}
        public int? committeGrade { get;set; }
    }
}
