using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineJobPortal.Models
{
	public class CgpaViewModel
	{
		public int CgpaId { get; set; }
		[Required(ErrorMessage ="It is needed")]
		[MaxLength(30)]
		public string CGPA { get; set; }
	}
}