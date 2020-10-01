using System;
using JobPortal.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JobPortal.Entity
{
	public class SearcherJobDetails
	{
		[Key]
		public int SearcherDetailId { get; set; }
		public int AccountId { get; set; }
		public AccountDetails account { get; set; }

		[Required]
		public int JobTypeId { get; set; }
		public JobTypes Jobtype { get; set; }
		[Required]
		[Column("Graduation")]
		public Graduation graduation { get; set; }
		public short WorkExperience { get; set; }

		public int LocationId { get; set; }
		public Location location { get; set; }

		[Required]
		[MaxLength(5)]
		public string Domain { get; set; }
		[Column("CGPA")]
		public float Cgpa { get; set; }
		[MaxLength(20)]
		public string Department { get; set; }

		
	}
}
