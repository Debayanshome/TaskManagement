using MediatR;
using TaskManagement.Shared.Web.Results;

namespace TaskManagement.Core.Task.Queries.GetById
{
    public class QueryModel : IRequest<ValidationResult>
    {
        public int Id { get; set; }
    }
}