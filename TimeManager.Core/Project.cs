using System.Collections.Generic;

namespace TimeManager.Core
{
    public class Project
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }

        public virtual IList<Category> Categories { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
