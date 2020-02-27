using System.ComponentModel.DataAnnotations;
using JobPortal.Common;
namespace JobPortal.Entity
{
	public class AccountDetails
	{
		[Key]
		public int AccountId { get; set; }	
		public string FirstName { get; set; }
		public string LastName { get; set; }	
		public string Address { get; set; }		
		public string Gender { get; set; }
		public long PhoneNumber { get; set; }
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }		   	
		public string Role { get; set; }		
		public Country country { get; set; }
		public string Email { get; set; }

	}
}
