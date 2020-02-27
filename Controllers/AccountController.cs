using System.Collections.Generic;
using System.Web.Mvc;
using OnlineJobPortal.Models;
using JobPortal.BL;
using JobPortal.Entity;
using ServiceStack.Auth;
using AutoMapper;
using System;

namespace OnlineJobPortal.Controllers
{
	[HandleError]

	public class AccountController : Controller
	{

		[HttpGet]
		public ActionResult SignUp()
		{
			return View();
		}
		[HttpPost]
		
		public ActionResult SignUp(AccountViewModel account)
		{
			try
			{
				AccountDetails accountDetails = new AccountDetails();
				if (ModelState.IsValid)
				{		

					accountDetails.AccountId = account.AccountId;
					accountDetails.FirstName = account.FirstName;
					accountDetails.LastName = account.LastName;
					accountDetails.Address = account.Address;
					accountDetails.PhoneNumber = account.PhoneNumber;
					accountDetails.Password = account.Password;
					accountDetails.ConfirmPassword = account.ConfirmPassword;
					accountDetails.Gender = account.Gender;
					accountDetails.Role = account.Role;
					accountDetails.country = account.country;
					accountDetails.Email = account.Email;
					new AccountMediator().AddAccountDetails(accountDetails);
					//AccountDetails accountDetails; //;= new AccountDetails();
					//accountDetails = (AccountDetails)account;
					//AuthenticateService authenticateService = new AuthenticateService();
					//var result = authenticateService.AccountDetails(account);
					//var config = new MapperConfiguration(cfg =>
					//{
					//	cfg.CreateMap<AccountValidation, AccountDetails>();
					//});
					//Mapper.CreateMap<AccountValidation, AccountDetails>();  //creating map  
					//AccountDetails accountDetails = new AccountDetails();
					//var accountDetails = Mapper.Map<AccountValidation, AccountDetails>(account);


					//new AccountMediator().AddAccountDetails(result);
					return RedirectToAction("LogIn");
				}

			}
			catch
			{
				throw new Exception("ErrorMessage");
			}

			return View();
		}
		[HttpGet]
		public ActionResult LogIn()
		{
			return View();
		}
		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult LogIn(AccountDetails account)
		{
			string login = new AccountMediator().CheckAccountDetails(account);
			if (login.Equals("Recruiter"))
				return RedirectToAction("RecruiterJobSpecification", "Job");
			else if (login.Equals("Searcher"))
				return RedirectToAction("CandidateDetails", "Job");
			else
			{
				throw new Exception("ErrorMessage");
				//Response.Write("LogIn failed");
				//return RedirectToAction("LogIn");
			}
			
		}
		public ActionResult Display()
		{
			IEnumerable<AccountDetails> account = new AccountMediator().View();
			ViewData["AccountDetails"] = account;
			return View();
		}
	}
}