namespace TimeManager.Presentation.ViewModels
{
    public interface IWorkbookViewModelFactory
    {
        WorkbookViewModel CreateViewModel();
        void Release(WorkbookViewModel viewModel);
    }
}