using JobPortal.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineJobPortal.Models
{
	public class RecruiterProfileViewModel
	{
		public int ProfileId { get; set; }
		public int AccountId { get; set; }

		public AccountDetails searcher { get; set; }

		//[RegularExpression("[A-Z][a-zA-Z]*", ErrorMessage = "Invalid Company Name")]
		[Display(Name = "Current Company")]

		public string WorkingCompany { get; set; }

		[RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Invalid Location")]
		[Display(Name = "Current Working Location")]

		public string WorkingLocation { get; set; }

		[RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Invalid Position")]
		[Display(Name = "Current Position")]

		public string Position { get; set; }
		[Display(Name = "About your company")]
	
		public string CompanyDescription { get; set; }
	}
}