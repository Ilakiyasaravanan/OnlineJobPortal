using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using JobPortal.Common;
using JobPortal.Entity;
namespace OnlineJobPortal.Models
{
	public class RecruiterJobViewModel
	{
		[Required(ErrorMessage = "Company Name is required")]
		[Display(Name = "Company Name")]
		public string CompanyName { get; set; }		
		public int JobTypeId { get; set; }
		public JobTypes Jobtype { get; set; }

		[Display(Name = "Job Type")]
		public string JobTypeName { get; set; }		

		[Display(Name = "Qualification")]
		public Graduation Graduation { get; set; }

		[Display(Name = "Work Experience")]
		public short WorkExperience { get; set; }

		[Display(Name = "Salary")]

		public string Salary { get; set; }

		[Display(Name = "Location")]
		
		public int LocationId { get; set; }
		public Locations Location { get; set; }
		public string LocationName { get; set; }
		public int CgpaId { get; set; }		
		public Cgpas Cgpa { get; set; }
		[Display(Name = "CGPA")]
		public string CgpaName { get; set; }



	}
}

