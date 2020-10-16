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
		readonly IAccountMediator accountMediator;

		/*Parameterless constructor*/
		public AccountController()
		{
			accountMediator = new AccountMediator();
		}

		/* Sign up Page*/
		[HttpGet]
		public ActionResult SignUp() //Get-Register new account
		{
			return View();
		}
		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult SignUp(AccountViewModel account) //Post-Process account details
		{

			if (ModelState.IsValid)
			{
				var sign = AutoMapper.Mapper.Map<AccountViewModel, AccountDetails>(account);
				bool result = accountMediator.CheckAccount(sign.Email);
				if (result == true)
				{
					ViewData["UserExists"] = "Email already exists";
					return View();
				}
				else
				{
					int status = accountMediator.AddAccountDetails(sign);
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
		[HttpGet]
		//	[AllowAnonymous]
		public ActionResult LogIn() //Get-Login 
		{
			return View();
		}
		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult LogIn(LoginViewModel account)//Post-Login
		{
			var loginDetails = AutoMapper.Mapper.Map<LoginViewModel, AccountDetails>(account);
			AccountDetails accountDetails = accountMediator.CheckAccountDetails(loginDetails);
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

		[Authorize(Roles = "Admin")]
		public ActionResult Display()//Display all accountdetails for admin
		{
			IEnumerable<AccountDetails> account = accountMediator.View();
			ViewData["AccountDetails"] = account;
			return View();
		}
		[Authorize(Roles = "Recruiter,Searcher")]
		public ActionResult DisplayDetail()//Display their account details
		{
			Object account = null;
			if (Session["AccountId"] != null)
			{
				account = Session["AccountId"];
			}

			int tempId = Convert.ToInt32(account);
			AccountDetails accountDetails = accountMediator.ParticularDetails(tempId);
			return View(accountDetails);
		}

		[HttpGet]
		[Authorize(Roles = "Admin,Recruiter,Searcher")]
		public ActionResult Edit(int id) //Get-Editing account details
		{

			AccountDetails account = accountMediator.Edit(id);
			var map = AutoMapper.Mapper.Map<AccountDetails, AccountViewModel>(account);
			return View(map);
		}
		[ExceptionHandler]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(AccountViewModel account)//Post-updating edited values
		{

			var accountDetails = AutoMapper.Mapper.Map<AccountViewModel, AccountDetails>(account);
			int result = accountMediator.Update(accountDetails);
			if (result == 1)
				return RedirectToAction("DisplayDetail");


			//ViewBag.Country = new SelectList(accountMediator.GetCountry(), "CountryId", "CountryName");
			return View();
		}
		[Authorize(Roles = "Admin")]
		public ActionResult Delete(int id)//Deleting record of account
		{
			accountMediator.Delete(id);
			return RedirectToAction("Display");
		}

		[Authorize(Roles = "Admin")]
		public ActionResult AdminDisplay()//Displaying particular link controls
		{
			return View();
		}

		public ActionResult LogOff()//Logout method
		{
			//AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
			Session.Clear();
			FormsAuthentication.SignOut();
			return RedirectToAction("Home", "Common");
		}
	}
}