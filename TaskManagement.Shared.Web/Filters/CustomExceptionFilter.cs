using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Net;

namespace TaskManagement.Shared.Web.Filters
{
    public class CustomExceptionFilter : BaseFilter, IExceptionFilter
    {
        private ILogger _logger;

        public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception != null)
            {
                HandleException(context);
            }
        }

        private void HandleException(ExceptionContext context)
        {
            var randomEventId = new Random().Next();
            var eventId = new EventId(randomEventId);
            var message = $"An eror has occurred while executing the request. Event Id: {eventId}";
            _logger.LogError(context.Exception, message);

#if DEBUG
            message = context.Exception.ToString();

#endif

            context.ExceptionHandled = true;
            var result = SerializeContent(new { error = message });
            result.StatusCode = (int)HttpStatusCode.InternalServerError;

            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Result = result;
        }
    }

}
