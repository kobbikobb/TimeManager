using System.Collections.Generic;

namespace TimeManager.Core
{
    public class Project
    {
        public decimal Id { get; set; }
        public string Name { get; set; }

        public List<Category> Categories { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public bool Equals(Project other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Id == Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Project)) return false;
            return Equals((Project) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
