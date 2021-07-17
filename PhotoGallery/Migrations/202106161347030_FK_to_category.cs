namespace PhotoGallery.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FK_to_category : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Photos", name: "Category_Id", newName: "CategoryId");
            RenameIndex(table: "dbo.Photos", name: "IX_Category_Id", newName: "IX_CategoryId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Photos", name: "IX_CategoryId", newName: "IX_Category_Id");
            RenameColumn(table: "dbo.Photos", name: "CategoryId", newName: "Category_Id");
        }
    }
}
