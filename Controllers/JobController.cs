using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using JobPortal.Entity;
using JobPortal.BL;
using OnlineJobPortal.Models;
using JobPortal.Common;

namespace OnlineJobPortal.Controllers
{
	//[Authorize]
	[ExceptionHandler]
	public class JobController : Controller
	{
		/// <summary>
		/// Here the job related operations are performed like adding informations and attaching resumes by searcher and posting vacancies by searcher
		/// </summary>
		private IJobMediator jobMediator;
		public JobController(IJobMediator jobMediator)
		{
			this.jobMediator = jobMediator;
		}
		/*--------Recruiter---------*/
		/*Display Recruiter page*/
		[Authorize(Roles = "Recruiter")]
		public ActionResult Recruiter()
		{
			return View();
		}
		/* Get-Recruiter job vacancies*/
		[HttpGet]
		[Authorize(Roles = "Admin,Recruiter")]
		public ActionResult RecruiterJobDetails()
		{
			ViewBag.JobTypes = new SelectList(this.jobMediator.GetJobTypes(), "JobTypeId", "JobType");
			ViewBag.Locations = new SelectList(this.jobMediator.GetLocations(), "LocationId", "Location");
			ViewBag.Cgpas = new SelectList(this.jobMediator.GetCgpas(), "CgpaId", "CGPA");
			return View();
		}
		//Post-Processing and storing the vacancy specified by recruiter
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult RecruiterJobDetails(RecruiterJobViewModel recruiterView)
		{
			if (ModelState.IsValid)
			{
				var recruit = AutoMapper.Mapper.Map<RecruiterJobViewModel, RecruiterJobDetails>(recruiterView);
				recruit.AccountId = Convert.ToInt32((int)Session["AccountId"]);
				this.jobMediator.AddJobDetails(recruit);
				return RedirectToAction("DisplayJobVacancy", "Job");
			}
			return View();
		}
		//Display submitted vacancy by recruiter
		[Authorize(Roles = "Recruiter")]
		public ActionResult DisplayJobVacancy()
		{
			IEnumerable<RecruiterJobDetails> recruiterJobs = this.jobMediator.FetchRecruiterJobVacancy((int)Session["AccountId"]);
			ViewData["RecruiterJob"] = recruiterJobs;
			if (recruiterJobs.Count() == 0)
				return RedirectToAction("RecruiterJobDetails", "Job");
			return View();
		}
		//Delete submitted vacancy by recruiter
		[Authorize(Roles = "Recruiter")]
		public ActionResult DeleteVacancy(int id)
		{
			this.jobMediator.RemoveVacancy(id);
			return RedirectToAction("DisplayJobVacancy");
		}
		//Get-Template for recruiter profiles
		[HttpGet]
		[Authorize(Roles = "Recruiter")]
		public ActionResult ProfileDetails()
		{
			RecruiterProfile recruiter = null;
			recruiter = this.jobMediator.CheckProfile((int)Session["AccountId"]);
			if (recruiter != null)
			{
				return RedirectToAction("DisplayProfile");
			}
			return View();
		}
		//Post-Processing profie of recruiter
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult ProfileDetails(RecruiterProfileViewModel job)
		{
			var work = AutoMapper.Mapper.Map<RecruiterProfileViewModel, RecruiterProfile>(job);
			work.AccountId = Convert.ToInt32(Session["AccountId"]);
			this.jobMediator.AddProfile(work);
			return RedirectToAction("DisplayProfile");
		}
		//Get-Editing profile details
		[HttpGet]
		[Authorize(Roles = "Recruiter")]
		public ActionResult EditProfile(int id)
		{
			RecruiterProfile recruiter = this.jobMediator.CheckProfile(id);
			var map = AutoMapper.Mapper.Map<RecruiterProfile, RecruiterProfileViewModel>(recruiter);
			return View(map);
		}
		//Post-updating edited profile
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditProfile(RecruiterProfileViewModel account)
		{
			var accountDetails = AutoMapper.Mapper.Map<RecruiterProfileViewModel, RecruiterProfile>(account);
			bool result = this.jobMediator.UpdateProfile(accountDetails);
			if (result == true)
				return RedirectToAction("DisplayProfile");
			return View();
		}
		//Display profile details of recruiter
		[Authorize(Roles = "Recruiter")]
		public ActionResult DisplayProfile()
		{
			RecruiterProfile account = this.jobMediator.CheckProfile(Convert.ToInt32(Session["AccountId"]));
			if (account != null)
				return View(account);
			else

				return RedirectToAction("ProfileDetails");
		}
		//Display matched candidate details to recruiter
		[Authorize(Roles = "Recruiter")]
		public ActionResult Notification()
		{
			IEnumerable<VacancyMatching> vacancy = this.jobMediator.FetchMatching((int)Session["AccountId"]);
			if (vacancy == null)
				return RedirectToAction("DisplayJobVacancy");
			ViewData["Vacancy"] = vacancy;
			return View();
		}
		//Get- send message to searcher by recruiter
		[Authorize(Roles = "Recruiter")]
		[HttpGet]
		public ActionResult Message_Recruiter(int id)
		{
			VacancyMatching vacancy = this.jobMediator.FetchMatch(id);
			var map = AutoMapper.Mapper.Map<VacancyMatching, VacancyMatchingViewModel>(vacancy);
			if (vacancy == null)
				return RedirectToAction("DisplayJobVacancy");
			return View(map);

		}
		//Post- send message to searcher by recruiter
		[HttpPost]
		public ActionResult Message_Recruiter(VacancyMatchingViewModel vacancyMatching)
		{
			if (ModelState.IsValid)
			{
				var recruit = AutoMapper.Mapper.Map<VacancyMatchingViewModel, VacancyMatching>(vacancyMatching);
				this.jobMediator.UpdateVacancyMatching(recruit);
			}
			return RedirectToAction("DisplayJobVacancy");
		}
		/*--------Searcher---------*/
		/*Display searcher front page*/
		[Authorize(Roles = "Searcher")]
		public ActionResult Searcher()
		{
			return View();
		}
		/*Display applied vacancies by searcher*/
		[Authorize(Roles = "Searcher")]
		public ActionResult DisplayCandidateDetails()
		{
			IEnumerable<SearcherJobDetails> searcherJobs = this.jobMediator.FetchCandidateDetails((int)Session["AccountId"]);
			if (searcherJobs.Count() == 0)
			{
				return RedirectToAction("CandidateJobDetails");
			}
			ViewData["SearcherJob"] = searcherJobs;
			return View();
		}
		//Get-Apply new vacancy by searcher
		[HttpGet]
		[Authorize(Roles = "Admin,Searcher")]
		public ActionResult CandidateJobDetails()
		{
			ViewBag.JobTypes = new SelectList(this.jobMediator.GetJobTypes(), "JobTypeId", "JobType");
			ViewBag.Locations = new SelectList(this.jobMediator.GetLocations(), "LocationId", "Location");
			ViewBag.Cgpas = new SelectList(this.jobMediator.GetCgpas(), "CgpaId", "CGPA");
			return View();
		}
		//Post-Processing and storing the candidate specifications
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult CandidateJobDetails(SearcherJobViewModel searcher)
		{
			var recruit = AutoMapper.Mapper.Map<SearcherJobViewModel, SearcherJobDetails>(searcher);
			if (ModelState.IsValid)
			{
				recruit.AccountId = Convert.ToInt32(Session["AccountId"]);
				this.jobMediator.AddDetails(recruit);
				if (searcher.WorkExperience > 0)
					return RedirectToAction("WorkExperienceDetails");
			}
			ViewData["Recruit"] = recruit;
			return View();
		}
		/*Display matched recruiter vacancies for searcher*/
		[Authorize(Roles = "Searcher")]
		public ActionResult MatchedVacancy()
		{
			IEnumerable<SearcherJobDetails> searcherJobs = this.jobMediator.FetchCandidateDetails((int)Session["AccountId"]);
			if (searcherJobs.Count() == 0)
			{
				return RedirectToAction("CandidateJobDetails");
			}
			IEnumerable<RecruiterJobDetails> Matchedjobs = this.jobMediator.FetchMatchedApplication(searcherJobs);
			if (Matchedjobs.Count() == 0)
				return RedirectToAction("DisplayCandidateDetails");
			else
				ViewData["MatchedJobs"] = Matchedjobs;
			return View();
		}
		/*Display resumes of searcher*/
		[Authorize(Roles = "Searcher")]
		public ActionResult Apply(int id)
		{
			Session["RecruiterId"] = id;
			IEnumerable<Resume> resumes = this.jobMediator.FetchFiles(Convert.ToInt32(Session["AccountId"]));
			return View(resumes);
		}
		/*Add particular resumes for showing to recruiter*/
		[Authorize(Roles = "Searcher")]
		public ActionResult ApplyMessage(int id)
		{
			IEnumerable<VacancyMatching> vacancies = this.jobMediator.FetchMatching((int)Session["AccountId"]);
			VacancyMatching vacancy = new VacancyMatching();
			if (vacancies.Count() == 0)
			{
				vacancy.ResumeId = id;
				vacancy.Searcher_AccountId = (int)Session["AccountId"];
				vacancy.Recruiterid = (int)Session["RecruiterId"];
				vacancy.Recruiter_AccountId = this.jobMediator.FetchRecruiterAccountId((int)Session["RecruiterId"]);
				this.jobMediator.AddMatching(vacancy);
			}
			else
				ViewBag.Message = "Already registered";
			return View();
		}
		/*Display any messages from recruiter*/
		[Authorize(Roles = "Searcher")]
		public ActionResult Message()
		{
			IEnumerable<VacancyMatching> vacancy = this.jobMediator.FetchMatching((int)Session["AccountId"]);
			List<VacancyMatching> vacancyMatchings = null;
			VacancyMatching vacancyMatching;
			foreach (var item in vacancy)
			{
				if (item.Message != null)
				{
					vacancyMatching = new VacancyMatching();
					vacancyMatching.Message = item.Message;
					vacancyMatching.Url = item.Url;
					vacancyMatchings = new List<VacancyMatching>();
					vacancyMatchings.Add(vacancyMatching);
				}
			}
			if (vacancyMatchings != null)
			{
				ViewData["Message"] = vacancyMatchings;
				return View();
			}
			else
				return RedirectToAction("MatchedVacancy");
		}
		//Get-Template for adding searcher skills
		[HttpGet]
		[Authorize(Roles = "Searcher")]
		public ActionResult SearcherSkillDetails()
		{
			int id = Convert.ToInt32(Session["AccountId"]);
			SearcherSkillSets result = this.jobMediator.FetchIndividualSkill(id);
			if (result != null)
			{
				return RedirectToAction("DisplaySkills");
			}
			return View();
		}
		//Post-Processing searcher skills
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult SearcherSkillDetails(SearcherSkillsViewModel job)
		{
			if (ModelState.IsValid)
			{
				var recruit = AutoMapper.Mapper.Map<SearcherSkillsViewModel, SearcherSkillSets>(job);
				recruit.AccountId = Convert.ToInt32(Session["AccountId"]);
				this.jobMediator.AddSearcherSkillSet(recruit);
				return RedirectToAction("DisplaySkills");
			}
			return View();
		}
		/*Display Skills of entered searcher*/
		[Authorize(Roles = "Searcher")]
		public ActionResult DisplaySkills()
		{
			SearcherSkillSets account = this.jobMediator.FetchIndividualSkill(Convert.ToInt32(Session["AccountId"]));
			return View(account);
		}
		[HttpGet]
		[Authorize(Roles = "Searcher")]
		public ActionResult EditSkill(int id) //Get-edit Skills
		{
			SearcherSkillSets skills = this.jobMediator.FetchSkill(id);
			var map = AutoMapper.Mapper.Map<SearcherSkillSets, SearcherSkillsViewModel>(skills);
			return View(map);
		}
		[HttpPost]
		public ActionResult EditSkill(SearcherSkillsViewModel skills) //Post-edit Skills
		{
			var map = AutoMapper.Mapper.Map<SearcherSkillsViewModel, SearcherSkillSets>(skills);
			bool skill = this.jobMediator.EditSkills(map);
			if (skill == true)
				return RedirectToAction("DisplaySkills");
			return RedirectToAction("SearcherSkillDetails");
		}
		/*Uploading resume of searcher*/
		[HttpGet]
		[Authorize(Roles = "Searcher")]
		public ActionResult UploadResume()//Get-Template for adding resume to searcher
		{
			IEnumerable<Resume> resumes = this.jobMediator.FetchFiles((int)Session["AccountId"]);
			return View(resumes);
		}
		[HttpPost]
		public ActionResult UploadResume(HttpPostedFileBase postedFile)//Post-Processing searcher resume
		{
			if (postedFile == null)
			{
				ViewBag.Message = "File not uploaded";
				return View();
			}
			else
			{
				byte[] bytes;
				using (BinaryReader br = new BinaryReader(postedFile.InputStream))
				{
					bytes = br.ReadBytes(postedFile.ContentLength);
					Resume resume = new Resume();
					resume.ContentType = postedFile.ContentType;
					resume.AccountId = Convert.ToInt32(Session["AccountId"]);
					resume.FileName = Path.GetFileName(postedFile.FileName);
					resume.Data = bytes;
					this.jobMediator.AttachResume(resume);
				}
				ViewBag.Message = "File uploaded successfully";
				return View();
			}
		}
		[HttpPost]
		[Authorize(Roles = "Searcher,Recruiter")]
		public FileResult DownloadFile(int? FileId)//Post-processing download resume
		{
			Resume file = this.jobMediator.DownloadResume((int)FileId);
			return File(file.Data, file.ContentType, file.FileName);
		}
		[Authorize(Roles = "Searcher")]
		public ActionResult Delete(int id)//Deleting resume file of searcher
		{
			this.jobMediator.RemoveFile(id);
			return RedirectToAction("UploadResume");
		}
		[HttpGet]
		[Authorize(Roles = "Searcher")]
		public ActionResult WorkExperienceDetails()//Get-Template for adding experience of searcher
		{
			IEnumerable<WorkExperiences> expereience = this.jobMediator.FetchWorkExperience((int)Session["AccountId"]);
			if (expereience.Count() == 0)
				ViewBag.Message = "Your experience is zero, check once";
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult WorkExperienceDetails(WorkExperienceViewModel job)//Post-Processing work experience
		{
			var work = AutoMapper.Mapper.Map<WorkExperienceViewModel, WorkExperiences>(job);
			Object temp = Session["AccountId"];
			work.AccountId = Convert.ToInt32(temp);
			this.jobMediator.CompletedExperience(work);
			return RedirectToAction("DisplayWorkExperience");
		}
		/*Display work experience*/
		[Authorize(Roles = "Searcher")]
		public ActionResult DisplayWorkExperience()
		{
			IEnumerable<WorkExperiences> expereience = this.jobMediator.FetchWorkExperience((int)Session["AccountId"]);
			if (expereience.Count() == 0)
				return RedirectToAction("WorkExperienceDetails", "Job");
			ViewData["Experience"] = expereience;
			return View();
		}
		/*Update work experience*/
		[Authorize(Roles = "Searcher")]
		[HttpGet]
		public ActionResult EditWorkExperience(int id) //Get-Editing work experience details
		{
			WorkExperiences experiences = this.jobMediator.FetchSingleWorkExperience(id);
			var map = AutoMapper.Mapper.Map<WorkExperiences, WorkExperienceViewModel>(experiences);
			return View(map);
		}
		[ExceptionHandler]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditWorkExperience(WorkExperienceViewModel account)//Post-updating edited values
		{
			var exp = AutoMapper.Mapper.Map<WorkExperienceViewModel, WorkExperiences>(account);
			bool result = this.jobMediator.UpdateExperience(exp);
			if (result == true)
				return RedirectToAction("DisplayWorkExperience");
			return View();
		}
		[Authorize(Roles = "Searcher")]
		public ActionResult DeleteExperience(int id)//Deleting experience 
		{
			this.jobMediator.RemoveExperience(id);
			return RedirectToAction("DisplayWorkExperience");
		}
		/*--------Admin---------*/
		/*Display Job types*/
		[Authorize(Roles = "Admin")]
		public ActionResult JobTypes()
		{
			IEnumerable<JobTypes> jobTypes = this.jobMediator.GetJobTypes();
			ViewData["JobTypeDisplay"] = jobTypes;
			return View();
		}
		[HttpGet]
		[Authorize(Roles = "Admin")]
		public ActionResult AddJobTypes()  //Get-Adding Jobtypes
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult AddJobTypes(JobTypeViewModel job)//Post-Adding jobtypes to database
		{
			var jobMapper = AutoMapper.Mapper.Map<JobTypeViewModel, JobTypes>(job);
			this.jobMediator.AddJobTypes(jobMapper);
			return RedirectToAction("JobTypes");
		}
		[HttpGet]
		[Authorize(Roles = "Admin")]
		public ActionResult EditJobType(int id) //Get-Editing JobTypes
		{
			JobTypes account = this.jobMediator.EditJobTypes(id);
			var map = AutoMapper.Mapper.Map<JobTypes, JobTypeViewModel>(account);
			return View(map);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]

		public ActionResult EditJobType(JobTypeViewModel account)//Updating-jobtypes
		{
			var jobDetails = AutoMapper.Mapper.Map<JobTypeViewModel, JobTypes>(account);
			this.jobMediator.UpdateJobTypes(jobDetails);
			return RedirectToAction("JobTypes");
		}
		[Authorize(Roles = "Admin")]
		public ActionResult DeleteJobType(int id)  //Deleting jobtypes
		{
			this.jobMediator.DeleteJobTypes(id);
			return RedirectToAction("JobTypes");
		}
		[Authorize(Roles = "Admin")]
		public ActionResult Location()  //Display Location
		{
			IEnumerable<Locations> jobTypes = this.jobMediator.GetLocations();
			ViewData["LocationDisplay"] = jobTypes;
			return View();
		}
		[HttpGet]
		[Authorize(Roles = "Admin")]
		public ActionResult AddLocation()  //Adding location
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult AddLocation(LocationViewModel job) //Post-Processing and adding location
		{
			var jobMapper = AutoMapper.Mapper.Map<LocationViewModel, Locations>(job);
			this.jobMediator.AddLocation(jobMapper);
			return RedirectToAction("Location");
		}
		[HttpGet]
		[Authorize(Roles = "Admin")]
		public ActionResult EditLocation(int id) //Get-edit location
		{

			Locations account = this.jobMediator.EditLocation(id);
			var map = AutoMapper.Mapper.Map<Locations, LocationViewModel>(account);
			return View(map);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditLocation(LocationViewModel account)//Post-updating location
		{

			var jobDetails = AutoMapper.Mapper.Map<LocationViewModel, Locations>(account);
			this.jobMediator.UpdateLocation(jobDetails);
			return RedirectToAction("Location");
		}
		[Authorize(Roles = "Admin")]
		public ActionResult DeleteLocation(int id) //Deleting location
		{
			this.jobMediator.DeleteLocation(id);
			return RedirectToAction("Location");
		}
		[Authorize(Roles = "Admin")]
		public ActionResult Cgpa()  //Display Cgpa
		{
			IEnumerable<Cgpas> jobTypes = this.jobMediator.GetCgpas();
			ViewData["CgpaDisplay"] = jobTypes;
			return View();
		}
		[HttpGet]
		[Authorize(Roles = "Admin")]
		public ActionResult AddCgpa()  //Add Cgpa
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult AddCgpa(CgpaViewModel job) //Post-Processing and adding Cgpa
		{
			var jobMapper = AutoMapper.Mapper.Map<CgpaViewModel, Cgpas>(job);
			this.jobMediator.AddCgpa(jobMapper);
			return RedirectToAction("Cgpa");
		}
		[HttpGet]
		[Authorize(Roles = "Admin")]
		public ActionResult EditCgpa(int id) //Get-edit cgpas
		{
			Cgpas account = this.jobMediator.EditCgpa(id);
			var map = AutoMapper.Mapper.Map<Cgpas, CgpaViewModel>(account);
			return View(map);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditCgpa(CgpaViewModel account)//Post-updating cgpas
		{
			var jobDetails = AutoMapper.Mapper.Map<CgpaViewModel, Cgpas>(account);
			this.jobMediator.UpdateCgpa(jobDetails);
			return RedirectToAction("Cgpa");
		}
		[Authorize(Roles = "Admin")]
		public ActionResult DeleteCgpa(int id) //Deleting location
		{
			this.jobMediator.DeleteCgpa(id);
			return RedirectToAction("Cgpa");
		}

		[Authorize(Roles = "Admin")]
		public ActionResult RecruiterVacancyDetails()//Display all recruiter details
		{
			IEnumerable<RecruiterJobDetails> recruiterjob = this.jobMediator.GetRecruiterRequirements();
			ViewData["RecruiterJobVacany"] = recruiterjob;
			return View();
		}
		[Authorize(Roles = "Admin")]
		public ActionResult SearcherJobDetails() //Display all searcher details
		{
			IEnumerable<SearcherJobDetails> recruiterjob = this.jobMediator.GetSearcherrApplications();
			ViewData["SearcherJobVacany"] = recruiterjob;
			return View();
		}

	}
}
