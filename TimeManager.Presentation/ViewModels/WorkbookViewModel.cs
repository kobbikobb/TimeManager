using System;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using TimeManager.Core.Repositories;

namespace TimeManager.Presentation.ViewModels
{
    public class WorkbookViewModel : ViewModelBase
    {
        private readonly ITimeManagerRepository timeManagerRepository;

        private enum WorkbookGroupings
        {
            NoGrouping,
            GroupByDayStarted,
            GroupByProject,
            GroupByCategory
        }

        public WorkbookViewModel(ITimeManagerRepository timeManagerRepository)
        {
            if (timeManagerRepository == null) throw new ArgumentNullException("timeManagerRepository");
            this.timeManagerRepository = timeManagerRepository;

            ShowUncompletedTasks = true;
            ShowCompletedTasks = true;

            currentGrouping = WorkbookGroupings.NoGrouping;

            var tasksViewModel = timeManagerRepository.GetAllTasks()
                .Select(x => new TaskViewModel(timeManagerRepository, x)).ToList();
            tasks = new ListCollectionView(tasksViewModel);
        }

        #region Properties

        private bool showCompletedTasks;
        public bool ShowCompletedTasks
        {
            get { return showCompletedTasks; }
            set
            {
                showCompletedTasks = value;

                RaisePropertyChanged(() => Tasks);
            }
        }

        private bool showUncompletedTasks;
        public bool ShowUncompletedTasks
        {
            get { return showUncompletedTasks; }
            set
            {
                showUncompletedTasks = value;

                RaisePropertyChanged(() => Tasks);
            }
        }

        private WorkbookGroupings currentGrouping;

        public bool NoGrouping
        {
            get { return currentGrouping == WorkbookGroupings.NoGrouping; }
            set
            {
                if (value)
                {
                    currentGrouping = WorkbookGroupings.NoGrouping;   
                    RaisePropertyChanged(() => Tasks);
                }

            }
        }

        public bool GroupByStarted
        {
            get { return currentGrouping == WorkbookGroupings.GroupByDayStarted; }
            set
            {

                if (value)
                {
                    currentGrouping = WorkbookGroupings.GroupByDayStarted;
                    RaisePropertyChanged(() => Tasks);
                }
            }
        }

        public bool GroupByProject
        {
            get { return currentGrouping == WorkbookGroupings.GroupByProject; ; }
            set 
            { 
                if (value)
                {
                    currentGrouping = WorkbookGroupings.GroupByProject;
                    RaisePropertyChanged(() => Tasks);
                }
            }
        }

        public bool GroupByCategory
        {
            get { return currentGrouping == WorkbookGroupings.GroupByCategory; ; }
            set
            {
                if (value)
                {
                    currentGrouping = WorkbookGroupings.GroupByCategory;
                    RaisePropertyChanged(() => Tasks);
                }
            }
        }

        private ListCollectionView tasks;
        public ListCollectionView Tasks
        {
            get
            {
                if (tasks != null)
                {
                    tasks.Filter =x => ((TaskViewModel)x).Completed.HasValue && ShowCompletedTasks || !((TaskViewModel)x).Completed.HasValue && ShowUncompletedTasks;
                    
                    tasks.GroupDescriptions.Clear();
                    
                    if(GroupByStarted)
                        tasks.GroupDescriptions.Add(new PropertyGroupDescription("DayStartedString"));
                    else if (GroupByProject)
                        tasks.GroupDescriptions.Add(new PropertyGroupDescription("ProjectName"));
                    else if (GroupByCategory)
                        tasks.GroupDescriptions.Add(new PropertyGroupDescription("ProjectCategoryName"));
                }

                return tasks;
            }
            private set
            {
                tasks = value;
                RaisePropertyChanged(() => Tasks);
            }

        }
        
        public TaskViewModel Task { get; set; }

        #endregion

        #region Commands

        public ICommand DeleteTaskCommand
        {
            get
            {
                return new DelegateCommand(DeleteTask);
            }
        }

        private void DeleteTask()
        {
            if (Task != null)
            {
                timeManagerRepository.DeleteTask(Task.Task);
                Tasks.Remove(Task);
            }
        }

        #endregion
    }
}
