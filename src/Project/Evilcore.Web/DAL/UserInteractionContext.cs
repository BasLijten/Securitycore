using Evilcore.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Evilcore.Web.DAL
{
  public class UserInteractionContext : DbContext
  {
    public DbSet<CommentModel> Comments { get; set; }
    public UserInteractionContext() : base("UserInteractionContext")
    {      
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>
      //      base.OnModelCreating(modelBuilder);
    }
  }
}