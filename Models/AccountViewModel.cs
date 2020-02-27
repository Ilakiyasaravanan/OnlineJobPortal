using System.ComponentModel.DataAnnotations;
using JobPortal.Common;
using JobPortal.Entity;
namespace OnlineJobPortal.Models
{
	public class AccountViewModel
	{
		public int AccountId { get; set; }

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

		[Required(ErrorMessage = "Select option")]
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



		[Display(Name = "Role")]
		[Required(ErrorMessage = "Select option")]
		public string Role { get; set; }
		[Display(Name = "Country")]
		public Country country { get; set; }

		[Required(ErrorMessage = "EmailAddress is required")]

		[DataType(DataType.EmailAddress)]
		[StringLength(30)]
		[Display(Name = "Email Address")]
		public string Email { get; set; }

		//public static implicit operator AccountViewModel(AccountDetails account)
		//{
		//	return new AccountViewModel
		//	{
		//		AccountId = account.AccountId,
		//		FirstName = account.FirstName,
		//		LastName = account.LastName,
		//		Address = account.Address,
		//		PhoneNumber = account.PhoneNumber,
		//		Password = account.Password,
		//		ConfirmPassword = account.ConfirmPassword,
		//		Gender = account.Gender,
		//		Role = account.Role,
		//		country = account.country,
		//		Email = account.Email
		//	};
		//}
		//public static implicit operator AccountDetails(AccountViewModel validation)
		//{
		//	return new AccountDetails
		//	{
		//		AccountId = validation.AccountId,
		//		FirstName = validation.FirstName,
		//		LastName = validation.LastName,
		//		Address = validation.Address,
		//		PhoneNumber = validation.PhoneNumber,
		//		Password =validation.Password,
		//		ConfirmPassword = validation.ConfirmPassword,
		//		Gender = validation.Gender,
		//		Role = validation.Role,
		//		country = validation.country,
		//		Email =validation.Email
		//	};

		//}

	}
}
