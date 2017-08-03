namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoviesModification : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Movies", name: "Genre_Id", newName: "GenreId");
            RenameIndex(table: "dbo.Movies", name: "IX_Genre_Id", newName: "IX_GenreId");
            DropColumn("dbo.Movies", "GenderId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "GenderId", c => c.Byte(nullable: false));
            RenameIndex(table: "dbo.Movies", name: "IX_GenreId", newName: "IX_Genre_Id");
            RenameColumn(table: "dbo.Movies", name: "GenreId", newName: "Genre_Id");
        }
    }
}
