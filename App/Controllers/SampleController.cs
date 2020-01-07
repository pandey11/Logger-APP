using App.Interfaces;
using App.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    [ApiController]
    [Route("sample")]
    public class SampleController : BaseController
    {
        private readonly ConfigurationManager configurationManager;
        private readonly ILogger logger;

        public SampleController(HttpContext httpContext, ILogger logger): base(httpContext)
        {
            this.configurationManager = ConfigurationManager.Instance;
            this.logger = logger;
        }

        [HttpGet("response")]
        public string GetSample()
        {
           this.logger.LogInformation("GetSample: Recieved Request");
           return "SampleController";
        }
    }
}
