namespace TaskManagement.Core.Task.Commands.Shared
{
    public abstract class TaskBaseCommandModel
    {
        public string Name { get; set; }
        public string Details { get; set; }
        public string Notes { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Timezone { get; set; }
        public EmployeeCommandModel Employee { get; set; }
        public List<DocumentCommandModel> Documents { get; set; }
    }

    public class EmployeeCommandModel
    {
        public string Name { get; set; }
        public int? Id { get; set; }
    }

    public class DocumentCommandModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Type { get; set; }
    }
}
