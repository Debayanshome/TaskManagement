using FluentValidation;
using TaskManagement.Core.Task.Commands.Shared;

namespace TaskManagement.Core.Common.Validators
{
    public class DocumentValidator : AbstractValidator<DocumentCommandModel>
    {
        public DocumentValidator()
        {
            RuleFor(y => y.Name).NotEmpty().NotNull().MinimumLength(1).MaximumLength(100);
            RuleFor(y => y.Path).NotEmpty().NotNull().MinimumLength(1).MaximumLength(200);
            RuleFor(y => y.Type).MinimumLength(1).MaximumLength(50);
        }
    }
}
