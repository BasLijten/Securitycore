using Sitecore.Analytics;
using Sitecore.Analytics.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Safecore.Web.Controllers
{
    public class HiItsMeController : Controller
    {
        // GET: HiItsMe
        public ActionResult HiItsMe()
        {
            var name = String.Empty;      
            if(Tracker.Current!=null && Tracker.IsActive)
            {
                var contact = Tracker.Current.Contact;
                var data = contact.GetFacet<IContactPersonalInfo>("Personal");
                if (data != null && !String.IsNullOrEmpty(data.FirstName) && !String.IsNullOrEmpty(data.Surname))
                    name = String.Format("{0} {1}", data.FirstName, data.Surname);                                         
            }
            ViewBag.Name = name;
            return View();
        }
    }
}