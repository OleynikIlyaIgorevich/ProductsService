using System.Net;
using System.Text.Json;

namespace ProductsService.Api.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next) => _next = next;
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "text/plain";

            response.StatusCode = error switch
            {
                _ => (int)HttpStatusCode.InternalServerError
            };

            await response.WriteAsync(error.Message);
        }
    }
}
