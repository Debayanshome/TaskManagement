using MediatR;
using TaskManagement.Core.Task.Commands.Shared;
using TaskManagement.Shared.Web.Results;

namespace TaskManagement.Core.Task.Commands.Create
{
    public class CommandModel : TaskBaseCommandModel, IRequest<ValidationResult>
    {

    }
}
