using Sitecore.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Safecore.Web.Pipelines
{
    public class RegisterHttpRoutes
    {
        public void Process(PipelineArgs args)
        {
            //RouteTable.Routes.MapMvcAttributeRoutes();
            GlobalConfiguration.Configure(Configure);
        }

        private void Configure(HttpConfiguration configuration)
        {
            var routes = configuration.Routes;
            routes.MapHttpRoute("DefaultApiRoute", "api/{controller}/{action}/{id}", new { action="Index", id=RouteParameter.Optional });
        }
    }
}