using System;
using System.ComponentModel.DataAnnotations;
using JobPortal.Common;
using JobPortal.Entity;
namespace OnlineJobPortal.Models
{
	public class AccountViewModel
	{
		public int AccountId { get; set; }

		[Required(ErrorMessage = "First name is required")]
		[RegularExpression("[A-Z][a-z][a-z][a-zA-Z]*",ErrorMessage="Invalid Name/First letter should be capital")]
		[StringLength(20)]
		[Display(Name = "First Name")]
		
		public string FirstName { get; set; }

	
		[StringLength(20)]
		[RegularExpression("[a-zA-Z]*", ErrorMessage = "Invalid Name")]
		[Display(Name = "Last Name")]
		public string LastName { get; set; }

		[Display(Name = "Address")]
		[Required(ErrorMessage = "Address is required")]
		[DataType(DataType.MultilineText)]
		public string Address { get; set; }

		[Required(ErrorMessage = "Select gender")]
		[Display(Name = "Gender")]
		public string Gender { get; set; }

		[Display(Name = "Phone Number")]
		[Required(ErrorMessage = "Phone number field is required")]
		[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone number")]
		public long PhoneNumber { get; set; }

		[RegularExpression("^.*(?=.{6,})(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$", ErrorMessage = "Must have 1 Uppercase,1 Lowercase,1 special,Minimum 6 characters")]
		[Required(ErrorMessage = "Password is required")]
		[DataType(DataType.Password)]
		
		public string Password { get; set; }

		[DataType(DataType.Password)]
		
		[Compare("Password")]
		[Display(Name = "Confirm password")]
		public string ConfirmPassword { get; set; }

		[Display(Name = "Role")]
		[Required(ErrorMessage = "Select role")]
		public string Role { get; set; }

	

		[Required(ErrorMessage = "Email address is required")]
		[DataType(DataType.EmailAddress)]		
		public string Email { get; set; }


		[Display(Name = "Date of Birth")]		
		[Required]
		public DateTime? DateofBirth { get; set; }


	}
}
