using System.Collections.Generic;

namespace TimeManager.Core
{
    public class Category
    {
        public decimal Id { get; set; }
        public decimal IdProject { get; set; }
        public string Name { get; set; }

        public Project Project { get; set; }
        public List<Task> Tasks { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public bool Equals(Category other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Id == Id && other.IdProject == IdProject;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Category)) return false;
            return Equals((Category) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Id.GetHashCode()*397) ^ IdProject.GetHashCode();
            }
        }
    }
}
