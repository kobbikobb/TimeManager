using System;
using TimeManager.Windows;

namespace TimeManager
{
    public class MachineStatusTaskAutomation
    {
        private readonly TaskAutomation taskAutomation;
        
        public MachineStatusTaskAutomation(TaskAutomation taskAutomation)
        {
            if (taskAutomation == null) throw new ArgumentNullException("taskAutomation");
            this.taskAutomation = taskAutomation;
        }

        public void StartAutomation()
        {
            var sessionHandler = new SessionChangeHandler();
            sessionHandler.MachineLocked += OnMachineLocked;
            sessionHandler.MachineUnlocked += OnMachineUnlocked;
        }

        private void OnMachineUnlocked(object sender, EventArgs e)
        {
            taskAutomation.StartTaskIfNoUncompletedTasks();
        }

        private void OnMachineLocked(object sender, EventArgs e)
        {
            taskAutomation.MarkUncompletedTasksCompleted(DateTime.Now);
        }
    }
}
