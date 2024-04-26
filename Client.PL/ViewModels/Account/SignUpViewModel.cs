using System.ComponentModel.DataAnnotations;

namespace Client.PL.ViewModels.Account
{
	public class SignUpViewModel
	{
        [Required(ErrorMessage = "Username Is Rquired")]
        public string Username { get; set; }


        [Required(ErrorMessage="Email Is Rquired")]
		[EmailAddress(ErrorMessage ="Invaild Email")]
		public string Email { get; set; }


		[Required(ErrorMessage = "FName Is Rquired")]
		[Display(Name = "First Name")]
		public string FName { get; set; }

		[Required(ErrorMessage = "Last Is Rquired")]
		[Display(Name = "Last Name")]
		public string LName { get; set; }


        [Required(ErrorMessage ="Password Is Required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }


		[Required(ErrorMessage = "Email Is Rquired")]
		[DataType(DataType.Password)]
		[Compare(nameof(Password),ErrorMessage ="Password Dosent Match")]
		public string ConfirmPassword { get; set; }

		public bool IsAgree { get; set; }
	}
}
