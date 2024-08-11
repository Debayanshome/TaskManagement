using TaskManagement.Shared.Web.Models;

namespace TaskManagement.Shared.Web.Results
{
    public abstract class ValidationResult
    {
        public IList<ErrorMessage> Errors { get; private set; }

        public ValidationResult()
        {
            Errors = new List<ErrorMessage>();
        }

        public void AddError(string field, string description)
        {
            Errors.Add(new ErrorMessage()
            {
                Field = field,
                Description = new List<string>() { description }
            });
        }
    }
}
