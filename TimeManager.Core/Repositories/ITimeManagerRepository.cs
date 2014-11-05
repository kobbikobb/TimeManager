using System.Collections.Generic;

namespace TimeManager.Core.Repositories
{
    public interface ITimeManagerRepository
    {
        List<Project> GetProjects();

        List<Task> GetUncompletedTasks();
        List<Task> GetAllTasks();
        
        void SaveTask(Task task);
        void DeleteTask(Task task);
    }
}
