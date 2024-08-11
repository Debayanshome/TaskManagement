using FluentValidation;

namespace TaskManagement.Core.Task.Queries.GetById
{
    public class Validator : AbstractValidator<QueryModel>
    {
        public Validator()
        {
            RuleFor(query => query).NotEmpty().NotNull();
            RuleFor(x => x.Id).NotEmpty().NotNull().GreaterThan(0);
        }
    }
}
