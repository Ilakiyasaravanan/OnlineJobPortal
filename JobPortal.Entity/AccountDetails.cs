using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using JobPortal.Common;
namespace JobPortal.Entity
{
    public class AccountDetails
    {

		[Required(ErrorMessage = "First Name is required")]
		[DataType(DataType.Text)]
		[Display(Name = "First Name")]
		[StringLength(20, MinimumLength = 5)]
		public string FirstName { get; set; }

		[DataType(DataType.Text)]
		[StringLength(20)]
		[Display(Name = "Last Name")]
		public string LastName { get; set; }

		[Display(Name = "Address")]
		[Required(ErrorMessage = "Address is required")]
		[DataType(DataType.MultilineText)]
		public string Address { get; set; }

		[Display(Name = "Gender")]
		public string Gender { get; set; }

		[Display(Name = "Phone Number")]
		[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone number")]
		public long PhoneNumber { get; set; }

		[Display(Name = "Password")]
		[Required(ErrorMessage = "Password is required")]
		[DataType(DataType.Password)]
		[StringLength(20)]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[StringLength(20)]
		[Compare("Password")]
		[Display(Name = "Confirm Password")]
		public string ConfirmPassword { get; set; }


		[StringLength(10)]
		[Display(Name = "Role")]
		public string Role { get; set; }
		[Display(Name = "Country")]
		public Country country { get; set; }

		[Required(ErrorMessage = "EmailAddress is required")]
		[DataType(DataType.EmailAddress)]
		[StringLength(30)]
		[Display(Name = "Email Address")]
		public string Email { get; set; }

	}
}
