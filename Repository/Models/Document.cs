using TaskManagement.Shared.Repository;

namespace TaskManagement.Repository.Models
{
    public class Document : EntityBase
    {
        public string Name { get; set; }
        public string Path { get; set; }
    }
}
