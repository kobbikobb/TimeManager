using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Linq;
using TimeManager.Core;
using TimeManager.Core.Repositories;
using Task = TimeManager.Core.Task;

namespace TimeManager.NHibernate
{
    public class TimeManagerRepository : ITimeManagerRepository
    {
        private readonly ISession session;

        public TimeManagerRepository(ISession session)
        {
            if (session == null) throw new ArgumentNullException("session");
            this.session = session;
        }

        public List<Project> GetProjects()
        {
            return session.Query<Project>().ToList();
        }

        public List<Task> GetUncompletedTasks()
        {
            return session.Query<Task>().Where(x => !x.Completed.HasValue).ToList();
        }

        public List<Task> GetAllTasks()
        {
            return session.Query<Task>().ToList();
        }

        public void SaveTask(Task task)
        {
            session.SaveOrUpdate(task);
            session.Flush();
        }

        public void DeleteTask(Task task)
        {
            session.Delete(task);
            session.Flush();
        }
    }
}
