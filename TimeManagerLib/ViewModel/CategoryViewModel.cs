using GalaSoft.MvvmLight;
using TimeManager.Core;

namespace TimeManagerLib.ViewModel
{
    public class CategoryViewModel : ViewModelBase
    {
        public Category Category { get; private set; }

        public CategoryViewModel(Category category)
        {
            Category = category;
        }

        public string Name
        {
            get { return Category.Name; }
        }
    }
}
