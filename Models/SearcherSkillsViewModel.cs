using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JobPortal.Common;
using System.ComponentModel.DataAnnotations;
namespace OnlineJobPortal.Models
{
	public class SearcherSkillsViewModel
	{
		[Display(Name = "Software Skills")]
		[Required(ErrorMessage = "Specify or else type nill")]
		public string SoftwareSkills { get; set; }

		[Display(Name = "Achievements")]
		[Required(ErrorMessage = "Specify or else type nill")]
		public string Achievements { get; set; }
		[Display(Name = "Certifiations")]
		[Required(ErrorMessage = "Specify or else type nill")]
		public string Certifications { get; set; }
		[Required(ErrorMessage = "Specify or else type nill")]
		[Display(Name = "Language Known")]
		public LanguageKnown Language { get; set; }
		[Display(Name = "Sports Events")]
		[Required(ErrorMessage = "Specify or else type nill")]
		public string Sports { get; set; }


	}
}