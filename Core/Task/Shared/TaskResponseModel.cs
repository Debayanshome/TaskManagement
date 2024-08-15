namespace TaskManagement.Core.Task.Shared
{
    public class TaskResponseModel
    {

        public int Id { get; set; }
        public string Details { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public EmployeeResponseModel Employee { get; set; }
        public List<DocumentResponseModel> Documents { get; set; }
    }
}
