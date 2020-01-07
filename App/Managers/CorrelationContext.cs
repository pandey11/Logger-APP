using App.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace App.Managers
{

    public class CorrelationContext
    {
        public string SessionId { get; }

        public string RootActivityId { get; }

        public string Application { get; }

        public readonly ClaimsPrincipal contextUser;

        public string EncryptedUserToken { get; set; }

        public CorrelationContext(HttpContext httpContext)
        {
            this.SessionId = httpContext.Request.Headers["SessionId"];
            this.RootActivityId = httpContext.Request.Headers["RootActivityId"];
            if (httpContext != null && httpContext.User != null && httpContext.User.Identity.IsAuthenticated)
            {
                this.contextUser = httpContext.User;
                this.EncryptedUserToken = httpContext.Request.Headers["Authorization"][0].Substring("Bearer ".Length);
            }
        }
    }
}
