using System.Data.EntityClient;
using System.Data.Objects;
using TimeManagerLib.Model;

namespace TimeManagerLib.Data
{
    public class TimeManagerContext : ObjectContext
    {
        #region Constructors

        /// <summary>
        /// Initializes a new TimeManagerContext object using the connection string found in the 'TimeManagerContext' section of the application configuration file.
        /// </summary>
        public TimeManagerContext() : base("name=TimeManagerContext", "TimeManagerContext")
        {

        }

        /// <summary>
        /// Initialize a new TimeManagerContext object.
        /// </summary>
        public TimeManagerContext(string connectionString) : base(connectionString, "TimeManagerContext")
        {

        }

        /// <summary>
        /// Initialize a new TimeManagerContext object.
        /// </summary>
        public TimeManagerContext(EntityConnection connection) : base(connection, "TimeManagerContext")
        {

        }

        #endregion

        #region ObjectSet Properties

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Category> Categories
        {
            get
            {
                if ((_categories == null))
                {
                    _categories = CreateObjectSet<Category>("Categories");
                }
                return _categories;
            }
        }
        private ObjectSet<Category> _categories;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Project> Projects
        {
            get
            {
                if ((_projects == null))
                {
                    _projects = CreateObjectSet<Project>("Projects");
                }
                return _projects;
            }
        }
        private ObjectSet<Project> _projects;

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Task> Tasks
        {
            get
            {
                if ((_tasks == null))
                {
                    _tasks = CreateObjectSet<Task>("Tasks");
                }
                return _tasks;
            }
        }
        private ObjectSet<Task> _tasks;

        #endregion
    }
}
