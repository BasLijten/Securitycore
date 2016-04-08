using Glass.Mapper.Sc;
using SitecoreSecurity.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evilcore.Web.Controllers
{
    public class SpeakerController: Controller
    {
        public ActionResult SpeakerDetail()
        {
            var ctx = new SitecoreContext();
            var currentItem = ctx.GetCurrentItem<ISpeaker>();            
            return View(currentItem);
        }

        public ActionResult AllSpeakers()
        {
            var ctx = new SitecoreContext();
            var currentItem = ctx.GetCurrentItem<ISpeakers>();
            return View(currentItem);
        }

    }
}