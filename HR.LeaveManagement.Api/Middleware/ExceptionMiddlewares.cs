using System.Net;
using HR.LeaveManagement.Application.Exceptions;
using Newtonsoft.Json;

namespace HR.LeaveManagement.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await HandleException(httpContext, e);
            }
        }

        private async Task HandleException(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.ContentType = "application/json";
            string result = JsonConvert.SerializeObject(new {eror = ex.Message});

            var statusCode = ex switch
            {
                (DirectoryNotFoundException) => HttpStatusCode.NotFound,
                (ValidationException) => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };



            httpContext.Response.StatusCode = (int)statusCode;
            await httpContext.Response.WriteAsync(result);
        }
    }
}
