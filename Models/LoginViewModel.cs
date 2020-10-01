using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineJobPortal.Models
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "Email address is required")]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		
		[Required(ErrorMessage = "Password is required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}