using FluentValidation;

namespace TaskManagement.Core.Reports.Queries.ByEmployeeId
{
    public class Validator : AbstractValidator<QueryModel>
    {
        public Validator()
        {
            RuleFor(query => query).NotEmpty().NotNull();
            RuleFor(x => x.EmployeeId).NotEmpty().NotNull().GreaterThan(0);
        }
    }
}
