using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobPortal.Common;
namespace JobPortal.Entity
{
	public class SearcherSkillSets
	{
		[Key]
		public int SkillId { get; set; }
		public AccountDetails searcher { get; set; }
		public int AccountId { get; set; }
		[MaxLength(30)]
		public string SoftwareSkills { get; set; }
		[MaxLength(100)]
		public string Achievements { get; set; }
		[MaxLength(100)]
		public string Certifications { get; set; }
		[Column("Language Known")]
		public LanguageKnown Language { get; set; }
		[MaxLength(50)]
		public string Sports { get; set; }

	}
}
