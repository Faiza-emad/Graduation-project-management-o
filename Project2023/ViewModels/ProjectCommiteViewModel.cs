using Project2023.Models;

namespace Project2023.ViewModels
{
    public class ProjectCommiteViewModel
    {
        public Project project { get; set; }
        public ProjectDissDate projectDissDate { get; set; }

        public ProjectCommDecision projectCommDecision { get; set; }
        public List<ProjectGroup>? projectGroup { get; set; }


    }
}
