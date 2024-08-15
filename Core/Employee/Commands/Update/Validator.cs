using FluentValidation;

namespace TaskManagement.Core.Employee.Commands.Update
{
    public class Validator : AbstractValidator<CommandModel>
    {
        public Validator()
        {
            RuleFor(query => query).NotEmpty().NotNull();
            RuleFor(x => x.Id).NotEmpty().NotNull().GreaterThan(0);
        }
    }
}

