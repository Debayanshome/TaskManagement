using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManagement.Shared.Repository;

namespace TaskManagement.Repository.Models
{
    public class TaskDetail : EntityBase
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string Timezone { get; set; }
        public List<Document> Documents { get; set; }

        [NotMapped]
        public double? TotalHoursSpent
        {
            get
            {
                return (CompletedDate.HasValue ? CompletedDate.Value - StartDate : DateTime.UtcNow - StartDate).TotalHours;
            }
        }
    }
}
