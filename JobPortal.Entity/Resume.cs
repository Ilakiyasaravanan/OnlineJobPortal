using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Entity
{
	public class Resume
	{
		[Key]
		public int ResumeId { get; set; }
		public AccountDetails Searcher { get; set; }
		public int AccountId { get; set; }
		public string FileName { get; set; }
		public string ContentType { get; set; }
		public byte[] Data { get; set; }
	}
}
