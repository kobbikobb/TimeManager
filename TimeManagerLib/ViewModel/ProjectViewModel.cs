using GalaSoft.MvvmLight;
using TimeManager.Core;

namespace TimeManagerLib.ViewModel
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
