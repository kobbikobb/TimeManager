using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using PatternLib;
using TimeManagerLib.Model;

namespace TimeManagerLib.ViewModel
{
    public class TaskViewModel : ViewModelBase
    {
        private readonly ITimeManagerRepository _repository;

        public Task Task { get; private set; }

        public TaskViewModel(Task task)
        {
            Task = task;

            _repository = DependencyResolver.Resolve<ITimeManagerRepository>();
        }

        private bool _workedHoursSet;

        public string DayStartedString
        {
            get { return Task.Started.ToString("dd.MM.yy"); }
        }

        public DateTime Started
        {
            get { return Task.Started; }
            set
            {
                Task.Started = value;

                SetStartedOrCompleted();

                _repository.SaveTask(Task);
            }
        }

        public DateTime? Completed
        {
            get { return Task.Completed; }
            set
            {
                Task.Completed = value;

                RaisePropertyChanged("Completed");

                SetStartedOrCompleted();

                _repository.SaveTask(Task);
            }
        }

        private void SetStartedOrCompleted()
        {
            if (!Task.Completed.HasValue)
                return;

            var hours = Task.Completed.Value.Subtract(Started).TotalMinutes / 60;
            if (hours > 0)
            {
                Task.WorkedHours = (decimal)hours;
                RaisePropertyChanged("WorkedHours");
            }
            
        }

        public bool IsCompleted
        {
            get { return Task.Completed.HasValue; }
            set
            {
                if (value)
                    Completed = DateTime.Now;
                else
                    Completed = null;
            }
        }

        public decimal WorkedHours
        {
            get { return Task.WorkedHours; }
            set
            {
                _workedHoursSet = true;

                Task.WorkedHours = value;

                _repository.SaveTask(Task);
            }
        }

        public string ProjectName
        {
            get
            {
                if (Task.Category != null && Task.Category.Project != null)
                    return Task.Category.Project.Name;
                return null;
            }
        }

        public string CategoryName
        {
            get
            {
                if (Task.Category != null)
                    return Task.Category.Name;
                return null;
            }
        }

        public string ProjectCategoryName
        {
            get
            {
                if (Task.Category != null && Task.Category.Project != null)
                    return Task.Category.Project.Name + " - " + Task.Category.Name;
                return null;
            }
        }

        public string Description
        {
            get { return Task.Description; }
            set
            {
                Task.Description = value;

                _repository.SaveTask(Task);
            }
        }
    }
}
