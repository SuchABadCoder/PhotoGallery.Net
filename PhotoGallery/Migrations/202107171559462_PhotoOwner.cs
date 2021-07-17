namespace PhotoGallery.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PhotoOwner : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ApplicationUserPhotoes", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplicationUserPhotoes", "Photo_Id", "dbo.Photos");
            DropIndex("dbo.ApplicationUserPhotoes", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplicationUserPhotoes", new[] { "Photo_Id" });
            AddColumn("dbo.Photos", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Photos", "Owner_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "Photo_Id", c => c.Int());
            CreateIndex("dbo.Photos", "ApplicationUser_Id");
            CreateIndex("dbo.Photos", "Owner_Id");
            CreateIndex("dbo.AspNetUsers", "Photo_Id");
            AddForeignKey("dbo.Photos", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUsers", "Photo_Id", "dbo.Photos", "Id");
            AddForeignKey("dbo.Photos", "Owner_Id", "dbo.AspNetUsers", "Id");
            DropTable("dbo.ApplicationUserPhotoes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ApplicationUserPhotoes",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Photo_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Photo_Id });
            
            DropForeignKey("dbo.Photos", "Owner_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Photo_Id", "dbo.Photos");
            DropForeignKey("dbo.Photos", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUsers", new[] { "Photo_Id" });
            DropIndex("dbo.Photos", new[] { "Owner_Id" });
            DropIndex("dbo.Photos", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.AspNetUsers", "Photo_Id");
            DropColumn("dbo.Photos", "Owner_Id");
            DropColumn("dbo.Photos", "ApplicationUser_Id");
            CreateIndex("dbo.ApplicationUserPhotoes", "Photo_Id");
            CreateIndex("dbo.ApplicationUserPhotoes", "ApplicationUser_Id");
            AddForeignKey("dbo.ApplicationUserPhotoes", "Photo_Id", "dbo.Photos", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ApplicationUserPhotoes", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
