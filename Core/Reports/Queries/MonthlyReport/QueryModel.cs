using MediatR;
using TaskManagement.Shared.Web.Results;

namespace TaskManagement.Core.Reports.Queries.MonthlyReport
{
    public class QueryModel : IRequest<ValidationResult>
    {
        public int TaskId { get; set; }
    }
}
