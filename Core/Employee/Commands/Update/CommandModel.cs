using MediatR;
using TaskManagement.Core.Employee.Commands.Shared;
using TaskManagement.Shared.Web.Results;

namespace TaskManagement.Core.Employee.Commands.Update
{
    public class CommandModel : BaseCommandModel, IRequest<ValidationResult>
    {
        public int Id { get; set; }
    }
}
