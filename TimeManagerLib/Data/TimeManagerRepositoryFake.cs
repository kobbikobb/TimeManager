using System;
using System.Collections.Generic;
using TimeManager.Core;
using TimeManager.Core.Repositories;

namespace TimeManagerLib.Data
{
    public class TimeManagerRepositoryFake : ITimeManagerRepository
    {
        public List<Project> GetProjects()
        {
            return new List<Project>();
        }

        public List<Category> GetProjectCategories(Project project)
        {
            return new List<Category>();
        }

        public List<Task> GetCategoryTasks(Category category)
        {
            return new List<Task>();
        }

        public List<Task> GetUncompletedTasks()
        {
            return new List<Task>();
        }

        public List<Task> GetAllTasks()
        {
           return new List<Task>();
        }

        public void SaveProject(Project project)
        {
            throw new NotImplementedException();
        }

        public void DeleteProject(Project project)
        {
            throw new NotImplementedException();
        }

        public void SaveCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public void DeleteCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public void SaveTask(Task task)
        {
            throw new NotImplementedException();
        }

        public void DeleteTask(Task task)
        {
            throw new NotImplementedException();
        }
    }
}
