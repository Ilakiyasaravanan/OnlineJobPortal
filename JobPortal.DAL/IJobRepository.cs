using System.Collections.Generic;
using JobPortal.Entity;
namespace JobPortal.DAL
{
	public interface IJobRepository
	{
		void AddRecruiterDetails(RecruiterJobDetails job);
		void AddCandidateDetails(SearcherJobDetails job);
		IEnumerable<JobTypes> GetJobTypes();
		void AddJobTypes(JobTypes job);
		JobTypes EditJobType(int id);
		void RemoveJobType(int id);
		void UpdateJobType(JobTypes account);
		IEnumerable<Cgpas> GetCgpas();
		void AddCgpa(Cgpas job);
		Cgpas EditCgpa(int id);
		void RemoveCgpa(int id);
		void UpdateCgpa(Cgpas account);
		IEnumerable<Locations> GetLocation();
		void AddLocation(Locations locations);
		Locations EditLocation(int id);
		void RemoveLocation(int id);
		void UpdateLocation(Locations account);
		void AddExperience(WorkExperiences work);
		void AddSkills(SearcherSkillSets skill);
		void AddProfile(RecruiterProfile profile);
		RecruiterProfile FetchProfile(int log);
		bool UpdateProfile(RecruiterProfile profile);
		IEnumerable<RecruiterJobDetails> FetchRecruiterVacancies();
		IEnumerable<SearcherJobDetails> FetchSearcherApplications();

		bool CheckSkillExists(int log);
		SearcherSkillSets FetchIndividualSkill(int log);
		IEnumerable<RecruiterJobDetails> FetchRecruiterDetails(int recruiterId);
		
		IEnumerable<SearcherJobDetails> FetchCandidate(int id);
		bool AttachResume(Resume resume);
		Resume DownloadResume(int fileId);
		void RemoveVacancy(int idValue);
		IEnumerable<WorkExperiences> FetchWorkExperience(int id);
		bool UpdateExperience(WorkExperiences experience);
		WorkExperiences FetchSingleWorkExperience(int log);
		void RemoveExperience(int idValue);
		IEnumerable<RecruiterProfile> FetchProfile();
	}
}
