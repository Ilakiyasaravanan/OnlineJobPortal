using System.Collections.Generic;
using JobPortal.DAL;
using JobPortal.Entity;
namespace JobPortal.BL
{
	public class JobMediator : IJobMediator
	{
		IJobRepository jobRepository;
		public JobMediator()//Parameterless contructor
		{
			jobRepository = new JobRepository();
		}
		public void AddJobDetails(RecruiterJobDetails account) //Add recruiter details
		{
			jobRepository.AddRecruiterDetails(account);
			
		}
		public void AddDetails(SearcherJobDetails account) //Add searcher details
		{
			jobRepository.AddCandidateDetails(account);
			
		}
		public IEnumerable<JobTypes> GetJobTypes() //Get jobtypes
		{
			return jobRepository.GetJobTypes();
		}
		
		public void AddJobTypes(JobTypes job) //Add new jobtype
		{
			jobRepository.AddJobTypes(job);
		}
		public JobTypes EditJobTypes(int id)  //Edit details
		{
			return jobRepository.EditJobType(id);
		}
		public void DeleteJobTypes(int id)  //Delete details
		{
			jobRepository.RemoveJobType(id);
		}
		public void UpdateJobTypes(JobTypes account) //Update details
		{
			jobRepository.UpdateJobType(account);
		}
		public IEnumerable<Cgpas> GetCgpas() //Get Cgpa
		{
			return jobRepository.GetCgpas();
		}
		public void AddCgpa(Cgpas job) //Add new jobtype
		{
			jobRepository.AddCgpa(job);
		}
		public Cgpas EditCgpa(int id)  //Edit details
		{
			return jobRepository.EditCgpa(id);
		}
		public void DeleteCgpa(int id)  //Delete details
		{
			jobRepository.RemoveCgpa(id);
		}
		public void UpdateCgpa(Cgpas account) //Update details
		{
			jobRepository.UpdateCgpa(account);
		}
		public IEnumerable<Locations> GetLocations() //Display jobtypes
		{
			return jobRepository.GetLocation();
		}
		public void AddLocation(Locations location) //Add new jobtype
		{
			jobRepository.AddLocation(location);
		}
		public Locations EditLocation(int id)  //Edit location
		{
			return jobRepository.EditLocation(id);
		}
		public void DeleteLocation(int id)  //Delete location
		{
			jobRepository.RemoveLocation(id);
		}
		public void UpdateLocation(Locations location) //Update location
		{
			jobRepository.UpdateLocation(location);
		}
		public void CompletedExperience(WorkExperiences work)//Completed searcher experience
		{
			jobRepository.AddExperience(work);
		}
		
		public void AddSearcherSkillSet(SearcherSkillSets work)//Adding Searcher Skillset
		{
			jobRepository.AddSkills(work);
		}
		
		public bool EditSkills(SearcherSkillSets id)//Update edited skills
		{
			return jobRepository.UpdateSkills(id);
		}
		public SearcherSkillSets FetchSkill(int id)//Fetch skill using skillid
		{
			return jobRepository.FetchSkill(id);
		}
		public SearcherSkillSets FetchIndividualSkill(int id)//Get profile details of particular one using accountid
		{
			return jobRepository.FetchIndividualSkill(id);
		}

		public void AddProfile(RecruiterProfile profile)//Adding Recruiter profile
		{
			jobRepository.AddProfile(profile);
		}
		public RecruiterProfile CheckProfile(int id)//Fetch profile
		{
			return jobRepository.FetchProfile(id);
		}
		public bool UpdateProfile(RecruiterProfile recruiter)//Update profiles
		{
		     return jobRepository.UpdateProfile(recruiter);
		}
		public void RemoveVacancy(int id)//Remove vacancy of recruiter
		{
		 jobRepository.RemoveVacancy(id);
		}
		public IEnumerable<RecruiterJobDetails> GetRecruiterRequirements()//fetch all recruiter details
		{
			return jobRepository.FetchRecruiterVacancies();
		}
		public IEnumerable<SearcherJobDetails> GetSearcherrApplications()//fetch all searcher details
		{
			return jobRepository.FetchSearcherApplications();
		}
		
		
		public IEnumerable<RecruiterJobDetails> FetchRecruiterJobVacancy(int recruiterId)//Fetch recruiter applied vacancy
		{
			return jobRepository.FetchRecruiterDetails(recruiterId);

		}
		public IEnumerable<SearcherJobDetails> FetchCandidateDetails(int recruiterId)//Fetch candidate applied vacancy
		{
			return jobRepository.FetchCandidate(recruiterId);

		}
        public bool AttachResume(Resume resume)//Add resume
		{
			return jobRepository.AttachResume(resume);
		}
		public Resume DownloadResume(int fileId)//Download file
		{
			return jobRepository.DownloadResume(fileId);
		}
		public IEnumerable<Resume> FetchFiles(int id)
		{
			return jobRepository.FetchFiles(id);
		}
		public void RemoveFile(int idValue)
		{
			jobRepository.RemoveFile(idValue);
		}
		public IEnumerable<WorkExperiences> FetchWorkExperience(int id)//Fetch whole work experience of searcher
		{
			return jobRepository.FetchWorkExperience(id);
		}
		public bool UpdateExperience(WorkExperiences experience)//Update work experience of searcher
		{
			return jobRepository.UpdateExperience(experience);
		}
		public WorkExperiences FetchSingleWorkExperience(int log)//Fetch individual work experience of searcher
		{
			return jobRepository.FetchSingleWorkExperience(log);
		}
		public void RemoveExperience(int idValue)//Remove experience of searcher
		{
			 jobRepository.RemoveExperience(idValue);
		}
		public IEnumerable<RecruiterProfile> FetchProfile()//Fetch whole profile of recruiter
		{
			return jobRepository.FetchProfile();
		}
		public IEnumerable<RecruiterJobDetails> FetchMatchedApplication(IEnumerable<SearcherJobDetails> job)
		{
			return jobRepository.FetchMatchedApplication(job);
		}
	}
}
