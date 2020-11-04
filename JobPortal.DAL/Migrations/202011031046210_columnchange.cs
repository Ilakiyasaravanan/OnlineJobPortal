namespace JobPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class columnchange : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.SearcherJobDetails", name: "CGPA", newName: "CgpaId");
            RenameIndex(table: "dbo.SearcherJobDetails", name: "IX_CGPA", newName: "IX_CgpaId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.SearcherJobDetails", name: "IX_CgpaId", newName: "IX_CGPA");
            RenameColumn(table: "dbo.SearcherJobDetails", name: "CgpaId", newName: "CGPA");
        }
    }
}
