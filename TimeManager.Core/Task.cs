using System;

namespace TimeManager.Core
{
    public class Task
    {
        public virtual int Id { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime Started { get; set; }
        public virtual DateTime? Completed { get; set; }
        public virtual decimal WorkedHours { get; set; }

        public virtual Category Category { get; set; }
    }
}
