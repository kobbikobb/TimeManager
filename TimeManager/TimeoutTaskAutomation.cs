using System;
using System.Windows.Forms;
using TimeManager.Windows;

namespace TimeManager
{
    public class TimeoutTaskAutomation
    {
        private readonly TaskAutomation taskAutomation;

        public TimeoutTaskAutomation(TaskAutomation taskAutomation)
        {
            if (taskAutomation == null) throw new ArgumentNullException("taskAutomation");
            this.taskAutomation = taskAutomation;
        }

        public void StartAutomation()
        {
            var timer = new Timer();
            timer.Tick += TimerElapsed;
            timer.Interval = 60000;
            timer.Start();
        }

        private void TimerElapsed(object sender, EventArgs e)
        {
            taskAutomation.StartTaskIfNoUncompletedTasks();

            //If the idle time is more then 10 minutes we close all active jobs
            var span = TimeSpan.FromMilliseconds(Win32.GetIdleTime());
            if (span.TotalMinutes > 30)
            {
                taskAutomation.MarkUncompletedTasksCompleted(DateTime.Now.Subtract(span));
            }
        }
    }
}
