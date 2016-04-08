using Sitecore.Pipelines.HttpRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Safecore.Web.Pipelines
{
  public class AbortSitecoreApiServicesRouteForKnownRoutes:HttpRequestProcessor
  {
        public override void Process(HttpRequestArgs args)
        {
            var routeCollection = RouteTable.Routes;
            var routeData = routeCollection.GetRouteData(new HttpContextWrapper(args.Context));

            if (routeData == null || args.Url.ItemPath.Contains("sitecore/api")) return;

            HttpContext.Current.RemapHandler(routeData.RouteHandler.GetHttpHandler(HttpContext.Current.Request.RequestContext));
            args.AbortPipeline();
        }
    }
}