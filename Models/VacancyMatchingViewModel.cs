using System.Security.Policy;
using System.ComponentModel.DataAnnotations;

namespace OnlineJobPortal.Models
{
	public class VacancyMatchingViewModel
	{
		public int VacancyId { get; set; }
		[RegularExpression("(.*?(\n))", ErrorMessage ="Please fill out here...")]

		[DataType(DataType.MultilineText)]
		public string Message { get; set; }

		[DataType(DataType.Url)]
		public Url Url { get; set; }
		public int Searcher_AccountId { get; set; }
		public ResumeViewModel Resume { get; set; }
		public int ResumeId { get; set; }
		public RecruiterJobViewModel Recruiter { get; set; }
		public int Recruiterid { get; set; }
		public int Recruiter_AccountId { get; set; }
		

	}
}