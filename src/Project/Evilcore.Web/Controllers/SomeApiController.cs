using Newtonsoft.Json;
using Evilcore.Web.DAL;
using Sitecore.Services.Core;
using Sitecore.Services.Infrastructure.Web.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using SitecoreSecurity.Web.Models;

namespace Evilcore.Web.Controllers
{        
    public class SomeApiController : ServicesApiController
    {
        private UserInteractionContext db = new UserInteractionContext();
        public SomeApiController()
        {
            db.Database.Log = Console.Write;
        }
        // GET: SomeApi
        [HttpGet]
        public IHttpActionResult GetItems(int id)
        {
            var comment = new CommentModel { Comment = "a" };
            return new JsonResult<CommentModel>(comment, new JsonSerializerSettings(), Encoding.UTF8, this);
        }

        [HttpGet]
        public IHttpActionResult GetAllItems()
        {
            var comments = db.Comments.ToList();           
            return new JsonResult<IList<CommentModel>>(comments, new JsonSerializerSettings(), Encoding.UTF8, this);
        }

        [HttpGet]
        public IHttpActionResult GetAllCommentsForSession(string SessionID)
        {
            var commentsQuery = db.Comments.Where(c => c.SessionID == SessionID);
            var comments = commentsQuery.ToList();
            return new JsonResult<IList<CommentModel>>(comments, new JsonSerializerSettings(), Encoding.UTF8, this);
        }

        [HttpGet]
        public IHttpActionResult GetAllCommentsForUser(string User)
        {
            var commentsQuery = db.Comments.SqlQuery(String.Format("Select * From dbo.CommentModels Where UserIdentifier like '{0}'", User));            
            var comments = commentsQuery.ToList();
            return new JsonResult<IList<CommentModel>>(comments, new JsonSerializerSettings(), Encoding.UTF8, this);
        }

        [HttpPost]
        // http://Evilcore.local/basapi/SomeApi/NewComment/
        // Content-Type:  application/x-www-form-urlencoded
        // raw body: =<script>bla</script>
        public IHttpActionResult NewComment([FromBody]string id)
        {
            var comment = new CommentModel { Comment = "a" };
            return new JsonResult<CommentModel>(comment, new JsonSerializerSettings(), Encoding.UTF8, this);
        }

        [HttpPost]
        // http://Evilcore.local/basapi/SomeApi/NewComment/
        // Content-Type:  application/json
        // raw body: 
        // { ID:1, Comment: "bla" }
        //
        //
        public IHttpActionResult AddComment(CommentModel model)
        {
            model.Date = DateTime.Now;
            model.Status = "unapproved";
            if (Sitecore.Context.User.IsAuthenticated)
                model.UserIdentifier = Sitecore.Context.User.Name;
            else
                model.UserIdentifier = "Anonymous";

            db.Comments.Add(model);
            db.SaveChanges();
            return new JsonResult<CommentModel>(model, new JsonSerializerSettings(), Encoding.UTF8, this);
        }
    }
}