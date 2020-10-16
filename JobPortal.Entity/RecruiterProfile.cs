using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Entity
{
	public class RecruiterProfile
	{
		[Key]

		public int ProfileId { get; set; }
		[MaxLength(20)]
		public string WorkingCompany { get; set; }
		[MaxLength(20)]
		public string WorkingLocation { get; set; }
		[MaxLength(20)]
		public string Position { get; set; }
		[MaxLength(30)]
		public string CompanyDescription { get; set; }

		public int AccountId { get; set; }

		public AccountDetails Recruiter { get; set; }

	}
}
