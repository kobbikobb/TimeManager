using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using TimeManager.Core;
using TimeManager.Core.Repositories;

namespace TimeManager.Presentation.ViewModels
{
    public class StartTaskViewModel : BindableBase, IDataErrorInfo
    {
        private readonly ITimeManagerRepository timeManagerRepository;

        public StartTaskViewModel(ITimeManagerRepository timeManagerRepository)
        {
            if (timeManagerRepository == null) throw new ArgumentNullException("timeManagerRepository");
            this.timeManagerRepository = timeManagerRepository;

            Started = DateTime.Now.TimeOfDay;

            SaveCommand = new DelegateCommand(Save, () => Error == null);
            CancelCommand = new DelegateCommand(Cancel);
        }
        
        #region Properties

        public string Description { get; set; }

        private TimeSpan started;
        public TimeSpan Started
        {
            get { return started; }
            set
            {
                SetProperty(ref started, value);
            }
        }

        private TimeSpan? completed;
        public TimeSpan? Completed
        {
            get { return completed; }
            set
            {
                SetProperty(ref completed, value);

                if (completed.HasValue)
                {
                    var hours = completed.Value.Subtract(Started).TotalMinutes / 60;

                    WorkedHours = (decimal)hours;
                    OnPropertyChanged(() => WorkedHours);
                }
            }
        }

        private decimal workedHours;
        public decimal WorkedHours
        {
            get { return workedHours; }
            set
            {
                SetProperty(ref workedHours, value);
            }
        }

        public DelegateCommand SaveCommand { get; private set; }
        public DelegateCommand CancelCommand { get; private set; }
        public Action Close { get; set; }

        #endregion

        #region Project

        private List<Project> projects;
        public List<Project> Projects
        {
            get
            {
                if(projects == null)
                {
                    projects = timeManagerRepository.GetProjects();
                }
               return projects;
            }
        }

        private Project project;
        public Project Project
        {
            get { return project; }
            set
            {
                SetProperty(ref project, value);
                
                Categories = project != null ? project.Categories.ToList() : null;
                OnPropertyChanged(() => Categories);
            }
        }

        private string projectName;
        public string ProjectName
        {
            get { return projectName; }
            set
            {
                SetProperty(ref projectName, value);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region Category

        private List<Category> categories;
        public List<Category> Categories
        {
            get { return categories; }
            set 
            {
                SetProperty(ref categories, value);
            }
        }

        private Category category;
        public Category Category
        {
            get { return category; }
            set { SetProperty(ref category, value); }
        }

        private string categoryName;
        public string CategoryName
        {
            get { return categoryName; }
            set
            {
                SetProperty(ref categoryName, value);
                SaveCommand.RaiseCanExecuteChanged();                
            }
        }

        #endregion
        
        private void Save()
        {
            var task = new Task();

            if (Project == null)
            {
                if (string.IsNullOrEmpty(ProjectName))
                    throw new InvalidOperationException("Please fill in project name.");

                Project = new Project() {Name = ProjectName};
            }
            if (Category == null)
            {
                if (string.IsNullOrEmpty(CategoryName))
                    throw new InvalidOperationException("Please fill in category name.");

                Category = new Category() {Name = CategoryName};
            }

            Category.Project = Project;

            task.Category = Category;
            task.Description = Description ?? string.Empty;
            task.Started = DateTime.Today.Add(Started);

            if (Completed.HasValue)
                task.Completed = DateTime.Today.Add(Completed.Value);

            task.WorkedHours = WorkedHours;

            timeManagerRepository.SaveTask(task);

            Close();
        }
 
        public void SelectProject(decimal id)
        {
            Project = Projects.Single(x => x.Id == id);
        }

        public void SelectCategory(decimal id)
        {
            Category = Categories.Single(x => x.Id == id);
        }

        private void Cancel()
        {
            Close();
        }

        #region Validation

        public string this[string columnName]
        {
            get
            {
                if (columnName == "ProjectName")
                {
                    if (string.IsNullOrWhiteSpace(ProjectName))
                        return "Please fill in project name.";
                }
                if (columnName == "CategoryName")
                {
                    if (string.IsNullOrWhiteSpace(CategoryName))
                        return "Please fill in category name.";
                }

                return null;
            }
        }

        public string Error
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ProjectName))
                    return "Please fill in project name.";
                if (string.IsNullOrWhiteSpace(CategoryName))
                    return "Please fill in category name.";
                return null;
            }
        }

        #endregion
    }
}
