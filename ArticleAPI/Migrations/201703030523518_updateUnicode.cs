namespace ArticleAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateUnicode : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.menu", "title", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.page", "url", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.page", "title", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.page", "contents", c => c.String(nullable: false));
            AlterColumn("dbo.post", "title", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.post", "texts", c => c.String(nullable: false));
            AlterColumn("dbo.post", "image", c => c.String());
            AlterColumn("dbo.post", "author", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.category", "name", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.category", "description", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.category", "description", c => c.String(maxLength: 50, unicode: true));
            AlterColumn("dbo.category", "name", c => c.String(nullable: false, maxLength: 30, unicode: true));
            AlterColumn("dbo.post", "author", c => c.String(nullable: false, maxLength: 30, unicode: true));
            AlterColumn("dbo.post", "image", c => c.String(unicode: true));
            AlterColumn("dbo.post", "texts", c => c.String(nullable: false, unicode: true));
            AlterColumn("dbo.post", "title", c => c.String(nullable: false, maxLength: 50, unicode: true));
            AlterColumn("dbo.page", "contents", c => c.String(nullable: false, unicode: true));
            AlterColumn("dbo.page", "title", c => c.String(nullable: false, maxLength: 50, unicode: true));
            AlterColumn("dbo.page", "url", c => c.String(nullable: false, maxLength: 100, unicode: true));
            AlterColumn("dbo.menu", "title", c => c.String(nullable: false, maxLength: 30, unicode: true));
        }
    }
}
