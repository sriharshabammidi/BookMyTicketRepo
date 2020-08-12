using System;
using System.Net;

namespace BookMyTicket.Models.Core
{
    public class ApiResponse<T>
    {
        public ApiResponse(HttpStatusCode status, T result, ApiResponseErrorResult error = null)
        {
            Status = status;
            Result = result;
            Error = error;
        }

        public HttpStatusCode Status { get; set; }
        public T Result { get; set; }
        public ApiResponseErrorResult Error { get; set; }
    }

    public class ApiResponseErrorResult
    {
        public ApiResponseErrorResult()
        {
        }

        public ApiResponseErrorResult(object exception, string message)
        {
            Exception = exception;
            Message = message;
        }

        public object Exception { get; set; }

        public string Message { get; set; }
    }

    public class AppValidationException : Exception
    {
        public AppValidationException(string message)
    : base(String.Format(message))
        {
        }

        public AppValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
