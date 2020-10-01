using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Entity
{
	public class Cgpas
	{
		[Key]
		public int CgpaId { get; set; }
		[Required]
		[MaxLength(30)]
		public string CGPA { get; set; }
	}
}
