using FluentValidation;

namespace TaskManagement.Core.Reports.Queries.WeeklyReport
{
    public class Validator : AbstractValidator<QueryModel>
    {
        public Validator()
        {
            RuleFor(query => query).NotEmpty().NotNull();
            RuleFor(x => x.TaskId).NotEmpty().NotNull().GreaterThan(0);
        }

    }
}
