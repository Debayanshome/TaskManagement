using MediatR;
using TaskManagement.Shared.Web.Results;

namespace TaskManagement.Core.Task.Commands.Status
{
    public class CommandModel : IRequest<ValidationResult>
    {
        public int Id { get; set; }
        public string Status { get; set; }
    }
}
