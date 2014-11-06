using Microsoft.Practices.Prism.Mvvm;
using TimeManager.Core;

namespace TimeManager.Presentation.ViewModels
{
    public class ProjectViewModel : BindableBase
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
