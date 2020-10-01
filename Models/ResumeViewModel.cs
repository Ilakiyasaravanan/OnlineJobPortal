using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineJobPortal.Models
{
	public class ResumeViewModel
	{	
		public int ResumeId { get; set; }

		[Display(Name = "Resume Name")]
		public string FileName { get; set; }
		[Display(Name ="Resume Type")]
		public string ContentType { get; set; }
		[Display(Name ="Resume Content")]
		public byte[] Data { get; set; }
	}
}