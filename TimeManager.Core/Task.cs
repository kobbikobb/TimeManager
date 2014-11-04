using System;

namespace TimeManager.Core
{
    public class Task
    {
        public decimal Id { get; set; }

        public decimal IdCategory { get; set; }
        public Category Category { get; set; }
 
        public string Description { get; set; }
        public DateTime Started { get; set; }
        public DateTime? Completed { get; set; }
        public decimal WorkedHours { get; set; }
    }
}
