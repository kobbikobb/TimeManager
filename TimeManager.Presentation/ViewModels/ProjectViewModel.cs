using TimeManager.Core;

namespace TimeManager.Presentation.ViewModels
{
    public class ProjectViewModel : ViewModelBase
    {
        public Project Project { get; private set; }

        public ProjectViewModel(Project project)
        {
            Project = project;
        }

        public string Name
        {
            get { return Project.Name; }
        }
    }
}
