using System;
using GalaSoft.MvvmLight;
using TimeManager.Core;
using TimeManager.Core.Repositories;

namespace TimeManagerLib.ViewModel
{
    public class TaskViewModel : ViewModelBase
    {
        private readonly ITimeManagerRepository timeManagerRepository;

        public Task Task { get; private set; }

        public TaskViewModel(ITimeManagerRepository timeManagerRepository, Task task)
        {
            if (timeManagerRepository == null) throw new ArgumentNullException("timeManagerRepository");
            this.timeManagerRepository = timeManagerRepository;

            Task = task;
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

                timeManagerRepository.SaveTask(Task);
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

                timeManagerRepository.SaveTask(Task);
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

                timeManagerRepository.SaveTask(Task);
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

                timeManagerRepository.SaveTask(Task);
            }
        }
    }
}
