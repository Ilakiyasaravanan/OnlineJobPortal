using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using JobPortal.Entity;
namespace JobPortal.DAL
{
	class DBUtills : DbContext
	{
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<AccountDetails>()
					.MapToStoredProcedures(p => p.Insert(sp => sp.HasName("sp_InsertAccountDetails"))
							.Update(sp => sp.HasName("sp_UpdateAccountDetails"))
							.Delete(sp => sp.HasName("sp_DeleteAccountDetails"))
					);
			
		}
		public DBUtills() : base("DbConnection") { }
		public DbSet<AccountDetails> AccountDb { get; set; }
		public DbSet<RecruiterJobDetails> RecruiterDb { get; set; }
		public DbSet<SearcherJobDetails> SearcherDb { get; set; }
	
		public DbSet<JobTypes> JobTypeDb { get; set; }
		public DbSet<Locations> LocationDb { get; set; }
		public DbSet<WorkExperiences> WorkExperienceDb { get; set; }
		public DbSet<RecruiterProfile> ProfileDb {get;set;}
		public DbSet<SearcherSkillSets> SkillDb { get; set; }

		public DbSet<Resume> ResumeDb { get; set; }
		public DbSet<Cgpas> CgpaDb { get; set; }




	}
}
