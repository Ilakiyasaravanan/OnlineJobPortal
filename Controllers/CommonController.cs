using JobPortal.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobPortal.Entity;
using JobPortal.Common;
using OnlineJobPortal.Models;

namespace OnlineJobPortal.Controllers
{
	[ExceptionHandler]
	public class CommonController : Controller
	{
		readonly IJobMediator job;
		public CommonController()//constructor
		{
			job = new JobMediator();
		}
	
		public ActionResult Home()//Home page
		{
			IEnumerable<RecruiterProfile> profiles = job.FetchProfile();
			return View(profiles);
		}
		
		public ActionResult SearchResult()
		{
			return View();
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