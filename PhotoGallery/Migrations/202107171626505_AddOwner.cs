namespace PhotoGallery.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOwner : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Photos", "OwnerName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Photos", "OwnerName");
        }
    }
}
