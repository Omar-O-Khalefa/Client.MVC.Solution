using System.ComponentModel.DataAnnotations;

namespace Client.PL.ViewModels.Account
{
	public class ResetPasswordViewModel
	{
		[Required(ErrorMessage = "Password Is Required")]
		[MinLength(5,ErrorMessage ="Minimum password length Is 5")]
		[DataType(DataType.Password)]
		public string NewPassword { get; set; }


		[Required(ErrorMessage = "Email Is Rquired")]
		[DataType(DataType.Password)]
		[Compare(nameof(NewPassword), ErrorMessage = "Password Dosent Match")]
		public string ConfirmPassword { get; set; }

    
    }
}
