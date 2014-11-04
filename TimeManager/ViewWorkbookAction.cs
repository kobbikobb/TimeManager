using System;
using TimeManagerLib.View;
using TimeManagerLib.ViewModel;

namespace TimeManager
{
    public class ViewWorkbookAction : ITrayAction
    {
        private readonly IWorkbookViewModelFactory workbookViewModelFactory;
        private WindowView window;

        public ViewWorkbookAction(IWorkbookViewModelFactory workbookViewModelFactory)
        {
            if (workbookViewModelFactory == null) throw new ArgumentNullException("workbookViewModelFactory");
            this.workbookViewModelFactory = workbookViewModelFactory;
        }

        public string Name
        {
            get { return "View workbook"; }
        }

        public void Execute()
        {
            if (window == null)
            {
                window = new WindowView { Title = Name, Width = 800, Height = 600};
            }
            if (window.IsVisible)
            {
                window.Focus();
                return;
            }

            window.DataContext = workbookViewModelFactory.CreateViewModel();
            window.Show();
            window.Closing += (sender, args) =>
            {
                window = null;
            };
            System.Windows.Threading.Dispatcher.Run();
        }
    }
}
