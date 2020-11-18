namespace JobPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Newtabledelete : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VacancyMatchings", "Url", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.VacancyMatchings", "Url");
        }
    }
}
