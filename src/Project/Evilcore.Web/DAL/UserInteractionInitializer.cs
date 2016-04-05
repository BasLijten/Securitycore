using Evilcore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Evilcore.Web.DAL
{
  public class UserInteractionInitializer: System.Data.Entity.DropCreateDatabaseIfModelChanges<UserInteractionContext>
  {
    protected override void Seed(UserInteractionContext context)
    {
      var comments = new List<CommentModel>
      {
        new CommentModel {Comment="bla", Date=DateTime.Today,UserIdentifier="bas.lijten@gmail.com", SessionID= "1", Status="Not Approved" }
      };

      comments.ForEach(c => context.Comments.Add(c));
      context.SaveChanges();      
    }
  }
}