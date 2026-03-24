using System.Net;
using System.Text.Json;

namespace EmployeeManagementAPI.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (KeyNotFoundException ex)
            {
                await HandleException(context, ex.Message, HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Employee not found") || ex.Message.Contains("Invalid password"))
                {
                    await HandleException(context, "Invalid email or password", HttpStatusCode.Unauthorized);
                }
                else
                {
                    await HandleException(context, "Server error", HttpStatusCode.InternalServerError);
                }
            }
        }

        private static Task HandleException(HttpContext context, string message, HttpStatusCode statusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var result = JsonSerializer.Serialize(new
            {
                StatusCode = context.Response.StatusCode,
                Message = message
            });

            return context.Response.WriteAsync(result);
        }
    }
}
