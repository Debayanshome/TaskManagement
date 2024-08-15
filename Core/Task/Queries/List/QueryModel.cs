using MediatR;
using TaskManagement.Shared.Repository;
using TaskManagement.Shared.Web.Results;

namespace TaskManagement.Core.Task.Queries.List
{
    public class QueryModel : PageQueryModel, IRequest<ValidationResult>
    {
        public string StatusFilter { get; set; }
    }
}
