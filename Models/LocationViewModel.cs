using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineJobPortal.Models
{
	public class LocationViewModel
	{
		public int LocationId { get; set; }

		[Required(ErrorMessage = "Specify JobType")]
		public string Location { get; set; }
	}
}