using ECommerce.Service.Abstraction.ICacheService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Presintation.Attributes
{
    public class RedisCacheAttribute : ActionFilterAttribute
    {
        private readonly int _timeToLive;
        public RedisCacheAttribute(int timeToLiveOnMinutes = 10 )
        {
            _timeToLive = timeToLiveOnMinutes;
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // var services = context.HttpContext.RequestServices.GetService(ICa)

            var myService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            var key = CreateKey(context.HttpContext.Request);

            var data = await myService.GetAsync(key);
            if(data is not null)
            {
                context.Result = new ContentResult()
                {
                    Content = data,
                    StatusCode = StatusCodes.Status200OK,
                    ContentType = "application/Json"
                    
                };
                return;
            }

           var executedResult =  await next.Invoke();
            if (executedResult.Result is ObjectResult result) {

              await  myService.SetAsync(key, result, TimeSpan.FromMinutes(_timeToLive));
            }

        }

        private string CreateKey(HttpRequest bath)
        {
            StringBuilder key = new StringBuilder();
            key.Append(bath);
            foreach (var parameter in bath.Query.OrderBy(B => B.Key))
            {

                key.Append($"|{parameter.Key}-{parameter.Value}");

            }
            return key.ToString();
        }
    }
}

