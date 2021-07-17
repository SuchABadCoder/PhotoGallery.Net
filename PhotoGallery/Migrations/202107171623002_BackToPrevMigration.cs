namespace PhotoGallery.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BackToPrevMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Photos", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Photo_Id", "dbo.Photos");
            DropForeignKey("dbo.Photos", "Owner_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Photos", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Photos", new[] { "Owner_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Photo_Id" });
            CreateTable(
                "dbo.ApplicationUserPhotoes",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Photo_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Photo_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Photos", t => t.Photo_Id, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Photo_Id);
            
            DropColumn("dbo.Photos", "ApplicationUser_Id");
            DropColumn("dbo.Photos", "Owner_Id");
            DropColumn("dbo.AspNetUsers", "Photo_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Photo_Id", c => c.Int());
            AddColumn("dbo.Photos", "Owner_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Photos", "ApplicationUser_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.ApplicationUserPhotoes", "Photo_Id", "dbo.Photos");
            DropForeignKey("dbo.ApplicationUserPhotoes", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserPhotoes", new[] { "Photo_Id" });
            DropIndex("dbo.ApplicationUserPhotoes", new[] { "ApplicationUser_Id" });
            DropTable("dbo.ApplicationUserPhotoes");
            CreateIndex("dbo.AspNetUsers", "Photo_Id");
            CreateIndex("dbo.Photos", "Owner_Id");
            CreateIndex("dbo.Photos", "ApplicationUser_Id");
            AddForeignKey("dbo.Photos", "Owner_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUsers", "Photo_Id", "dbo.Photos", "Id");
            AddForeignKey("dbo.Photos", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
