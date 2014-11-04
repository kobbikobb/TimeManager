using System;
using System.Collections.Generic;
using System.Data;
using System.Data.EntityClient;
using System.Data.Metadata.Edm;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using TimeManagerLib.Model;

namespace TimeManagerLib.Data
{
    public class TimeManagerRepository : ITimeManagerRepository
    {
        #region Members

        private readonly EntityConnection _connection;

        #endregion

        #region Contructors

        public TimeManagerRepository()
        {

        }
        public TimeManagerRepository(SqlConnection sqlConnection)
        {
            _connection = SqlConnectionToEntityConnection(sqlConnection);
        }

        #endregion

        #region Connection

        private TimeManagerContext GetDataContext()
        {
            if(_connection == null)
                return new TimeManagerContext();
            return new TimeManagerContext(_connection);
        }

        public static EntityConnection SqlConnectionToEntityConnection(SqlConnection sqlConnection)
        {
            var workspace = new MetadataWorkspace(new string[] { "res://*/" }, new Assembly[] { Assembly.GetExecutingAssembly() });
            return new EntityConnection(workspace, sqlConnection);
        }

        #endregion

        #region Data getters

        public List<Project> GetProjects()
        {
            using(var gögn = GetDataContext())
            {
                return gögn.Projects.ToList();
            }
        }

        public List<Category> GetProjectCategories(Project project)
        {
            using (var gögn = GetDataContext())
            {
                return gögn.Categories.Where(x => x.IdProject == project.Id).ToList();
            }
        }

        public List<Task> GetCategoryTasks(Category category)
        {
            using (var gögn = GetDataContext())
            {
                return gögn.Tasks.Where(x => x.IdCategory == category.Id).ToList();
            }
        }

        public List<Task> GetUncompletedTasks()
        {
            using (var gögn = GetDataContext())
            {
                return gögn.Tasks.Include("Category.Project").Where(x => !x.Completed.HasValue).ToList();
            }
        }

        public List<Task> GetAllTasks()
        {
            using (var gögn = GetDataContext())
            {
                return gögn.Tasks.Include("Category.Project").ToList();
            }
        }

        #endregion

        #region Save data
        
        public void SaveProject(Project project)
        {
            if (project == null)
                throw new Exception("New project not specified");

            using (var gögn = GetDataContext())
            {
                gögn.Projects.Attach(project);
                gögn.ObjectStateManager.ChangeObjectState(project, EntityState.Added);

                gögn.SaveChanges();
            }
        }

        public void DeleteProject(Project project)
        {
            if (project == null)
                throw new Exception("Project not specified");

            using (var gögn = GetDataContext())
            {
                gögn.Projects.Attach(project);
                gögn.ObjectStateManager.ChangeObjectState(project, EntityState.Deleted);

                gögn.SaveChanges();
            }
        }

        public void SaveCategory(Category category)
        {
            if (category == null)
                throw new Exception("New category not specified");

            if (category.IdProject == 0)
            {
                SaveProject(category.Project);
                category.IdProject = category.Project.Id;
            }

            using (var gögn = GetDataContext())
            {
                gögn.Categories.Attach(category);
                gögn.ObjectStateManager.ChangeObjectState(category, EntityState.Added);

                gögn.SaveChanges();
            }
        }

        public void DeleteCategory(Category category)
        {
            if (category == null)
                throw new Exception("Category not specified");

            using (var gögn = GetDataContext())
            {
                gögn.Categories.Attach(category);
                gögn.ObjectStateManager.ChangeObjectState(category, EntityState.Deleted);

                gögn.SaveChanges();
            }
        }

        public void SaveTask(Task task)
        {
            if (task.IdCategory == 0)
            {
                SaveCategory(task.Category);
                task.IdCategory = task.Category.Id;
            }

            using (var gögn = GetDataContext())
            {
                if (task.Id == 0)
                {
                    gögn.Tasks.Attach(task);
                    gögn.ObjectStateManager.ChangeObjectState(task, EntityState.Added);
                }
                else
                {
                    gögn.Tasks.Attach(task);
                    gögn.ObjectStateManager.ChangeObjectState(task, EntityState.Modified);
                }

                gögn.SaveChanges();
            }
        }

        public void DeleteTask(Task task)
        {
            if (task == null)
                throw new Exception("Category not specified");

            using (var gögn = GetDataContext())
            {
                gögn.Tasks.Attach(task);
                gögn.ObjectStateManager.ChangeObjectState(task, EntityState.Deleted);

                gögn.SaveChanges();
            }
        }

        #endregion
    }
}
