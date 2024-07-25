using System.ComponentModel.DataAnnotations;

namespace PersentationLayer.ModelsView
{
	public class RegisterVM
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		[Required(ErrorMessage = "Email Is Required")]
		[EmailAddress(ErrorMessage ="This Email Not Valid")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Email Is Required")]
		[EmailAddress(ErrorMessage ="This Email Not Valid")]

		public string Password { get; set; }
		[Required(ErrorMessage = "Confirm Password Is Required")]
		[DataType(DataType.Password)]
		[Compare("Password" , ErrorMessage = "Confirm Password doesn't Match")]
		public string ConfirmPassword { get; set; }

		public bool IsAgree { get; set; }
	}
}
