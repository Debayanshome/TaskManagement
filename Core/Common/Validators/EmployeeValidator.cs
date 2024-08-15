using FluentValidation;
using TaskManagement.Core.Task.Commands.Shared;

namespace TaskManagement.Core.Common.Validators
{
    public class EmployeeValidator : AbstractValidator<EmployeeCommandModel>
    {
        public EmployeeValidator()
        {
            RuleFor(y => y.Name).NotEmpty().NotNull().MinimumLength(1).MaximumLength(100);
            RuleFor(z => z.Id).GreaterThan(0);
        }
    }
}
