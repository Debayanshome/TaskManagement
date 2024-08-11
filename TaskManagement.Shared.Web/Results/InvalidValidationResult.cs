namespace TaskManagement.Shared.Web.Results
{
    public class InvalidValidationResult : ValidationResult
    {
        public InvalidValidationResult(string field, string description)
        {
            AddError(field, description);
        }
    }
}
