using MediatR;
using TaskManagement.Shared.Web.Results;

namespace TaskManagement.Core.Reports.Queries.EntireReport
{
    public class QueryModel : IRequest<ValidationResult>
    {
        public int TaskId { get; set; }
    }
}
