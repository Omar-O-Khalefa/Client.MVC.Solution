using System.ComponentModel.DataAnnotations;

namespace Client.PL.ViewModels.Account
{
	public class ForgetPasswordViewModel
	{
		[Required(ErrorMessage = "Email Is Rquired")]
		[EmailAddress(ErrorMessage = "Invaild Email")]
		public string Email { get; set; }
	}
}
