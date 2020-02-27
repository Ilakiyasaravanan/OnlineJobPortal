using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using JobPortal.Common;
namespace JobPortal.Entity
{
	public class JobDetails
	{
		[Required(ErrorMessage = "Company Name is required")]		
		[Display(Name = "Company Name")]
		[StringLength(50, MinimumLength = 5)]
		public string CompanyName { get; set; }	
	
		[Display(Name = "Job Type")]
		public JobType jobType { get; set; }

		[Display(Name = "Graduation")]

		public Graduation graduation{ get; set; }

		[Display(Name = "Work Experience")]
		public short WorkExperience { get; set; }

		[Display(Name = "Salary")]
		
		public long Salary { get; set; }

		[Display(Name = "Location")]
	
		public Location location{ get; set; }

	}
}
