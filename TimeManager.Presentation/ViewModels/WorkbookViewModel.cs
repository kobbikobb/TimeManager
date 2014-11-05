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

            _currentGrouping = WorkbookGroupings.NoGrouping;

            var tasksViewModel = timeManagerRepository.GetAllTasks()
                .Select(x => new TaskViewModel(timeManagerRepository, x)).ToList();
            _tasks = new ListCollectionView(tasksViewModel);
        }

        #region Properties

        private bool _showCompletedTasks;
        public bool ShowCompletedTasks
        {
            get { return _showCompletedTasks; }
            set
            {
                _showCompletedTasks = value;

                RaisePropertyChanged(() => Tasks);
            }
        }

        private bool _showUncompletedTasks;
        public bool ShowUncompletedTasks
        {
            get { return _showUncompletedTasks; }
            set
            {
                _showUncompletedTasks = value;

                RaisePropertyChanged(() => Tasks);
            }
        }

        private WorkbookGroupings _currentGrouping;

        public bool NoGrouping
        {
            get { return _currentGrouping == WorkbookGroupings.NoGrouping; }
            set
            {
                if (value)
                {
                    _currentGrouping = WorkbookGroupings.NoGrouping;   
                    RaisePropertyChanged(() => Tasks);
                }

            }
        }

        public bool GroupByStarted
        {
            get { return _currentGrouping == WorkbookGroupings.GroupByDayStarted; }
            set
            {

                if (value)
                {
                    _currentGrouping = WorkbookGroupings.GroupByDayStarted;
                    RaisePropertyChanged(() => Tasks);
                }
            }
        }

        public bool GroupByProject
        {
            get { return _currentGrouping == WorkbookGroupings.GroupByProject; ; }
            set 
            { 
                if (value)
                {
                    _currentGrouping = WorkbookGroupings.GroupByProject;
                    RaisePropertyChanged(() => Tasks);
                }
            }
        }

        public bool GroupByCategory
        {
            get { return _currentGrouping == WorkbookGroupings.GroupByCategory; ; }
            set
            {
                if (value)
                {
                    _currentGrouping = WorkbookGroupings.GroupByCategory;
                    RaisePropertyChanged(() => Tasks);
                }
            }
        }

        private ListCollectionView _tasks;
        public ListCollectionView Tasks
        {
            get
            {
                if (_tasks != null)
                {
                    _tasks.Filter =x => ((TaskViewModel)x).Completed.HasValue && ShowCompletedTasks || !((TaskViewModel)x).Completed.HasValue && ShowUncompletedTasks;
                    
                    _tasks.GroupDescriptions.Clear();
                    
                    if(GroupByStarted)
                        _tasks.GroupDescriptions.Add(new PropertyGroupDescription("DayStartedString"));
                    else if (GroupByProject)
                        _tasks.GroupDescriptions.Add(new PropertyGroupDescription("ProjectName"));
                    else if (GroupByCategory)
                        _tasks.GroupDescriptions.Add(new PropertyGroupDescription("ProjectCategoryName"));
                }

                return _tasks;
            }
            private set
            {
                _tasks = value;
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
                //TODO: Eyða category og project ef það eru ekki fleiri task á viðkomandi stað
                timeManagerRepository.DeleteTask(Task.Task);

                Tasks.Remove(Task);
            }
        }

        #endregion

        public override bool Equals(object obj)
        {
            return obj is WorkbookViewModel;
        }

        public override string ToString()
        {
            return "Workbook";
        }
    }
}
