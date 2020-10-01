using JobPortal.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobPortal.Entity;
using JobPortal.Common;

namespace OnlineJobPortal.Controllers
{
    [ExceptionHandler]
    public class CommonController : Controller
    {
        IJobMediator job;
        public CommonController()
        {
            job = new JobMediator();
        }
       
        public ActionResult Home()//Home page
        {
            //IEnumerable<RecruiterProfile> profiles= job.FecthRecruiterProfile();  
            return View();
            //return View(profiles);
        }
        public ActionResult Help()//Help page
        {
            return View();
        }
        public ActionResult Contact()//Contact page
        {
            return View();
        }
    }
}