namespace JobPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dob : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountDetails",
                c => new
                    {
                        AccountId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 30),
                        LastName = c.String(maxLength: 30),
                        Address = c.String(nullable: false, maxLength: 50),
                        Gender = c.String(nullable: false, maxLength: 6),
                        PhoneNumber = c.Long(nullable: false),
                        Password = c.String(nullable: false, maxLength: 30),
                        Role = c.String(nullable: false, maxLength: 10),
                        Email = c.String(nullable: false, maxLength: 25),
                        DateofBirth = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AccountId)
                .Index(t => t.Email, unique: true);
            
            CreateTable(
                "dbo.Cgpas",
                c => new
                    {
                        CgpaId = c.Int(nullable: false, identity: true),
                        CGPA = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.CgpaId);
            
            CreateTable(
                "dbo.JobTypes",
                c => new
                    {
                        JobTypeId = c.Int(nullable: false, identity: true),
                        JobType = c.String(),
                    })
                .PrimaryKey(t => t.JobTypeId);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        LocationId = c.Int(nullable: false, identity: true),
                        Location = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.LocationId);
            
            CreateTable(
                "dbo.RecruiterProfiles",
                c => new
                    {
                        ProfileId = c.Int(nullable: false, identity: true),
                        WorkingCompany = c.String(maxLength: 20),
                        WorkingLocation = c.String(maxLength: 20),
                        Position = c.String(maxLength: 20),
                        CompanyDescription = c.String(maxLength: 30),
                        AccountId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProfileId)
                .ForeignKey("dbo.AccountDetails", t => t.AccountId, cascadeDelete: true)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.RecruiterJobDetails",
                c => new
                    {
                        RecuiterDetailId = c.Int(nullable: false, identity: true),
                        AccountId = c.Int(nullable: false),
                        CompanyName = c.String(nullable: false, maxLength: 30),
                        JobTypeId = c.Int(nullable: false),
                        Graduation = c.Int(nullable: false),
                        WorkExperience = c.Short(nullable: false),
                        Salary = c.String(),
                        LocationId = c.Int(nullable: false),
                        Location = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RecuiterDetailId)
                .ForeignKey("dbo.AccountDetails", t => t.AccountId, cascadeDelete: true)
                .ForeignKey("dbo.JobTypes", t => t.JobTypeId, cascadeDelete: true)
                .Index(t => t.AccountId)
                .Index(t => t.JobTypeId);
            
            CreateTable(
                "dbo.Resumes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        AccountId = c.Int(nullable: false),
                        FileName = c.String(),
                        ContentType = c.String(),
                        Data = c.Binary(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AccountDetails", t => t.AccountId, cascadeDelete: true)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.SearcherJobDetails",
                c => new
                    {
                        SearcherDetailId = c.Int(nullable: false, identity: true),
                        AccountId = c.Int(nullable: false),
                        JobTypeId = c.Int(nullable: false),
                        Graduation = c.Int(nullable: false),
                        WorkExperience = c.Short(nullable: false),
                        LocationId = c.Int(nullable: false),
                        location = c.Int(nullable: false),
                        Domain = c.String(nullable: false, maxLength: 5),
                        CGPA = c.Single(nullable: false),
                        Department = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.SearcherDetailId)
                .ForeignKey("dbo.AccountDetails", t => t.AccountId, cascadeDelete: true)
                .ForeignKey("dbo.JobTypes", t => t.JobTypeId, cascadeDelete: true)
                .Index(t => t.AccountId)
                .Index(t => t.JobTypeId);
            
            CreateTable(
                "dbo.SearcherSkillSets",
                c => new
                    {
                        SkillId = c.Int(nullable: false, identity: true),
                        AccountId = c.Int(nullable: false),
                        SoftwareSkills = c.String(maxLength: 30),
                        Achievements = c.String(maxLength: 100),
                        Certifications = c.String(maxLength: 100),
                        LanguageKnown = c.Int(name: "Language Known", nullable: false),
                        Sports = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.SkillId)
                .ForeignKey("dbo.AccountDetails", t => t.AccountId, cascadeDelete: true)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.WorkExperiences",
                c => new
                    {
                        WorkExperienceId = c.Int(nullable: false, identity: true),
                        AccountId = c.Int(nullable: false),
                        WorkCompanyName = c.String(maxLength: 30),
                        CompletedExpereince = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.WorkExperienceId)
                .ForeignKey("dbo.AccountDetails", t => t.AccountId, cascadeDelete: true)
                .Index(t => t.AccountId);
            
            CreateStoredProcedure(
                "dbo.sp_InsertAccountDetails",
                p => new
                    {
                        FirstName = p.String(maxLength: 30),
                        LastName = p.String(maxLength: 30),
                        Address = p.String(maxLength: 50),
                        Gender = p.String(maxLength: 6),
                        PhoneNumber = p.Long(),
                        Password = p.String(maxLength: 30),
                        Role = p.String(maxLength: 10),
                        Email = p.String(maxLength: 25),
                        DateofBirth = p.DateTime(),
                    },
                body:
                    @"INSERT [dbo].[AccountDetails]([FirstName], [LastName], [Address], [Gender], [PhoneNumber], [Password], [Role], [Email], [DateofBirth])
                      VALUES (@FirstName, @LastName, @Address, @Gender, @PhoneNumber, @Password, @Role, @Email, @DateofBirth)
                      
                      DECLARE @AccountId int
                      SELECT @AccountId = [AccountId]
                      FROM [dbo].[AccountDetails]
                      WHERE @@ROWCOUNT > 0 AND [AccountId] = scope_identity()
                      
                      SELECT t0.[AccountId]
                      FROM [dbo].[AccountDetails] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[AccountId] = @AccountId"
            );
            
            CreateStoredProcedure(
                "dbo.sp_UpdateAccountDetails",
                p => new
                    {
                        AccountId = p.Int(),
                        FirstName = p.String(maxLength: 30),
                        LastName = p.String(maxLength: 30),
                        Address = p.String(maxLength: 50),
                        Gender = p.String(maxLength: 6),
                        PhoneNumber = p.Long(),
                        Password = p.String(maxLength: 30),
                        Role = p.String(maxLength: 10),
                        Email = p.String(maxLength: 25),
                        DateofBirth = p.DateTime(),
                    },
                body:
                    @"UPDATE [dbo].[AccountDetails]
                      SET [FirstName] = @FirstName, [LastName] = @LastName, [Address] = @Address, [Gender] = @Gender, [PhoneNumber] = @PhoneNumber, [Password] = @Password, [Role] = @Role, [Email] = @Email, [DateofBirth] = @DateofBirth
                      WHERE ([AccountId] = @AccountId)"
            );
            
            CreateStoredProcedure(
                "dbo.sp_DeleteAccountDetails",
                p => new
                    {
                        AccountId = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[AccountDetails]
                      WHERE ([AccountId] = @AccountId)"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.sp_DeleteAccountDetails");
            DropStoredProcedure("dbo.sp_UpdateAccountDetails");
            DropStoredProcedure("dbo.sp_InsertAccountDetails");
            DropForeignKey("dbo.WorkExperiences", "AccountId", "dbo.AccountDetails");
            DropForeignKey("dbo.SearcherSkillSets", "AccountId", "dbo.AccountDetails");
            DropForeignKey("dbo.SearcherJobDetails", "JobTypeId", "dbo.JobTypes");
            DropForeignKey("dbo.SearcherJobDetails", "AccountId", "dbo.AccountDetails");
            DropForeignKey("dbo.Resumes", "AccountId", "dbo.AccountDetails");
            DropForeignKey("dbo.RecruiterJobDetails", "JobTypeId", "dbo.JobTypes");
            DropForeignKey("dbo.RecruiterJobDetails", "AccountId", "dbo.AccountDetails");
            DropForeignKey("dbo.RecruiterProfiles", "AccountId", "dbo.AccountDetails");
            DropIndex("dbo.WorkExperiences", new[] { "AccountId" });
            DropIndex("dbo.SearcherSkillSets", new[] { "AccountId" });
            DropIndex("dbo.SearcherJobDetails", new[] { "JobTypeId" });
            DropIndex("dbo.SearcherJobDetails", new[] { "AccountId" });
            DropIndex("dbo.Resumes", new[] { "AccountId" });
            DropIndex("dbo.RecruiterJobDetails", new[] { "JobTypeId" });
            DropIndex("dbo.RecruiterJobDetails", new[] { "AccountId" });
            DropIndex("dbo.RecruiterProfiles", new[] { "AccountId" });
            DropIndex("dbo.AccountDetails", new[] { "Email" });
            DropTable("dbo.WorkExperiences");
            DropTable("dbo.SearcherSkillSets");
            DropTable("dbo.SearcherJobDetails");
            DropTable("dbo.Resumes");
            DropTable("dbo.RecruiterJobDetails");
            DropTable("dbo.RecruiterProfiles");
            DropTable("dbo.Locations");
            DropTable("dbo.JobTypes");
            DropTable("dbo.Cgpas");
            DropTable("dbo.AccountDetails");
        }
    }
}
