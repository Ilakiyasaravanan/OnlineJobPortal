using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobPortal.Entity;
using JobPortal.BL;
namespace OnlineJobPortal.Controllers
{
    public class JobController : Controller
    {
        [HttpGet]
        public ActionResult RecruiterJobSpecification()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RecruiterJobSpecification(JobDetails job)
        {
            if (ModelState.IsValid)
            {

                bool status = new JobMediator().AddJobDetails(job);
                if (status)
                {
                    ViewBag.Message = "Successful";
                    return RedirectToAction("Help","Common");
                }

            }
            return View();
        }
        [HttpGet]
        public ActionResult CandidateDetails()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CandidateDetails(JobDetails job)
        {
            if (ModelState.IsValid)
            {
                bool status = new JobMediator().AddDetails(job);
                if (status)
                {
                    ViewBag.Message = "Successful";
                    return RedirectToAction("Help", "Common");
                }

            }
            return View();
        }
    }
}