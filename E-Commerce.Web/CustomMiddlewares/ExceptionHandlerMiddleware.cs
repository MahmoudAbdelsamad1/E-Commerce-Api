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

                if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound) {

                    var response = new ProblemDetails()
                    {
                        Detail = "Error while processing HTTP request - not found",
                        Title = $"Endpoint {httpContext.Request.Path} Not found  " ,
                         Status = StatusCodes.Status404NotFound,
                         Instance = httpContext.Request.Path
                    };

                   await  httpContext.Response.WriteAsJsonAsync(response);
                
                }
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
