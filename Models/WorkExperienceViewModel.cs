using JobPortal.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineJobPortal.Models
{
	public class WorkExperienceViewModel { 
		public AccountDetails searcher { get; set; }
	public int AccountId { get; set; }
	public int WorkExperienceId { get; set; }
	
		[Display(Name = "Previous Company Name:")]

		public string WorkCompanyName { get; set; }

		[Display(Name = "Experience:")]

		public short CompletedExpereince { get; set; }
	}
}