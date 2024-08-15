using Ardalis.Specification;

namespace TaskManagement.Repository.Specifications.Employee
{
    public sealed class EmployeeFetchSpecification : Specification<Models.Employee>, ISingleResultSpecification<Models.Employee>
    {
        public ISpecification<Models.Employee> NoTracking()
        {
            return Query.AsNoTracking().Specification;
        }
        public EmployeeFetchSpecification(int employeeId, bool isEntireTaskDetails = false)
        {
            if(isEntireTaskDetails)
            {
                Query.Include(r => r.Tasks).ThenInclude(t => t.Documents);
            }
            Query
                .Where(b => b.Id == employeeId);
        }
    }
}
