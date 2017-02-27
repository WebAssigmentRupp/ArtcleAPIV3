namespace ArticleAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateField : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArtUser",
                c => new
                    {
                        id = c.Short(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 30),
                        email = c.String(nullable: false, maxLength: 30),
                        firstname = c.String(nullable: false, maxLength: 30),
                        lastname = c.String(nullable: false, maxLength: 30),
                        gender = c.String(nullable: false, maxLength: 1),
                        passwd = c.String(),
                        role_id = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.UserRole", t => t.role_id)
                .Index(t => t.role_id);
            
            CreateTable(
                "dbo.menu",
                c => new
                    {
                        id = c.Short(nullable: false, identity: true),
                        title = c.String(nullable: false, maxLength: 30, unicode: false),
                        parent_id = c.Short(),
                        user_id = c.Short(nullable: false),
                        page_id = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.page", t => t.page_id)
                .ForeignKey("dbo.menu", t => t.parent_id)
                .ForeignKey("dbo.ArtUser", t => t.user_id)
                .Index(t => t.parent_id)
                .Index(t => t.user_id)
                .Index(t => t.page_id);
            
            CreateTable(
                "dbo.page",
                c => new
                    {
                        id = c.Short(nullable: false, identity: true),
                        url = c.String(nullable: false, maxLength: 100, unicode: false),
                        title = c.String(nullable: false, maxLength: 50, unicode: false),
                        contents = c.String(nullable: false, unicode: false),
                        user_id = c.Short(nullable: false),
                        created_date = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.ArtUser", t => t.user_id)
                .Index(t => t.user_id);
            
            CreateTable(
                "dbo.post",
                c => new
                    {
                        id = c.Short(nullable: false, identity: true),
                        title = c.String(nullable: false, maxLength: 50, unicode: false),
                        texts = c.String(nullable: false, unicode: false),
                        image = c.String(unicode: false),
                        post_date = c.DateTime(nullable: false, storeType: "date"),
                        author = c.String(nullable: false, maxLength: 30, unicode: false),
                        category_id = c.Short(nullable: false),
                        user_id = c.Short(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.category", t => t.category_id)
                .ForeignKey("dbo.ArtUser", t => t.user_id)
                .Index(t => t.category_id)
                .Index(t => t.user_id);
            
            CreateTable(
                "dbo.category",
                c => new
                    {
                        id = c.Short(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 30, unicode: false),
                        description = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        id = c.Short(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 30),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ArtUser", "role_id", "dbo.UserRole");
            DropForeignKey("dbo.post", "user_id", "dbo.ArtUser");
            DropForeignKey("dbo.post", "category_id", "dbo.category");
            DropForeignKey("dbo.page", "user_id", "dbo.ArtUser");
            DropForeignKey("dbo.menu", "user_id", "dbo.ArtUser");
            DropForeignKey("dbo.menu", "parent_id", "dbo.menu");
            DropForeignKey("dbo.menu", "page_id", "dbo.page");
            DropIndex("dbo.post", new[] { "user_id" });
            DropIndex("dbo.post", new[] { "category_id" });
            DropIndex("dbo.page", new[] { "user_id" });
            DropIndex("dbo.menu", new[] { "page_id" });
            DropIndex("dbo.menu", new[] { "user_id" });
            DropIndex("dbo.menu", new[] { "parent_id" });
            DropIndex("dbo.ArtUser", new[] { "role_id" });
            DropTable("dbo.UserRole");
            DropTable("dbo.category");
            DropTable("dbo.post");
            DropTable("dbo.page");
            DropTable("dbo.menu");
            DropTable("dbo.ArtUser");
        }
    }
}
