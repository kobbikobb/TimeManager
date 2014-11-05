using System.Collections.Generic;

namespace TimeManager.Core
{
    public class Category
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }

        public virtual Project Project { get; set; }
        public virtual IList<Task> Tasks { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
