using FluentValidation;
using TaskManagement.Repository.Common.Enum;

namespace TaskManagement.Core.Task.Commands.Status
{
    public class Validator : AbstractValidator<CommandModel>
    {
        public Validator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.Status).Must(type => Enum.IsDefined(typeof(TaskStatusType), type)).WithMessage($"Invalid Task Status Type. Valid types are: {string.Join(", ", Enum.GetNames(typeof(TaskStatusType)))}.").MaximumLength(10);
        }
    }
}
