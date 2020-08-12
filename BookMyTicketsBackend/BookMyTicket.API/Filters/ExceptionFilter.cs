using BookMyTicket.Core.Logger;
using BookMyTicket.Models.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Net;

namespace BookMyTicket.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private ILogger _logger { get; set; }

        public ExceptionFilter(ILogger logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            HttpStatusCode status = HttpStatusCode.InternalServerError;
            var message = "Server error occurred.";

            _logger.LogError(context.Exception, context.Exception.Message);

            context.ExceptionHandled = true;
            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = (int)status;
            response.ContentType = "application/json";

            if (context.Exception is AppValidationException)
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Result = new ObjectResult(new ApiResponse<object>(HttpStatusCode.BadRequest, null, new ApiResponseErrorResult(context.Exception.InnerException, context.Exception.Message)));
            }
            else
            {
                context.Result = new ObjectResult(new ApiResponse<object>(HttpStatusCode.InternalServerError, null, new ApiResponseErrorResult(JsonConvert.SerializeObject(context.Exception), message)));
            }
        }
    }
}
