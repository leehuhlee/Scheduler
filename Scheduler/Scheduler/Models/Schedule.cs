using System;

namespace Scheduler.Models
{
    [Serializable]
    public class Schedule
    {
        public bool Check { get; set; }
        public string Note { get; set; }
        public string Deadline { get; set; }
    }
}
