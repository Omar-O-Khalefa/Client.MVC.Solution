using System.ComponentModel.DataAnnotations;

namespace Client.PL.ViewModels.Account
{
	public class SignInViewModel
	{
		[Required(ErrorMessage = "Email Is Rquired")]
		[EmailAddress(ErrorMessage = "Invaild Email")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Password Is Required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

        public bool  RemmeberMe { get; set; }
    }
}
