using MediatR;
using TaskManagement.Core.Employee.Commands.Shared;
using TaskManagement.Shared.Web.Results;

namespace TaskManagement.Core.Employee.Commands.Create
{
    public class CommandModel : BaseCommandModel, IRequest<ValidationResult>
    {

    }
}
