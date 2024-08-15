using MediatR;
using TaskManagement.Shared.Web.Results;

namespace TaskManagement.Core.Reports.Queries.WeeklyReport
{
    public class QueryModel : IRequest<ValidationResult>
    {
        public int TaskId { get; set; }
    }
}
