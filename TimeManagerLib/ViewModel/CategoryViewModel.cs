using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using TimeManagerLib.Model;

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
