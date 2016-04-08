using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Pipelines.HttpRequest;

namespace Safecore.Web.Pipelines.HttpRequest
{
    public class AddSecurityHeaders : HttpRequestProcessor
    {
        public override void Process(HttpRequestArgs args)
        {
            args.Context.Response.Headers.Add("Content-Security-Policy", "default-src 'self' ;");
            args.Context.Response.Headers.Add("X-Frame-Options", "DENY");
            args.Context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
        }
    }
}