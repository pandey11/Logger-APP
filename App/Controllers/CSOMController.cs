using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.CompliancePolicy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Controllers
{
    [Route("csom")]
    [ApiController]
    public class CSOMController : BaseController
    {
        private ClientContext SPClientContext;
        public CSOMController(HttpContext httpContext) : base(httpContext)
        {
             this.SPClientContext = new ClientContext("http://gopandeyodspc:9000");
        }

        [HttpGet("applyLabel")]
        public bool ApplySensitivityLabel()
        {
            string actionsPayload = "{\"ExecuteAction\":[{\"Arguments\":[{\"Value\":\"57704cc5-316c-4d1c-9c15-d489dc810bac\",\"Values\":null}],\"Name\":\"StampLabel\",\"Properties\":{\"externalName\":\"StampLabel\"}}]}";
            SPPolicyStoreProxy policyStoreProxy = new SPPolicyStoreProxy(SPClientContext, SPClientContext.Site.RootWeb);
            SPPolicyStoreProxy.ApplyDlpActions(SPClientContext, "http://gopandeyodspc:9000/Shared Documents/Document6.docx", actionsPayload);
            SPClientContext.Load(policyStoreProxy);
            SPClientContext.ExecuteQuery();
            return true;
        }
    }
}