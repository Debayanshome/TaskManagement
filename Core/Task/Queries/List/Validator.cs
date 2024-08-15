using FluentValidation;
using TaskManagement.Core.Common.Validators;
using TaskManagement.Repository.Common.Enum;

namespace TaskManagement.Core.Task.Queries.List
{
    public class Validator : PageQueryValidator<QueryModel>
    {
        public Validator() : base()
        {
            RuleFor(x => x.StatusFilter).MaximumLength(100);
            RuleFor(x => x.SortCol)
                .Must(x => new[] { "Name", "EmployeeName", "DocumentName", "Status" }.Contains(x))
                .WithMessage("Sorting is applicable for 'Name', 'EmployeeName', 'DocumentName', and 'Status'")
                .MaximumLength(50)
                .When(x => !string.IsNullOrWhiteSpace(x.SortCol));
            RuleFor(x => x.StatusFilter).Must(type => Enum.IsDefined(typeof(TaskStatusType), type)).WithMessage($"Invalid Task Status Type. Valid types are: {string.Join(", ", Enum.GetNames(typeof(TaskStatusType)))}.").MaximumLength(100)
                .When(x => !string.IsNullOrWhiteSpace(x.StatusFilter));
        }
    }
}
