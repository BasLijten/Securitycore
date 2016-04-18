using Newtonsoft.Json;
using Safecore.Web.DAL;
using SitecoreSecurity.Web.Models;
using Sitecore.Services.Core;
using Sitecore.Services.Infrastructure.Web.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Data.SqlClient;

namespace Safecore.Web.Controllers
{        
    public class CommentsApiController : ServicesApiController
    {
        private UserInteractionContext db = new UserInteractionContext();
        public CommentsApiController()
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
            foreach(var c in comments)
            {
                c.Comment = c.Comment;
            }
            return new JsonResult<IList<CommentModel>>(comments, new JsonSerializerSettings(), Encoding.UTF8, this);
        }

        [HttpGet]
        public IHttpActionResult GetAllCommentsForUser(string User)
        {
            var user = new SqlParameter("User", User);
            var commentsQuery = db.Comments.SqlQuery("Select * From dbo.CommentModels Where UserIdentifier like {0}", user);
            var comments = commentsQuery.ToList();
            return new JsonResult<IList<CommentModel>>(comments, new JsonSerializerSettings(), Encoding.UTF8, this);
        }

        [HttpPost]
        // http://safecore.local/basapi/SomeApi/NewComment/
        // Content-Type:  application/x-www-form-urlencoded
        // raw body: =<script>bla</script>
        public IHttpActionResult NewComment([FromBody]string id)
        {
            var comment = new CommentModel { Comment = "a" };
            return new JsonResult<CommentModel>(comment, new JsonSerializerSettings(), Encoding.UTF8, this);
        }

        [HttpPost]
        // http://safecore.local/basapi/SomeApi/NewComment/
        // Content-Type:  application/json
        // raw body: 
        // { ID:1, Comment: "bla" }
        //
        //
        public IHttpActionResult AddComment(CommentModel model)
        {
            db.Comments.Add(model);
            db.SaveChanges();
            return new JsonResult<CommentModel>(model, new JsonSerializerSettings(), Encoding.UTF8, this);
        }
    }
}