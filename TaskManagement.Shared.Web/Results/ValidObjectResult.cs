namespace TaskManagement.Shared.Web.Results
{
    public class ValidObjectResult : ValidationResult
    {
        public dynamic Data { get; }

        public ValidObjectResult(dynamic data)
        {
            Data = data;
        }
    }
}
