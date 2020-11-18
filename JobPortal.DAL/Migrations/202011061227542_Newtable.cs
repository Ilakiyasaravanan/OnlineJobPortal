namespace JobPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Newtable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VacancyMatchings",
                c => new
                    {
                        VacancyId = c.Int(nullable: false, identity: true),
                        Searcher_AccountId = c.Int(nullable: false),
                        ResumeId = c.Int(nullable: false),
                        Recruiterid = c.Int(nullable: false),
                        Recruiter_AccountId = c.Int(nullable: false),
                        Message = c.String(maxLength: 200),
                        Recruiter_RecuiterDetailId = c.Int(),
                        Url=c.Int(nullable:false)
                    })
                .PrimaryKey(t => t.VacancyId)
                .ForeignKey("dbo.RecruiterJobDetails", t => t.Recruiter_RecuiterDetailId)
                .ForeignKey("dbo.Resumes", t => t.ResumeId, cascadeDelete: true)
                .Index(t => t.ResumeId)
                .Index(t => t.Recruiter_RecuiterDetailId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VacancyMatchings", "ResumeId", "dbo.Resumes");
            DropForeignKey("dbo.VacancyMatchings", "Recruiter_RecuiterDetailId", "dbo.RecruiterJobDetails");
            DropIndex("dbo.VacancyMatchings", new[] { "Recruiter_RecuiterDetailId" });
            DropIndex("dbo.VacancyMatchings", new[] { "ResumeId" });
            DropTable("dbo.VacancyMatchings");
        }
    }
}
