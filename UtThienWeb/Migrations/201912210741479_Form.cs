namespace UtThienWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Form : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Form",
                c => new
                    {
                        FormId = c.Int(nullable: false, identity: true),
                        FormEmail = c.String(maxLength: 50),
                        FormName = c.String(maxLength: 60),
                        FormPhone = c.String(maxLength: 12),
                        NewsId = c.Int(nullable: false),
                        NewsTitle = c.String(),
                    })
                .PrimaryKey(t => t.FormId)
                .ForeignKey("dbo.News", t => t.NewsId, cascadeDelete: true)
                .Index(t => t.NewsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Form", "NewsId", "dbo.News");
            DropIndex("dbo.Form", new[] { "NewsId" });
            DropTable("dbo.Form");
        }
    }
}
