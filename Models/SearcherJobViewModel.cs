using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using JobPortal.Common;
using JobPortal.Entity;

namespace OnlineJobPortal.Models
{
	public class SearcherJobViewModel
	{
		public int SearcherId { get; set; }
		[Display(Name = "Job Type")]
		public int JobTypeId { get; set; }
		public JobTypes Jobtype { get; set; }
		[Display(Name = "Job Type")]
		public string JobTypeName { get; set; }		

		[Display(Name = "Work Experience")]
		public short WorkExperience { get; set; }	
		public int LocationId { get; set; }
		public Locations Location { get; set; }
		[Display(Name = "Location")]
		public string LocationName { get; set; }

		[Display(Name = "Domain")]
		public string Domain { get; set; }
		[Display(Name = "CGPA")]
		public int CgpaId { get; set; }
		public Cgpas Cgpa { get; set; }
		[Display(Name = "Location")]
		public string Cgpas { get; set; }

		[Display(Name = "Your Department")]
		public string Department { get; set; }

		[Display(Name = "Qualification")]
		public Graduation graduation { get; set; }

	}
}
