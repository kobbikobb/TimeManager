using System;
using TimeManagerLib.Model;

namespace TimeManager
{
    public class TaskAutomation
    {
        private readonly ITimeManagerRepository timeManagerRepository;
        private readonly StartTaskAction startTaskAction;

        public TaskAutomation(ITimeManagerRepository timeManagerRepository, StartTaskAction startTaskAction)
        {
            if (timeManagerRepository == null) throw new ArgumentNullException("timeManagerRepository");
            if (startTaskAction == null) throw new ArgumentNullException("startTaskAction");
            this.timeManagerRepository = timeManagerRepository;
            this.startTaskAction = startTaskAction;
        }

        public void StartTaskIfNoUncompletedTasks()
        {
            var uncompletedTasks = timeManagerRepository.GetUncompletedTasks();

            if (uncompletedTasks.Count == 0)
                startTaskAction.Execute();
        }

        public void MarkUncompletedTasksCompleted(DateTime completed)
        {
            foreach (var task in timeManagerRepository.GetUncompletedTasks())
            {
                task.Completed = completed;
                timeManagerRepository.SaveTask(task);
            }
        }
    }
}
