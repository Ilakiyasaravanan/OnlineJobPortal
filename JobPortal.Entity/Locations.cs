using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Entity
{
	public class Locations
	{
		[Key]
		public int LocationId { get; set; }
		[Required]
		[MaxLength(20)]
		public string Location { get; set; }
	}
}
