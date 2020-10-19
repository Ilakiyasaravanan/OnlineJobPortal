using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using JobPortal.Common;
using JobPortal.Entity;
namespace JobPortal.DAL
{
	[ExceptionHandler]
	public class JobRepository : IJobRepository
	{
		/*Recruiter*/
		public void AddRecruiterDetails(RecruiterJobDetails job)   //Insert recruiter vacancy 
		{
			using (DBUtills db = new DBUtills())
			{
				db.RecruiterDb.Add(job);
				db.SaveChanges();
			}
		}
		
		public IEnumerable<RecruiterJobDetails> FetchRecruiterDetails(int recruiterId)//Extract vacancy of recruiter
		{		
			
			IEnumerable<RecruiterJobDetails> account = null;
			using (DBUtills db = new DBUtills())
			{
				account = db.RecruiterDb.Include("Jobtype").Where(s => s.AccountId == recruiterId).ToList();
				return account;
			}
		}
		public IEnumerable<RecruiterJobDetails> FetchRecruiterVacancies()//Get vacancy of recruiters
		{
			IEnumerable<RecruiterJobDetails> recruit = null;
			using (DBUtills dB = new DBUtills())
			{
				recruit = dB.RecruiterDb.Include("Jobtype").Include("Account").ToList();
				return recruit;
			}
		}
		public void AddProfile(RecruiterProfile profile)//Add profile
		{
			using (DBUtills db = new DBUtills())
			{
				db.ProfileDb.Add(profile);
				db.SaveChanges();
			}
		}
		public RecruiterProfile FetchProfile(int log)//Fetch profile
		{
			RecruiterProfile profile = null;
			using (DBUtills db = new DBUtills())
			{		

				profile = db.ProfileDb.FirstOrDefault(id=>id.AccountId==log);
				return profile;
			}
		}

		public bool UpdateProfile(RecruiterProfile profile)  //Update profile 
		{
			bool status = false;
			using (DBUtills dBUtills = new DBUtills())
			{
				dBUtills.Entry(profile).State = EntityState.Modified;
				dBUtills.SaveChanges();
				status = true;

			}
			return status;
		}
			
		public void RemoveVacancy(int idValue)  //Delete vacancy
		{
			using (DBUtills dBUtills = new DBUtills())
			{
				RecruiterJobDetails loc = dBUtills.RecruiterDb.Find(idValue);
				dBUtills.RecruiterDb.Remove(loc);
				dBUtills.SaveChanges();
			}
		}
		
		/*Searcher*/
		public IEnumerable<SearcherJobDetails> FetchSearcherApplications()//Getting applications of searcher
		{
			IEnumerable<SearcherJobDetails> searchers = null;

			using (DBUtills dB = new DBUtills())
			{
				searchers = dB.SearcherDb.Include("Jobtype").Include("Account").ToList();
				return searchers;
			}
		}
	
		public IEnumerable<SearcherJobDetails> FetchCandidate(int recruiterId) //Extract applied job of searcher
		{
			IEnumerable<SearcherJobDetails> account = null;
			using (DBUtills db = new DBUtills())
			{
				account = db.SearcherDb.Include("Jobtype").Where(s => s.AccountId == recruiterId).ToList();
				return account;
			}
		}
		public void AddCandidateDetails(SearcherJobDetails job)//Insert searcher application
		{
			using (DBUtills db = new DBUtills())
			{

				db.SearcherDb.Add(job);
				db.SaveChanges();
			}
		}
		public SearcherSkillSets FetchSkill(int log)//Check Skill Exists
		{
			using (DBUtills db = new DBUtills())
			{
				SearcherSkillSets output = null;
				output = db.SkillDb.Find(log);
				return output;
			}
		}
		public SearcherSkillSets FetchIndividualSkill(int log)//Fetch skills
		{
			SearcherSkillSets skill = null;
			using (DBUtills dB = new DBUtills())
			{

				skill = dB.SkillDb.FirstOrDefault(id => id.AccountId == log);
				return skill;
			}
		}
		public void AddSkills(SearcherSkillSets skill)//Add skills
		{
			using (DBUtills db = new DBUtills())
			{
				try
				{
					db.SkillDb.Add(skill);
					db.SaveChanges();
				}
				catch (DbEntityValidationException dbEx)
				{
					foreach (var validationErrors in dbEx.EntityValidationErrors)
					{
						foreach (var validationError in validationErrors.ValidationErrors)
						{
							System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
						}
					}
				}

			}
		}
		public bool UpdateSkills(SearcherSkillSets skillSets)  //Update skills
		{
			bool status = false;
			using (DBUtills dBUtills = new DBUtills())
			{
				dBUtills.Entry(skillSets).State = EntityState.Modified;
				dBUtills.SaveChanges();
				status = true;

			}
			return status;
		}
	
		public void AddExperience(WorkExperiences work)//Add work experience
		{
			using (DBUtills dB = new DBUtills())
			{
				dB.WorkExperienceDb.Add(work);
				dB.SaveChanges();
			}
		}
		public IEnumerable<WorkExperiences> FetchWorkExperience(int log)//Extract work experience
		{
			IEnumerable<WorkExperiences> work = null;
			using (DBUtills dB = new DBUtills())
			{
				work = dB.WorkExperienceDb.Where(id => id.AccountId == log).ToList();
				return work;

			}
		}
		public WorkExperiences FetchSingleWorkExperience(int log)//Extract single work experience
		{
			WorkExperiences work = null;
			using (DBUtills dB = new DBUtills())
			{
				work = dB.WorkExperienceDb.Find(log);
				return work;

			}
		}
		public bool UpdateExperience(WorkExperiences experience)  //Update experience
		{
			bool status = false;
			using (DBUtills dBUtills = new DBUtills())
			{
				dBUtills.Entry(experience).State = EntityState.Modified;
				dBUtills.SaveChanges();
				status = true;
			}
			return status;
		}
		public void RemoveExperience(int idValue) //Delete experience
		{
			using (DBUtills dBUtills = new DBUtills())
			{
				WorkExperiences work = dBUtills.WorkExperienceDb.Find(idValue);
				dBUtills.WorkExperienceDb.Remove(work);
				dBUtills.SaveChanges();
			}

		}
		public bool AttachResume(Resume resume)//Attach resume
		{
			bool status = false;
			using (DBUtills db = new DBUtills())
			{
				db.ResumeDb.Add(resume);
				db.SaveChanges();
				status = true;
			}
			return status;
		}

		public Resume DownloadResume(int fileId)//Download file
		{
			Resume resume = null;
			using (DBUtills db = new DBUtills())
			{

				resume = db.ResumeDb.ToList().Find(p => p.ResumeId == fileId);
				return resume;
			}
		}
		public IEnumerable<Resume> FetchFiles(int id)//Fetch File
		{
			IEnumerable<Resume> resume = null;
			using (DBUtills db = new DBUtills())
			{

				resume = db.ResumeDb.ToList().Where(p => p.AccountId == id);
				return resume;
			}
		}
		public void RemoveFile(int idValue)  //Delete files
		{
			using (DBUtills dBUtills = new DBUtills())
			{

				Resume resume = dBUtills.ResumeDb.Find(idValue);
				dBUtills.ResumeDb.Remove(resume);
				dBUtills.SaveChanges();

			}

		}
		/*Admin controls*/
		public IEnumerable<JobTypes> GetJobTypes() //Fetch JobTypes
		{
			IEnumerable<JobTypes> job = null;
			using (DBUtills dB = new DBUtills())
			{
				job = dB.JobTypeDb.ToList();
				return job;
			}
		}		
		public void AddJobTypes(JobTypes job)//Add JobTypes
		{
			using (DBUtills db = new DBUtills())
			{
				db.JobTypeDb.Add(job);
				db.SaveChanges();
			}
		}
		
		public void RemoveJobType(int idValue) //Delete Jobtypes
		{
			using (DBUtills dBUtills = new DBUtills())
			{
				JobTypes job = dBUtills.JobTypeDb.Find(idValue);
				dBUtills.JobTypeDb.Remove(job);
				dBUtills.SaveChanges();
			}

		}
	
		public JobTypes EditJobType(int idValue)  //Fetch particular Jobtypes
		{
			using (DBUtills dBUtills = new DBUtills())
			{
				JobTypes job = null;
				job = dBUtills.JobTypeDb.Find(idValue);
				return job;
			}
		}
		public void UpdateJobType(JobTypes job)  //Update Jobtypes
		{
			using (DBUtills dBUtills = new DBUtills())
			{

				dBUtills.Entry(job).State = EntityState.Modified;
				dBUtills.SaveChanges();

			}
		}
		public void AddCgpa(Cgpas cgpa)//Add Cgpas
		{
			using (DBUtills db = new DBUtills())
			{
				db.CgpaDb.Add(cgpa);
				db.SaveChanges();
			}
		}
		
		public IEnumerable<Cgpas> GetCgpas() //Fetch Cgpas
		{
			IEnumerable<Cgpas> cgpa = null;
			using (DBUtills dB = new DBUtills())
			{
				cgpa = dB.CgpaDb.ToList();
				return cgpa;
			}
		}
		public void RemoveCgpa(int idValue) //Delete Cgpas
		{
			using (DBUtills dBUtills = new DBUtills())
			{
				Cgpas job = dBUtills.CgpaDb.Find(idValue);
				dBUtills.CgpaDb.Remove(job);
				dBUtills.SaveChanges();
			}

		}
		public Cgpas EditCgpa(int idValue)  //Fetch particular Cgpas
		{
			using (DBUtills dBUtills = new DBUtills())
			{
				Cgpas job = null;
				job = dBUtills.CgpaDb.Find(idValue);
				return job;
			}
		}
		public void UpdateCgpa(Cgpas job)  //Update Cgpas
		{
			using (DBUtills dBUtills = new DBUtills())
			{

				dBUtills.Entry(job).State = EntityState.Modified;
				dBUtills.SaveChanges();

			}
		}
		public IEnumerable<Locations> GetLocation() //Fetch Location
		{
			IEnumerable<Locations> loc = null;
			using (DBUtills dB = new DBUtills())
			{
				loc = dB.LocationDb.ToList();
				return loc;
			}
		}
		public void AddLocation(Locations locations)//Add Location
		{
			using (DBUtills db = new DBUtills())
			{
				db.LocationDb.Add(locations);
				db.SaveChanges();
			}
		}
		public void RemoveLocation(int idValue)  //Delete Location
		{
			using (DBUtills dBUtills = new DBUtills())
			{
				Locations loc = dBUtills.LocationDb.Find(idValue);
				dBUtills.LocationDb.Remove(loc);
				dBUtills.SaveChanges();
			}
		}
		public Locations EditLocation(int idValue)  //Fetch particular location
		{
			using (DBUtills dBUtills = new DBUtills())
			{
				Locations loc = null;
				loc = dBUtills.LocationDb.Find(idValue);
				return loc;
			}
		}
		public void UpdateLocation(Locations locations)  //Update Location
		{
			using (DBUtills dBUtills = new DBUtills())
			{
				dBUtills.Entry(locations).State = EntityState.Modified;
				dBUtills.SaveChanges();

			}
		}
		public IEnumerable<RecruiterProfile> FetchProfile()//Fetch profiles
		{
			IEnumerable<RecruiterProfile> profile = null;
			using (DBUtills dB = new DBUtills())
			{

				profile = dB.ProfileDb.Include("Recruiter").ToList();
				// return dB.ProfileDb.SqlQuery("sp_Profile").ToList();
				// return dB.ProfileDb.SqlQuery("sp_ProfileDetails").ToList();

				return profile;

			}
		}
		
	}
}

