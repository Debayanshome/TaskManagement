using MediatR;
using TaskManagement.Core.Task.Commands.Shared;
using TaskManagement.Shared.Web.Results;

namespace TaskManagement.Core.Task.Commands.Update
{
    public class CommandModel : TaskBaseCommandModel, IRequest<ValidationResult>
    {
        public int Id { get; set; }

        public string Status { get; set; }
    }
}
