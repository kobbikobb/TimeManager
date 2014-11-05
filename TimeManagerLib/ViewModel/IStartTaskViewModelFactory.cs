namespace TimeManagerLib.ViewModel
{
    public interface IStartTaskViewModelFactory
    {
        StartTaskViewModel CreateViewModel();
        void Release(StartTaskViewModel viewModel);
    }
}