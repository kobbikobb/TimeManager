using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeManagerLib.Model
{
    public interface ITimeManagerRepository
    {
        List<Project> GetProjects();
        List<Category> GetProjectCategories(Project project);
        List<Task> GetCategoryTasks(Category category);

        List<Task> GetUncompletedTasks();
        List<Task> GetAllTasks();

        void SaveProject(Project project);
        void DeleteProject(Project project);

        void SaveCategory(Category category);
        void DeleteCategory(Category category);

        void SaveTask(Task task);
        void DeleteTask(Task task);

        void TestConnection();
    }
}
