namespace Evilcore.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentStatusAdded1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CommentModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SessionID = c.String(),
                        Comment = c.String(),
                        Date = c.DateTime(nullable: false),
                        UserIdentifier = c.String(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CommentModels");
        }
    }
}
