using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;
using TaskManagement.Shared.Web.Models;

namespace TaskManagement.Shared.Web.Filters
{
    public class CustomResultFilter : BaseFilter, IResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                CreateBadRequestContext(context);
            }
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {

        }

        private void CreateBadRequestContext(ResultExecutingContext context)
        {
            var errorMessages = TransformErrorMessages(context.ModelState);
            SetResult(context, errorMessages, HttpStatusCode.BadRequest);
        }

        public List<ErrorMessage> TransformErrorMessages(ModelStateDictionary modelState)
        {
            var errorMessages = new List<ErrorMessage>();
            foreach (var keyModelStatePair in modelState)
            {
                var key = keyModelStatePair.Key;
                var errors = keyModelStatePair.Value.Errors;
                if (errors != null && errors.Count > 0)
                {
                    errorMessages.Add(new ErrorMessage() { Field = key, Description = errors.Select(c => c.ErrorMessage).ToList() });
                }
            }

            return errorMessages;
        }

        private void SetResult(ResultExecutingContext context, object data, HttpStatusCode statusCode)
        {
            var result = SerializeContent(data);
            result.StatusCode = (int)statusCode;

            context.HttpContext.Response.StatusCode = (int)statusCode;
            context.Result = result;
        }
    }
}
