using Sitecore.Diagnostics;
using Sitecore.Pipelines.PreprocessRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Evilcore.Web.Pipelines.PreprocessRequest
{
    public class SafewebSuppressFormValidation : PreprocessRequestProcessor
    {
        public override void Process(PreprocessRequestArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            try
            {
                new SuppressFormValidation().Process(args);
            }
            catch (HttpRequestValidationException)
            {
                string rawUrl = args.Context.Request.RawUrl;
                if (!rawUrl.StartsWith("/sitecore/shell/", System.StringComparison.InvariantCultureIgnoreCase) && !rawUrl.StartsWith("/sitecore/admin/", System.StringComparison.InvariantCultureIgnoreCase) && !rawUrl.StartsWith("/-/speak/request/", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    throw;
                }
            }
        }
    }
}