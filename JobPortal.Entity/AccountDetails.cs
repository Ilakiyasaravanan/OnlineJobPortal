using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobPortal.Entity
{
	public class AccountDetails
	{
		[Key]
		public int AccountId { get; set; }
		[Required]
		[MaxLength(30)]
		public string FirstName { get; set; }

		[MaxLength(30)]
		public string LastName { get; set; }

		[Required]
		[MaxLength(50)]
		public string Address { get; set; }
		[Required]
		[MaxLength(6)]
		public string Gender { get; set; }
		[Required]
		public long PhoneNumber { get; set; }

		[Required]
		[MaxLength(64)]
		public string Password { get; set; }
		[Required]
		[MaxLength(10)]
		public string Role { get; set; }
		[Required]
		[MaxLength(25)]
		[Index(IsUnique = true)]
		public string Email { get; set; }

		[Column("DateofBirth")]
		[Required(ErrorMessage = "DoB is required")]

		public DateTime? DateOfBirth { get; set; }
	}
	//[Required]
	//public int CountryId { get; set; }
	//public Country Country { get; set; }
	//public string CountryName { get; set; }
}
