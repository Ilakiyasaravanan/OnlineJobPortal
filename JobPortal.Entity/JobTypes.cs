using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Entity
{
	public class JobTypes
	{
		[Key]
		public int JobTypeId { get; set; }
		public string JobType { get; set; }
	}
}
