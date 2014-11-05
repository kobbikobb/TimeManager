using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using TimeManager.Core;
using TimeManager.Core.Repositories;

namespace TimeManager.Presentation.ViewModels
{
    public class StartTaskViewModel : ViewModelBase
    {
        private readonly ITimeManagerRepository timeManagerRepository;

        public StartTaskViewModel(ITimeManagerRepository timeManagerRepository)
        {
            if (timeManagerRepository == null) throw new ArgumentNullException("timeManagerRepository");
            this.timeManagerRepository = timeManagerRepository;

            Started = DateTime.Now.TimeOfDay;
        }

        #region Properties

        public string Description { get; set; }

        private TimeSpan started;
        public TimeSpan Started
        {
            get { return started; }
            set
            {
                started = value;

                RaisePropertyChanged(() => Started);
            }
        }

        private TimeSpan? completed;
        public TimeSpan? Completed
        {
            get { return completed; }
            set
            {
                completed = value;

                if (completed.HasValue)
                {
                    var hours = completed.Value.Subtract(Started).TotalMinutes / 60;

                    WorkedHours = (decimal)hours;
                }
            }
        }

        private decimal workedHours;
        public decimal WorkedHours
        {
            get { return workedHours; }
            set
            {
                workedHours = value;
                
                RaisePropertyChanged(() => WorkedHours);
            }
        }

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
            set
            {
                projects = value;
            }
        }

        private Project project;
        public Project Project
        {
            get { return project; }
            set
            {
                project = value;

                if (project != null)
                {   
                    Categories = project.Categories.ToList();
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

        private List<Category> categories;
        public List<Category> Categories
        {
            get { return categories; }
            set 
            { 
                categories = value;
                RaisePropertyChanged(() => Categories);
            }
        }

        public Category Category { get; set; }

        public string CategoryName { get; set; }

        #endregion

        private ICommand saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                if (saveCommand == null)
                    saveCommand = new DelegateCommand(Save);
                return saveCommand;
            }
        }

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
        }

        public void SelectProject(decimal id)
        {
            Project = Projects.Single(x => x.Id == id);
        }

        public void SelectCategory(decimal id)
        {
            Category = Categories.Single(x => x.Id == id);
        }
    }
}
