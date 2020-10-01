using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Entity
{
	public class WorkExperiences
	{
		[Key]
		public int WorkExperienceId { get; set; }
		public AccountDetails searcher { get; set; }
		public int AccountId { get; set; }
		[MaxLength(30)]
		public string WorkCompanyName { get; set; }

		public short CompletedExpereince { get; set; }
	}

}
