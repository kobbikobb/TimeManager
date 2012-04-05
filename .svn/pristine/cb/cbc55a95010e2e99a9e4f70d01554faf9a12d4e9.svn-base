using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using TimeManagerLib.Model;

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
