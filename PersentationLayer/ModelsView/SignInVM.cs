using System.ComponentModel.DataAnnotations;

namespace PersentationLayer.ModelsView
{
	public class SignInVM
	{
		[Required(ErrorMessage = "Email Is Required")]
		[EmailAddress(ErrorMessage = "This Email Not Valid")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Email Is Required")]
		[EmailAddress(ErrorMessage = "This Email Not Valid")]
		public string Password { get; set; }
		public bool RememberMe { get; set; }

	}
}
