using FluentValidation;

namespace TaskManagement.Core.Employee.Commands.Shared
{
    public abstract class BaseValidator<T> : AbstractValidator<T> where T : BaseCommandModel
    {
        public BaseValidator()
        {
            RuleFor(x => x).NotEmpty().NotNull();
            RuleFor(x => x.Name).NotEmpty().NotNull().MinimumLength(1).MaximumLength(100);
        }
    }
}

