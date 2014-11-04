using System;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using GalaSoft.MvvmLight.Messaging;
using PatternLib;
using TimeManager.Properties;
using TimeManager.Windows;
using TimeManagerLib.Helpers;
using TimeManagerLib.Model;
using TimeManagerLib.ViewModel;
using TimeManagerLib.ViewModel.Extension;
using Application = System.Windows.Forms.Application;
using MessageBox = System.Windows.Forms.MessageBox;

namespace TimeManager
{

    public class Tray : ApplicationContext
    {
        private readonly NotifyIcon _notifyIcon;
        private readonly SessionChangeHandler _sessionHandler;
        private readonly Timer _idleTimer;
        private bool isStartedCanceled;

        public Tray()
        {
            _notifyIcon = new NotifyIcon {Icon = Resources.clock, ContextMenuStrip = new ContextMenuStrip(), Visible = true};

            _notifyIcon.ContextMenuStrip.Items.Add(new ToolStripMenuItem("Start task", null, StartTask));
            _notifyIcon.ContextMenuStrip.Items.Add(new ToolStripMenuItem("View workbook", null, ViewWorkbook));
            _notifyIcon.ContextMenuStrip.Items.Add(new ToolStripMenuItem("Quit", null, Quit));

            _sessionHandler = new SessionChangeHandler();
            _sessionHandler.MachineLocked += OnMachineLocked;
            _sessionHandler.MachineUnlocked += OnMachineUnlocked;

            _idleTimer = new Timer();
            _idleTimer.Tick += TimerElapsed;
            _idleTimer.Interval = 60000;
            _idleTimer.Start();
                  
            Messenger.Default.Register<DialogMessage>(this, ShowDialog);
        }

        private void ShowDialog(DialogMessage msg)
        {
            var icon = MessageBoxIcon.Information;
            if(msg.Icon == MessageBoxImage.Error)
                icon = MessageBoxIcon.Error;
            
            MessageBox.Show(msg.Content, msg.Caption, MessageBoxButtons.OK, icon);
        }

        private void TimerElapsed(object sender, EventArgs e)
        {
            if(!isStartedCanceled)
            {
                //If there is no active task we start one
                StartTaskIfNoUncompletedTasks();
            }

            //If the idle time is more then 10 minutes we close all active jobs
            var span = TimeSpan.FromMilliseconds(Win32.GetIdleTime());
            if (span.TotalMinutes > 30)
            {
                MarkUncompletedTasksCompleted(DateTime.Now.Subtract(span));
            }
        }

        private void OnMachineUnlocked(object sender, EventArgs e)
        {
            isStartedCanceled = false;
            StartTaskIfNoUncompletedTasks();
        }

        private void OnMachineLocked(object sender, EventArgs e)
        {
            MarkUncompletedTasksCompleted(DateTime.Now);
        }

        private void StartTask(object sender, EventArgs e)
        {
            StartTask();
        }

        private void ViewWorkbook(object sender, EventArgs e)
        {
            ViewWorkbook();
        }

        private void Quit(object sender, EventArgs e)
        {
            _notifyIcon.Visible = false;
            Application.Exit();
        }

        #region Open Views

        private void StartTaskIfNoUncompletedTasks()
        {
            try
            {
                //Ef það er verið að sýna form þá skoðum við ekki óunnin verk
                if (TrayWindow.Instance.IsShowing)
                    return;

                var repository = DependencyResolver.Resolve<ITimeManagerRepository>();
                var uncompletedTasks = repository.GetUncompletedTasks();

                if (uncompletedTasks.Count == 0)
                    StartTask();
            }
            catch (SqlException ex)
            {
                Logger.Instance.WriteException(ex);
                ex.Show();
            }
        }


        private StartTaskViewModel _lastStartTaskViewModel;

        private void StartTask()
        {
            try
            {
                var viewModel = new StartTaskViewModel();
                if (_lastStartTaskViewModel != null)
                {
                    if (_lastStartTaskViewModel.Project != null)
                        viewModel.Project = viewModel.Projects.SingleOrDefault(x => x.Id == _lastStartTaskViewModel.Project.Id);
                    if (_lastStartTaskViewModel.Category != null)
                        viewModel.Category = viewModel.Categories.SingleOrDefault(x => x.Id == _lastStartTaskViewModel.Category.Id);
                }

                if (TrayWindow.Instance.Show(viewModel))
                {
                    _lastStartTaskViewModel = viewModel;
                }
            }
            catch (SqlException ex)
            {
                Logger.Instance.WriteException(ex);
                ex.Show();
            }
        }

        private void ViewWorkbook()
        {
            try
            {
                var viewModel = new WorkbookViewModel();
                TrayWindow.Instance.Show(viewModel);
            }
            catch (SqlException ex)
            {
                Logger.Instance.WriteException(ex);
                ex.Show();
            }
        }

        private void MarkUncompletedTasksCompleted(DateTime completed)
        {
            try
            {
                //Mark all started tasks as completed
                var repository = DependencyResolver.Resolve<ITimeManagerRepository>();
                foreach (var task in repository.GetUncompletedTasks())
                {
                    new TaskViewModel(task) {Completed = completed};
                }

            }
            catch (SqlException ex)
            {
                Logger.Instance.WriteException(ex);
                ex.Show();
            }
        }

        #endregion
    }
}
