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
		IJobMediator jobMediator;

		//Paramterless constructor
		public JobController()
		{
			jobMediator = new JobMediator();
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
			ViewBag.JobTypes = new SelectList(jobMediator.GetJobTypes(), "JobTypeId", "JobType");
			ViewBag.Locations = new SelectList(jobMediator.GetLocations(), "LocationId", "Location");
			ViewBag.Cgpas = new SelectList(jobMediator.GetCgpas(), "CgpaId", "CGPA");
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
				object temp = Session["AccountId"].ToString();
				recruit.AccountId = Convert.ToInt32(temp);
				new JobMediator().AddJobDetails(recruit);
				return RedirectToAction("DisplayJobVacancy", "Job");
			}
			return View();
		}

		[Authorize(Roles = "Recruiter")]
		public ActionResult DisplayJobVacancy()//Get-Delete submitted vacancy
		{
			IEnumerable<RecruiterJobDetails> recruiterJobs = jobMediator.FetchRecruiterJobVacancy((int)Session["AccountId"]);
			ViewData["RecruiterJob"] = recruiterJobs;
			if (recruiterJobs.Count() == 0)
				return RedirectToAction("RecruiterJobDetails", "Job");
			return View();

		}
		[Authorize(Roles = "Recruiter")]
		public ActionResult DeleteVacancy(int id)//Post-Delete submitted vacancy
		{
			jobMediator.RemoveVacancy(id);
			return RedirectToAction("DisplayJobVacancy");
		}

		[HttpGet]
		[Authorize(Roles = "Recruiter")]
		public ActionResult ProfileDetails()//Get-Template for recruiter profiles
		{
			Object temp = Session["AccountId"];
			int id = Convert.ToInt32(temp);
			RecruiterProfile recruiter = null;
			recruiter = jobMediator.CheckProfile(id);
			if (recruiter != null)
			{
				return RedirectToAction("DisplayProfile");
			}
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult ProfileDetails(RecruiterProfileViewModel job)//Post-Processing profie of recruiter
		{
			var work = AutoMapper.Mapper.Map<RecruiterProfileViewModel, RecruiterProfile>(job);
			Object temp = Session["AccountId"];
			work.AccountId = Convert.ToInt32(temp);
			jobMediator.AddProfile(work);
			return RedirectToAction("DisplayProfile");
		}

		[HttpGet]
		[Authorize(Roles = "Recruiter")]
		public ActionResult EditProfile(int id) //Get-Editing account details
		{
			RecruiterProfile recruiter = jobMediator.CheckProfile(id);
			var map = AutoMapper.Mapper.Map<RecruiterProfile, RecruiterProfileViewModel>(recruiter);
			return View(map);

		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditProfile(RecruiterProfileViewModel account)//Post-updating edited values
		{
			var accountDetails = AutoMapper.Mapper.Map<RecruiterProfileViewModel, RecruiterProfile>(account);
			bool result = jobMediator.UpdateProfile(accountDetails);
			if (result == true)
				return RedirectToAction("DisplayProfile");
			return View();
		}
		[Authorize(Roles = "Recruiter")]
		public ActionResult DisplayProfile() //Get-Editing profile details
		{
			int temp = Convert.ToInt32(Session["AccountId"].ToString());
			RecruiterProfile account = jobMediator.CheckProfile(temp);
			if (account != null)
				return View(account);
			else

				return RedirectToAction("ProfileDetails");
		}

		/*--------Searcher---------*/
		/*Display Searcher page*/
		[Authorize(Roles = "Searcher")]
		public ActionResult Searcher()//Front view of searcher
		{
			return View();
		}
		/*Display applied vacancies*/
		[Authorize(Roles = "Searcher")]
		public ActionResult DisplayCandidateDetails()
		{
			IEnumerable<SearcherJobDetails> searcherJobs = jobMediator.FetchCandidateDetails((int)Session["AccountId"]);
			if (searcherJobs.Count() == 0)
			{
				return RedirectToAction("CandidateJobDetails");
			}
			ViewData["SearcherJob"] = searcherJobs;
			return View();
		}

		/*Apply new vacancy*/
		[HttpGet]
		[Authorize(Roles = "Admin,Searcher")]
		public ActionResult CandidateJobDetails()  //Get-Apply new vacancy
		{
			ViewBag.JobTypes = new SelectList(jobMediator.GetJobTypes(), "JobTypeId", "JobType");
			ViewBag.Locations = new SelectList(jobMediator.GetLocations(), "LocationId", "Location");
			ViewBag.Cgpas = new SelectList(jobMediator.GetCgpas(), "CgpaId", "CGPA");
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult CandidateJobDetails(SearcherJobViewModel searcher)//Post-Processing and storing the candidate specifications
		{			
			var recruit = AutoMapper.Mapper.Map<SearcherJobViewModel, SearcherJobDetails>(searcher);
			if (ModelState.IsValid)
			{
				Object temp = Session["AccountId"];
				recruit.AccountId = Convert.ToInt32(temp);			
				jobMediator.AddDetails(recruit);
				if (searcher.WorkExperience > 0)
				{
					return RedirectToAction("WorkExperienceDetails");
				}
				
			}
			ViewData["Recruit"] = recruit;
			return View();
		}
		[HttpGet]
		/*Display matched recruiter vacancies*/
		[Authorize(Roles = "Searcher")]
		public ActionResult MatchedVacancy()
		{
			IEnumerable<SearcherJobDetails> searcherJobs = jobMediator.FetchCandidateDetails((int)Session["AccountId"]);
			if (searcherJobs.Count() == 0)
			{
				return RedirectToAction("CandidateJobDetails");
			}
			IEnumerable<RecruiterJobDetails> Matchedjobs = jobMediator.FetchMatchedApplication(searcherJobs);
			if (Matchedjobs.Count()==0)
				return RedirectToAction("DisplayCandidateDetails");
			else
				ViewData["MatchedJobs"] = Matchedjobs;				
			return View();
		}
		//[HttpPost]
		///*Display matched recruiter vacancies*/
		//[Authorize(Roles = "Searcher")]
		//public ActionResult MatchedVacancy(int id)
		//{
			
		//	return View();
		//}
		public ActionResult Apply()
		{

			return View();
		}
		/*Adding Searcher skill details*/
		[HttpGet]
		[Authorize(Roles = "Searcher")]
		public ActionResult SearcherSkillDetails()//Get-Template for adding searcher skills
		{
			Object temp = Session["AccountId"];
			int id = Convert.ToInt32(temp);
			SearcherSkillSets result = jobMediator.FetchIndividualSkill(id);
			if (result !=null)
			{
				return RedirectToAction("DisplaySkills");
			}
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult SearcherSkillDetails(SearcherSkillsViewModel job)//Post-Processing searcher skills
		{
			if (ModelState.IsValid)
			{
				var recruit = AutoMapper.Mapper.Map<SearcherSkillsViewModel, SearcherSkillSets>(job);
				Object temp = Session["AccountId"];
				recruit.AccountId = Convert.ToInt32(temp);
				jobMediator.AddSearcherSkillSet(recruit);
				return RedirectToAction("DisplaySkills");
			}
			return View();
		}

		/*Display Skills of entered searcher*/
		[Authorize(Roles = "Searcher")]
		public ActionResult DisplaySkills()
		{
			int temp = Convert.ToInt32(Session["AccountId"].ToString());
			SearcherSkillSets account = jobMediator.FetchIndividualSkill(temp);

			return View(account);

		}
		[HttpGet]
		[Authorize(Roles = "Searcher")]
		public ActionResult EditSkill(int id) //Get-edit Skills
		{
			SearcherSkillSets skills = jobMediator.FetchSkill(id);
			var map = AutoMapper.Mapper.Map<SearcherSkillSets, SearcherSkillsViewModel>(skills);
			return View(map);
		}
		[HttpPost]
		public ActionResult EditSkill(SearcherSkillsViewModel skills) //Post-edit Skills
		{
			var map = AutoMapper.Mapper.Map<SearcherSkillsViewModel, SearcherSkillSets>(skills);
			bool skill = jobMediator.EditSkills(map);
			if (skill == true)

				return RedirectToAction("DisplaySkills");

			return RedirectToAction("SearcherSkillDetails");
		}
		/*Uploading resume of searcher*/
		[HttpGet]
		[Authorize(Roles = "Searcher")]
		public ActionResult UploadResume()//Get-Template for adding resume to searcher
		{
			Object temp = Session["AccountId"];
			int id = Convert.ToInt32(temp);
			IEnumerable<Resume> resumes = jobMediator.FetchFiles(id);
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
					jobMediator.AttachResume(resume);
				}
				ViewBag.Message = "File uploaded successfully";
				return View();
			}
		}
		[HttpPost]
		[Authorize(Roles = "Searcher")]
		public FileResult DownloadFile(int? FileId)//Post-processing download resume
		{
			Resume file = jobMediator.DownloadResume((int)FileId);
			return File(file.Data, file.ContentType, file.FileName);
		}
		[Authorize(Roles = "Searcher")]
		public ActionResult Delete(int id)//Deleting resume file of searcher
		{
			jobMediator.RemoveFile(id);
			return RedirectToAction("UploadResume");
		}
		[HttpGet]
		[Authorize(Roles = "Searcher")]
		public ActionResult WorkExperienceDetails()//Get-Template for adding experience of searcher
		{

			IEnumerable<WorkExperiences> expereience = jobMediator.FetchWorkExperience((int)Session["AccountId"]);
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
			jobMediator.CompletedExperience(work);
			return RedirectToAction("DisplayWorkExperience");
		}
		/*Display work experience*/
		[Authorize(Roles = "Searcher")]
		public ActionResult DisplayWorkExperience()
		{
			IEnumerable<WorkExperiences> expereience = jobMediator.FetchWorkExperience((int)Session["AccountId"]);
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
			WorkExperiences experiences = jobMediator.FetchSingleWorkExperience(id);
			var map = AutoMapper.Mapper.Map<WorkExperiences, WorkExperienceViewModel>(experiences);
			return View(map);
		}
		[ExceptionHandler]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditWorkExperience(WorkExperienceViewModel account)//Post-updating edited values
		{

			var exp = AutoMapper.Mapper.Map<WorkExperienceViewModel, WorkExperiences>(account);
			bool result = jobMediator.UpdateExperience(exp);
			if (result == true)
				return RedirectToAction("DisplayWorkExperience");
			return View();
		}
		[Authorize(Roles = "Searcher")]
		public ActionResult DeleteExperience(int id)//Deleting experience 
		{
			jobMediator.RemoveExperience(id);
			return RedirectToAction("DisplayWorkExperience");
		}
		/*--------Admin---------*/
		/*Display Job types*/
		[Authorize(Roles = "Admin")]
		public ActionResult JobTypes()
		{
			IEnumerable<JobTypes> jobTypes = jobMediator.GetJobTypes();
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
			jobMediator.AddJobTypes(jobMapper);
			return RedirectToAction("JobTypes");
		}
		[HttpGet]
		[Authorize(Roles = "Admin")]
		public ActionResult EditJobType(int id) //Get-Editing JobTypes
		{
			JobTypes account = jobMediator.EditJobTypes(id);
			var map = AutoMapper.Mapper.Map<JobTypes, JobTypeViewModel>(account);
			return View(map);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]

		public ActionResult EditJobType(JobTypeViewModel account)//Updating-jobtypes
		{
			var jobDetails = AutoMapper.Mapper.Map<JobTypeViewModel, JobTypes>(account);
			jobMediator.UpdateJobTypes(jobDetails);
			return RedirectToAction("JobTypes");
		}
		[Authorize(Roles = "Admin")]
		public ActionResult DeleteJobType(int id)  //Deleting jobtypes
		{
			jobMediator.DeleteJobTypes(id);
			return RedirectToAction("JobTypes");
		}
		[Authorize(Roles = "Admin")]
		public ActionResult Location()  //Display Location
		{
			IEnumerable<Locations> jobTypes = jobMediator.GetLocations();
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
			jobMediator.AddLocation(jobMapper);
			return RedirectToAction("Location");
		}
		[HttpGet]
		[Authorize(Roles = "Admin")]
		public ActionResult EditLocation(int id) //Get-edit location
		{

			Locations account = jobMediator.EditLocation(id);
			var map = AutoMapper.Mapper.Map<Locations, LocationViewModel>(account);
			return View(map);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditLocation(LocationViewModel account)//Post-updating location
		{

			var jobDetails = AutoMapper.Mapper.Map<LocationViewModel, Locations>(account);
			jobMediator.UpdateLocation(jobDetails);
			return RedirectToAction("Location");
		}
		[Authorize(Roles = "Admin")]
		public ActionResult DeleteLocation(int id) //Deleting location
		{
			jobMediator.DeleteLocation(id);
			return RedirectToAction("Location");
		}
		[Authorize(Roles = "Admin")]
		public ActionResult Cgpa()  //Display Cgpa
		{
			IEnumerable<Cgpas> jobTypes = jobMediator.GetCgpas();
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
			jobMediator.AddCgpa(jobMapper);
			return RedirectToAction("Cgpa");
		}
		[HttpGet]
		[Authorize(Roles = "Admin")]
		public ActionResult EditCgpa(int id) //Get-edit cgpas
		{

			Cgpas account = jobMediator.EditCgpa(id);
			var map = AutoMapper.Mapper.Map<Cgpas, CgpaViewModel>(account);
			return View(map);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditCgpa(CgpaViewModel account)//Post-updating cgpas
		{

			var jobDetails = AutoMapper.Mapper.Map<CgpaViewModel, Cgpas>(account);
			jobMediator.UpdateCgpa(jobDetails);
			return RedirectToAction("Cgpa");
		}
		[Authorize(Roles = "Admin")]
		public ActionResult DeleteCgpa(int id) //Deleting location
		{
			jobMediator.DeleteCgpa(id);
			return RedirectToAction("Cgpa");
		}

		[Authorize(Roles = "Admin")]
		public ActionResult RecruiterVacancyDetails()//Display all recruiter details
		{
			IEnumerable<RecruiterJobDetails> recruiterjob = jobMediator.GetRecruiterRequirements();
			ViewData["RecruiterJobVacany"] = recruiterjob;
			return View();
		}
		[Authorize(Roles = "Admin")]
		public ActionResult SearcherJobDetails() //Display all searcher details
		{
			IEnumerable<SearcherJobDetails> recruiterjob = jobMediator.GetSearcherrApplications();
			ViewData["SearcherJobVacany"] = recruiterjob;
			return View();
		}

	}
}
