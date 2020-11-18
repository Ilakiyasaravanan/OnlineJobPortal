using System.Collections.Generic;
using System.Web.Mvc;
using OnlineJobPortal.Models;
using JobPortal.BL;
using JobPortal.Entity;
using System;
using System.Web.Security;
using System.Web;
using JobPortal.Common;

namespace OnlineJobPortal.Controllers
{
	[ExceptionHandler]
	public class AccountController : Controller
	{
		/// <summary>
		/// Here the account creation, updation, deletion and view the account details takesplace
		/// </summary>
		private IAccountMediator accountMediator;
		public AccountController(IAccountMediator accountMediator)
		{
			this.accountMediator = accountMediator;
		}
		/* Sign up Page*/
		//Get-Register new account
		[HttpGet]
		public ActionResult SignUp() 
		{
			return View();
		}
		//Post-Process account details
		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult SignUp(AccountViewModel account) 
		{
			if (ModelState.IsValid)
			{
				var sign = AutoMapper.Mapper.Map<AccountViewModel, AccountDetails>(account);
				bool result = this.accountMediator.CheckAccount(sign.Email);
				if (result == true)
				{
					ViewData["UserExists"] = "Email already exists";
					return View();
				}
				else
				{
					int status = this.accountMediator.AddAccountDetails(sign);
					if (status == sign.AccountId)
					{
						FormsAuthentication.SetAuthCookie(sign.Email, false);
						var authTicket = new FormsAuthenticationTicket(1, sign.Email, DateTime.Now, DateTime.Now.AddMinutes(20), false, sign.Role);
						string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
						var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
						HttpContext.Response.Cookies.Add(authCookie);
						Session["AccountId"] = status;
						if (sign.Role.Equals("Recruiter"))
							return RedirectToAction("Recruiter", "Job");
						else if (sign.Role.Equals("Searcher"))
							return RedirectToAction("Searcher", "Job");
					}
					else
						return RedirectToAction("LogIn");
				}
			}
			return View();
		}
		/* Login page*/
		//Get-Login 
		[HttpGet]
		//	[AllowAnonymous]
		public ActionResult LogIn() 
		{
			return View();
		}
		//Post-Login
		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult LogIn(LoginViewModel account)
		{
			var loginDetails = AutoMapper.Mapper.Map<LoginViewModel, AccountDetails>(account);
			AccountDetails accountDetails = this.accountMediator.CheckAccountDetails(loginDetails);
			if (ModelState.IsValid)
			{
				if (accountDetails != null)
				{
					FormsAuthentication.SetAuthCookie(accountDetails.Email, false);
					var authTicket = new FormsAuthenticationTicket(1, accountDetails.Email, DateTime.Now, DateTime.Now.AddMinutes(20), false, accountDetails.Role);
					string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
					var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
					HttpContext.Response.Cookies.Add(authCookie);
					Session["AccountId"] = accountDetails.AccountId;
					if (accountDetails.Role.Equals("Recruiter"))
						return RedirectToAction("Recruiter", "Job");

					else if (accountDetails.Role.Equals("Searcher"))
						return RedirectToAction("Searcher", "Job");

					else if (accountDetails.Role.Equals("Admin"))
						return RedirectToAction("AdminDisplay");
				}
				else
				{
					TempData["User_Exists"] = "Invalid login attempt.";
					//ModelState.AddModelError("", "Invalid login attempt.");
					return View();
				}
			}
			else
			{
				TempData["User_Exists"] = "Invalid username or password";
				return View();
			}
			return View();
		}
		//Display all accountdetails for admin
		[Authorize(Roles = "Admin")]
		public ActionResult Display()
		{
			IEnumerable<AccountDetails> account = this.accountMediator.View();
			ViewData["AccountDetails"] = account;
			return View();
		}
		//Display their account details
		[Authorize(Roles = "Recruiter,Searcher")]
		public ActionResult DisplayDetail()
		{
			AccountDetails accountDetails = this.accountMediator.ParticularDetails((int)Session["AccountId"]);
			return View(accountDetails);
		}
		//Get-Editing account details
		[HttpGet]
		[Authorize(Roles = "Admin,Recruiter,Searcher")]
		public ActionResult Edit(int id) 
		{
			AccountDetails account = this.accountMediator.Edit(id);
			var map = AutoMapper.Mapper.Map<AccountDetails, AccountViewModel>(account);
			return View(map);
		}
		//Post-updating edited values
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(AccountViewModel account)
		{
			var accountDetails = AutoMapper.Mapper.Map<AccountViewModel, AccountDetails>(account);
			int result = this.accountMediator.Update(accountDetails);
			if (result == 1)
				return RedirectToAction("DisplayDetail");
			return View();
		}
		//Deleting record of account
		[Authorize(Roles = "Admin")]
		public ActionResult Delete(int id)
		{
			this.accountMediator.Delete(id);
			return RedirectToAction("Display");
		}
		//Displaying particular link controls
		[Authorize(Roles = "Admin")]
		public ActionResult AdminDisplay()
		{
			return View();
		}

		//Logout view
		public ActionResult LogOff()
		{
			//AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
			Session.Clear();
			FormsAuthentication.SignOut();
			return RedirectToAction("Home", "Common");
		}
	}
}