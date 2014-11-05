namespace TimeManagerLib.ViewModel
{
    public interface IWorkbookViewModelFactory
    {
        WorkbookViewModel CreateViewModel();
        void Release(WorkbookViewModel viewModel);
    }
}