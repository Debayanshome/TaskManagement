using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Net;
using TaskManagement.Shared.Web.Results;

namespace TaskManagement.Shared.Web.Filters
{
    public class CustomActionFilter : BaseFilter, IActionFilter
    {
        private ILogger _logger;

        public CustomActionFilter(ILogger<CustomActionFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var objectContent = context.Result as ObjectResult;
            if (objectContent == null)
            {
                return;
            }

            if (objectContent.Value is NotFoundValidationResult)
            {
                SetResult(context, (objectContent.Value as Results.NotFoundValidationResult).Errors, HttpStatusCode.NotFound);
                return;
            }

            if (objectContent.Value is InvalidValidationResult)
            {
                SetResult(context, (objectContent.Value as Results.InvalidValidationResult).Errors, HttpStatusCode.BadRequest);
                return;
            }

            if (objectContent.Value is ValidObjectResult)
            {
                SetResult(context, (objectContent.Value as Results.ValidObjectResult).Data, HttpStatusCode.OK);
                return;
            }

            if (objectContent.Value is ValidResult)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                return;
            }

            if (objectContent.Value is Results.CreatedResult)
            {
                SetResult(context, (objectContent.Value as Results.CreatedResult).Data, HttpStatusCode.Created);
                return;
            }

            if (objectContent.Value is Results.UnauthorizedResult)
            {
                SetResult(context, (objectContent.Value as Results.UnauthorizedResult).Errors, HttpStatusCode.Unauthorized);
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }

        private void SetResult(ActionExecutedContext context, object data, HttpStatusCode statusCode)
        {
            var result = SerializeContent(data);
            result.StatusCode = (int)statusCode;

            context.HttpContext.Response.StatusCode = (int)statusCode;
            context.Result = result;
        }
    }
}
