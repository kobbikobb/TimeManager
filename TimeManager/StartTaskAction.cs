using System;
using System.Net.Mime;
using TimeManagerLib.View;
using TimeManagerLib.ViewModel;

namespace TimeManager
{
    public class StartTaskAction : ITrayAction
    {
        private readonly IStartTaskViewModelFactory startTaskViewModelFactory;
        private WindowView window;
        private StartTaskViewModel lastStartTaskViewModel;

        public StartTaskAction(IStartTaskViewModelFactory startTaskViewModelFactory)
        {
            if (startTaskViewModelFactory == null) throw new ArgumentNullException("startTaskViewModelFactory");
            this.startTaskViewModelFactory = startTaskViewModelFactory;
        }

        public string Name
        {
            get { return "Start task"; }
        }

        public void Execute()
        {
            if (window == null)
            {
                window = new WindowView {Title = Name, Width = 400, Height = 300};
            }
            if (window.IsVisible)
            {
                window.Focus();
                return;
            }
            
            var startTaskViewModel = CreateStartTaskViewModel();

            window.DataContext = startTaskViewModel;
            if(window.ShowDialog() == true)
            {
                lastStartTaskViewModel = startTaskViewModel;
            };
            window = null;
        }

        private StartTaskViewModel CreateStartTaskViewModel()
        {
            var startTaskViewModel = startTaskViewModelFactory.CreateViewModel();
            if (lastStartTaskViewModel != null)
            {
                if (lastStartTaskViewModel.Project != null)
                {
                    startTaskViewModel.SelectProject(lastStartTaskViewModel.Project.Id);
                }
                if (lastStartTaskViewModel.Category != null)
                {
                    startTaskViewModel.SelectCategory(lastStartTaskViewModel.Category.Id);
                }
            }
            return startTaskViewModel;
        }
    }
}
