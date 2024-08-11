namespace TaskManagement.Shared.Web.Results
{
    public class NotFoundValidationResult : ValidationResult
    {
        public NotFoundValidationResult(string field, string description)
        {
            this.AddError(field, description);
        }
    }
}
