using System.Collections.Generic;
using JobPortal.Entity;
namespace JobPortal.BL
{
	public interface IJobMediator
	{
		void AddJobDetails(RecruiterJobDetails account);
		void AddDetails(SearcherJobDetails account);
		IEnumerable<JobTypes> GetJobTypes();
		void AddJobTypes(JobTypes job);
		JobTypes EditJobTypes(int id);
		void DeleteJobTypes(int id);
		void UpdateJobTypes(JobTypes account);
		IEnumerable<Cgpas> GetCgpas();
		void AddCgpa(Cgpas job);
		Cgpas EditCgpa(int id);
		void DeleteCgpa(int id);
		void UpdateCgpa(Cgpas account);
		IEnumerable<Locations> GetLocations();
		void AddLocation(Locations job);
		Locations EditLocation(int id);
		void DeleteLocation(int id);
		void UpdateLocation(Locations account);
		void CompletedExperience(WorkExperiences work);
		void AddProfile(RecruiterProfile profile);
		void AddSearcherSkillSet(SearcherSkillSets work);
		RecruiterProfile CheckProfile(int id);

		IEnumerable<RecruiterJobDetails> GetRecruiterRequirements();
		IEnumerable<SearcherJobDetails> GetSearcherrApplications();
		void RemoveVacancy(int id);
		bool UpdateProfile(RecruiterProfile recruiter);

		bool EditSkills(SearcherSkillSets id);
		SearcherSkillSets FetchSkill(int id);

		SearcherSkillSets FetchIndividualSkill(int log);

		IEnumerable<RecruiterJobDetails> FetchRecruiterJobVacancy(int recruiterId);
		IEnumerable<SearcherJobDetails> FetchCandidateDetails(int recruiterId);

		bool AttachResume(Resume resume);
		Resume DownloadResume(int fileId);
		IEnumerable<Resume> FetchFiles(int id);
		void RemoveFile(int idValue);
		IEnumerable<WorkExperiences> FetchWorkExperience(int id);
		bool UpdateExperience(WorkExperiences experience);
		WorkExperiences FetchSingleWorkExperience(int log);
		void RemoveExperience(int idValue);
		IEnumerable<RecruiterProfile> FetchProfile();
		IEnumerable<RecruiterJobDetails> FetchMatchedApplication(SearcherJobDetails job);
	}
}
