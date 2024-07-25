using System.ComponentModel.DataAnnotations;

namespace PersentationLayer.ModelsView
{
	public class ResetPasswordVM
	{
		[Required(ErrorMessage = "Confirm Password Is Required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Required(ErrorMessage = "Confirm Password Is Required")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Confirm Password doesn't Match")]
		public string ConfirmPassword { get; set; }
	}
}
