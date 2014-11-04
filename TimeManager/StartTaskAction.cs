using System.Net.Mime;
using TimeManagerLib.View;
using TimeManagerLib.ViewModel;

namespace TimeManager
{
    public class StartTaskAction : ITrayAction
    {
        private WindowView window;
        private StartTaskViewModel lastStartTaskViewModel;

        public StartTaskAction()
        {
         
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
            window.Show();
            window.Closing += (sender, args) =>
            {
                lastStartTaskViewModel = startTaskViewModel;
                window = null;
            };
            System.Windows.Threading.Dispatcher.Run();
        }

        private StartTaskViewModel CreateStartTaskViewModel()
        {
            var startTaskViewModel = new StartTaskViewModel();
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
