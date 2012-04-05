using System;
using System.Collections.Generic;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using PatternLib;
using TimeManagerLib.Model;
using TimeManagerLib.Data;
using GalaSoft.MvvmLight;
using TimeManagerLib.ViewModel.Extension;

namespace TimeManagerLib.ViewModel
{
    public class StartTaskViewModel : ViewModelBase, IRefreashable
    {
        public Task NewTask { get; private set; }

        public StartTaskViewModel()
        {

        }

        #region Properties

        public string Description { get; set; }

        private TimeSpan _started;
        public TimeSpan Started
        {
            get { return _started; }
            set
            {
                _started = value;

                RaisePropertyChanged("Started");
            }
        }

        private TimeSpan? _completed;
        public TimeSpan? Completed
        {
            get { return _completed; }
            set
            {
                _completed = value;

                if (_completed.HasValue)
                {
                    var hours = _completed.Value.Subtract(Started).TotalMinutes / 60;

                    WorkedHours = (decimal)hours;
                }
            }
        }

        private decimal _workedHours;
        public decimal WorkedHours
        {
            get { return _workedHours; }
            set
            {
                _workedHours = Math.Round(value, 2, MidpointRounding.ToEven);



                RaisePropertyChanged("WorkedHours");
            }
        }

        #endregion

        #region Project

        private List<Project> _projects;
        public List<Project> Projects
        {
            get
            {
                if(_projects == null)
                {
                    var repository = DependencyResolver.Resolve<ITimeManagerRepository>();
                    _projects = repository.GetProjects();
                }
               return _projects;
            }
            set
            {
                _projects = value;
            }
        }

        private Project _project;
        public Project Project
        {
            get { return _project; }
            set
            {
                _project = value;

                if (_project != null)
                {
                    //Here we have to load the project categories
                    var repository = DependencyResolver.Resolve<ITimeManagerRepository>();
                    Categories = repository.GetProjectCategories(_project);
                }
                else
                {
                    Categories = null;
                }
            }
        }

        public string ProjectName { get; set; }

        #endregion

        #region Category

        private List<Category> _categories;
        public List<Category> Categories
        {
            get { return _categories; }
            set 
            { 
                _categories = value;
                RaisePropertyChanged("Categories");
            }
        }

        public Category Category { get; set; }

        public string CategoryName { get; set; }

        #endregion

        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                    _saveCommand = new RelayCommand(Save);
                return _saveCommand;
            }
        }

        private void Save()
        {
            var task = new Task();

            if (Project == null)
            {
                if (string.IsNullOrEmpty(ProjectName))
                    throw new DataIntegrityException("Please fill in project name");

                Project = new Project() {Name = ProjectName};
            }
            if (Category == null)
            {
                if (string.IsNullOrEmpty(CategoryName))
                    throw new DataIntegrityException("Please fill in category name");

                Category = new Category() {Name = CategoryName};
            }

            Category.Project = Project;
            Category.IdProject = Project.Id;

            task.Category = Category;
            task.IdCategory = Category.Id;
            task.Description = Description ?? string.Empty;
            task.Started = DateTime.Today.Add(Started);

            if (Completed.HasValue)
                task.Completed = DateTime.Today.Add(Completed.Value);

            task.WorkedHours = WorkedHours;

            var repository = DependencyResolver.Resolve<ITimeManagerRepository>();
            repository.SaveTask(task);

            NewTask = task;
        }

        public void Refreash()
        {
            Started = DateTime.Now.TimeOfDay;
        }

        public override bool Equals(object obj)
        {
            return obj is StartTaskViewModel;
        }

        public override string ToString()
        {
            return "Start task";
        }
    }
}
