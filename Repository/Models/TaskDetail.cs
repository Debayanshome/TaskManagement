using System.ComponentModel.DataAnnotations.Schema;
using TaskManagement.Shared.Repository;

namespace TaskManagement.Repository.Models
{
    public class TaskDetail : EntityBase
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public string Details { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public List<Document> Documents { get; set; }
    }
}
