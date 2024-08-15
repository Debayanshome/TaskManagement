using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;

namespace TaskManagement.Repository.Specifications.Task
{
    public sealed class TaskFetchSpecification : Specification<Models.TaskDetail>, ISingleResultSpecification<Models.TaskDetail>
    {
        public ISpecification<Models.TaskDetail> NoTracking()
        {
            return Query.AsNoTracking().Specification;
        }
        public TaskFetchSpecification(int taskId)
        {            
            Query
                .Include(x => x.Employee)
                .Include(x => x.Documents)
                .Where(b => b.Id == taskId);
        }

        public TaskFetchSpecification(List<int> documentIds, int taskId)
        {
            Query
            .Include(x => x.Documents)
           .Where(b => b.Documents.Any(r => documentIds.Contains(r.Id)) && b.Id == taskId);
        }

        public TaskFetchSpecification(int taskId, DateTime start, DateTime end)
        {
            Query
                .Include(x => x.Employee)
                .Include(x => x.Documents)
                .Where(b => b.Id == taskId && b.DueDate.Date >= start && b.DueDate.Date < end);
        }
    }
}
