namespace TaskManagement.Shared.Web.Results
{
    public class CreatedResult : ValidationResult
    {
        public dynamic Data { get; private set; }

        public CreatedResult(dynamic data)
        {
            Data = data;
        }
    }
}
