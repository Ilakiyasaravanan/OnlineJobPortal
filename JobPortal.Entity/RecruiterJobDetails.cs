using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using JobPortal.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobPortal.Entity
{
	public class RecruiterJobDetails
	{		
		[Key]
		public int RecuiterDetailId { get; set; }
		public int AccountId { get; set; }
		public AccountDetails Account { get; set; }
		[Required]
		[MaxLength(30)]
		public string CompanyName { get; set; }
		public int JobTypeId { get; set; }
		public JobTypes Jobtype { get; set; }
		[Required]
		[Column("Graduation")]
		public Graduation Graduation{ get; set; }		
		public short WorkExperience { get; set; }	
		public string Salary { get; set; }
		public int LocationId { get; set; }

		[Column("Location")]
		public Location Location{ get; set; }

		public int CgpaId { get; set; }
		[Column("CGPA")]
		public Cgpas Cgpa { get; set; }


	}
}
