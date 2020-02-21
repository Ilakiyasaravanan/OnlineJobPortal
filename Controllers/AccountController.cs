using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobPortal.Entity;
using JobPortal.BL;
namespace OnlineJobPortal.Controllers
{
    public class AccountController : Controller
    {

		[HttpGet]
		public ActionResult SignUp()
		{
			return View();
		}
		[HttpPost]
		public ActionResult SignUp(AccountDetails account)
		{
			if (ModelState.IsValid)
			{
				bool status = new AccountMediator().AddAccountDetails(account);
				if (status)
				{
					ViewBag.Message = "Successful";
					return RedirectToAction("LogIn");
				}

			}
			return View();
		}
		[HttpGet]
		public ActionResult LogIn()
		{
			return View();
		}
		[HttpPost]
		public ActionResult LogIn(AccountDetails account)
		{
			bool login = new AccountMediator().CheckAccountDetails(account);
			if (login == true)
				return RedirectToAction("Help", "Common");
			else
			{
				Response.Write("LogIn failed");
				return RedirectToAction("LogIn");
			}

		}
	}
}