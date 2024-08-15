using MediatR;
using TaskManagement.Shared.Web.Results;

namespace TaskManagement.Core.Reports.Queries.ByEmployeeId
{
    public class QueryModel : IRequest<ValidationResult>
    {
        public int EmployeeId { get; set; }
    }
}
