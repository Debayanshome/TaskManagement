using TaskManagement.Shared.Repository;

namespace TaskManagement.Repository.Models
{
    public class TaskLog : EntityBase
    {
        public int TaskId { get; set; }
        public TaskDetail Task { get; set; }
        public DateTime DateLogged { get; set; }
        public TimeSpan TimeSpent { get; set; }
    }
}
