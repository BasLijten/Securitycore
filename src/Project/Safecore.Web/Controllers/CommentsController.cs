using Safecore.Web.DAL;
using Sitecore.Mvc.Presentation;
using SitecoreSecurity.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Safecore.Web.Controllers
{
    public class CommentsController : Controller
    {
        private UserInteractionContext db = new UserInteractionContext();
        // GET: Comments
        public ActionResult Index()
        {            
            return View(db.Comments.ToList());
        }
        
        public ActionResult AddComment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddComment(CommentModel model)
        {
            IView pageView = PageContext.Current.PageView;
            if(pageView==null)
            {
                return null;
            }
            else
            {
                db.Comments.Add(model);
                db.SaveChanges();
                return View(model);
            }          
        }
    }
}