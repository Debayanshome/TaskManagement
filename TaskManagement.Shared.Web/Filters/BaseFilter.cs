using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace TaskManagement.Shared.Web.Filters
{
    public abstract class BaseFilter
    {
        protected ContentResult SerializeContent(object data)
        {
            var content = JsonSerializer.Serialize(data);
            var result = new ContentResult();

            result.Content = content;
            result.ContentType = "application/json";

            return result;
        }
    }
}
