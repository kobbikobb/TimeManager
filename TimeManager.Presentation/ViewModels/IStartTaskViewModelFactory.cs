namespace TimeManager.Presentation.ViewModels
{
    public interface IStartTaskViewModelFactory
    {
        StartTaskViewModel CreateViewModel();
        void Release(StartTaskViewModel viewModel);
    }
}