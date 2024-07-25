using System.ComponentModel.DataAnnotations;

namespace PersentationLayer.ModelsView
{
	public class ForgetPasswordVM
	{
		[Required(ErrorMessage = "Email Is Required")]
		[EmailAddress(ErrorMessage = "This Email Not Valid")]
		public string Email { get; set; }
	}
}
