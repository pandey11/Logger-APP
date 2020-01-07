using App.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App
{
    public abstract class BaseController : ControllerBase
    {
        public BaseController(HttpContext httpContext)
        {
            var correlationContext = new CorrelationContext(httpContext);
            ServiceContext.Push(correlationContext);
        }
    }
}
