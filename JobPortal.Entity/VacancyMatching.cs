using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Entity
{
	public class VacancyMatching
	{
		
			[Key]
			public int VacancyId { get; set; }
			public int Searcher_AccountId { get; set; }
			public Resume Resume { get; set; }
			public int ResumeId { get; set; }
			public RecruiterJobDetails Recruiter { get; set; }
			public int Recruiterid { get; set; }
			public int Recruiter_AccountId { get; set; }
			[MaxLength(200)]
			public string Message { get; set; }
			
			[MaxLength(200)]
			public string Url { get; set; }
		}
	}

