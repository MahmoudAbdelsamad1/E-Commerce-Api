using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace E_Commerce.Web.CustomMiddlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware>  _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)   
        {
            try {

               await _next.Invoke(httpContext);
            } catch (Exception ex) {

                _logger.LogError(ex, ex.Message);

                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var problem = new ProblemDetails()
                {

                    Title = "Internal Server Error ",
                    Detail = ex.Message,
                    Status = StatusCodes.Status500InternalServerError,
                    Instance = httpContext.Request.Path

                };

                 await httpContext.Response.WriteAsJsonAsync(problem);



            }
        }
    }
}
