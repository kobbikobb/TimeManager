using System;
using System.Windows.Threading;
using TimeManager.Presentation.ViewModels;
using TimeManager.Presentation.Views;

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

            var viewModel = workbookViewModelFactory.CreateViewModel();
            window.DataContext = viewModel;
            window.Show();
            window.Closing += (sender, args) =>
            {
                window = null;
                workbookViewModelFactory.Release(viewModel);
            };
            Dispatcher.Run();
        }
    }
}
