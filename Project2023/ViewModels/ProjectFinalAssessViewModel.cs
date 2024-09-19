using Project2023.Models;

namespace Project2023.ViewModels
{
    public class ProjectFinalAssessViewModel
    {
        public ProjectAssessment? projectAssessment { get; set; }
        
        public List<ProjectGroup>? projectGroup { get; set; }
        public Project? project { get; set; }
    }
}
