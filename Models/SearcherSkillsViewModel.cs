using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JobPortal.Common;
using System.ComponentModel.DataAnnotations;
using JobPortal.Entity;

namespace OnlineJobPortal.Models
{
	public class SearcherSkillsViewModel
	{
		public int SkillId { get; set; }
		public int AccountId { get; set; }
		public AccountDetails searcher { get; set; }
		[Display(Name = "Software Skills")]
		[Required(ErrorMessage = "Specify orelse type nill")]
		public string SoftwareSkills { get; set; }

		[Display(Name = "Achievements")]
		[Required(ErrorMessage = "Specify orelse type nill")]
		public string Achievements { get; set; }
		[Display(Name = "Certifiations")]
		[Required(ErrorMessage = "Specify orelse type nill")]
		public string Certifications { get; set; }
		[Required(ErrorMessage = "Specify it")]
		[Display(Name = "Language Known")]
		public LanguageKnown Language { get; set; }
		[Display(Name = "Sports Events")]
		[Required(ErrorMessage = "Specify orelse type nill")]
		public string Sports { get; set; }


	}
}