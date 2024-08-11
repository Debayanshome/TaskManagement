namespace TaskManagement.Shared.Web.Results
{
    public class UnauthorizedResult : ValidationResult
    {
        public UnauthorizedResult(string field, string description)
        {
            this.AddError(field, description);
        }
    }
}
