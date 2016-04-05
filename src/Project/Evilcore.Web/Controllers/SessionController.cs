using Evilcore.Web.Models;
using Glass.Mapper.Sc;
using Sitecore;
using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evilcore.Web.Controllers
{
    public class SessionsController : Controller
    {
        // GET: Session
        public ActionResult SessionDetails()
        {
            var context = new SitecoreContext();
            var currentItem = context.GetCurrentItem<ISessionDetail>();
            return View(currentItem);
        }

        public ActionResult AllSessions()
        {

            var context = new SitecoreContext();            
            var currentItem = context.GetCurrentItem<ISessionOverview>();            
            return View(currentItem);
        }
    }
}