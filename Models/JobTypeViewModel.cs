using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineJobPortal.Models
{
	public class JobTypeViewModel
	{
		public int JobTypeId { get; set; }

		[Required(ErrorMessage = "Specify JobType")]
		public string JobType { get; set; }
	}
}