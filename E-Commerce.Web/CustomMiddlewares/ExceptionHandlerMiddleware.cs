using ECommerce.Service.CustomExceptions;
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


                var problem = new ProblemDetails()
                {

                    Title = "Error while processing HTTP request",
                    Detail = ex.Message,
                    Status = ex switch {
                        NonFoundException => StatusCodes.Status404NotFound,
                        _ => StatusCodes.Status500InternalServerError
                    },
                    Instance = httpContext.Request.Path

                };
                httpContext.Response.StatusCode = problem.Status.Value;


                await httpContext.Response.WriteAsJsonAsync(problem);



            }
        }
    }
}
