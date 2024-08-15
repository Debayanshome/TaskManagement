using FluentValidation;
using TaskManagement.Core.Common.Validators;
using TaskManagement.Repository.Specifications.Employee;
using TaskManagement.Shared.Repository.Interface;

namespace TaskManagement.Core.Task.Commands.Shared
{
    public abstract class TaskBaseValidator<T> : AbstractValidator<T> where T : TaskBaseCommandModel
    {
        private readonly IReadRepository<Repository.Models.Employee> _employeeRepository;
        public TaskBaseValidator(IReadRepository<Repository.Models.Employee> employeeRepository)
        {
            this._employeeRepository = employeeRepository;
            RuleFor(x => x).NotEmpty().NotNull();
            RuleFor(x => x.Name).NotEmpty().NotNull().MinimumLength(1).MaximumLength(50);
            RuleFor(x => x.Notes).MinimumLength(1).MaximumLength(5000);
            RuleFor(x => x.Details).NotEmpty().NotNull().MinimumLength(1).MaximumLength(5000);
            RuleFor(x => x.StartDate).NotEmpty().NotNull();
            RuleFor(x => x.DueDate).NotEmpty().NotNull().GreaterThan(z => z.StartDate);
            RuleFor(x => x.Timezone).NotEmpty().NotNull().MaximumLength(20);
            RuleForEach(x => x.Documents).SetValidator(new DocumentValidator()).When(x => x != null && x.Documents != null && x.Documents.Count > 0);
            RuleFor(x => x.Employee).NotEmpty().NotNull().SetValidator(new EmployeeValidator());
            RuleFor(x => x).Custom(EmployeeIdValidator).When(r => r.Employee != null && r.Employee.Id.HasValue);
        }

        private void EmployeeIdValidator(T data, ValidationContext<T> context)
        {
          if (data.Employee.Id.HasValue && !_employeeRepository.Any(new EmployeeFetchSpecification(data.Employee.Id.Value)))
            {
                context.AddFailure("EmployeeId", $"Invalid Employee Id, the Employee specified {data.Employee.Id.Value} doesn't exist in DB!");
            }
        }
    }
}
