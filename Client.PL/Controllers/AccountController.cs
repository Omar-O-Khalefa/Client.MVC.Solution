using Client.DAL.Models;
using Client.PL.services.EmailSender;
using Client.PL.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using NuGet.Protocol;
using System.Threading.Tasks;

namespace Client.PL.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IEmailSender _emailSender;
		private readonly IConfiguration _configuration;

		public AccountController(
			UserManager<ApplicationUser> userManager
			, SignInManager<ApplicationUser> signInManager
			,IEmailSender emailSender,
			IConfiguration configuration)
        {
			_userManager = userManager;
			_signInManager = signInManager;
			_emailSender = emailSender;
			_configuration = configuration;
		}
		#region Sign UP
        public IActionResult SignUp()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> SignUp(SignUpViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByNameAsync(model.Email);
				if(user is null)
				{

				 user = new ApplicationUser()
				{
					UserName = model.Username,
					Email = model.Email,
					FName =model.FName,
					LName = model.LName,
					IsAgree = model.IsAgree,

				 };

					var Result = await _userManager.CreateAsync(user, model.Password);
					if (Result.Succeeded)
					{
						return RedirectToAction(nameof(SignIn));
					}

				}
				ModelState.AddModelError(string.Empty, "This Email Is Already in Use For Another Account!");

			}
			return View(model);
		}
		#endregion

		#region Sign In
		public IActionResult Signin()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> SignIn(SignInViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				if (user is not null)
				{
					var flag = await _userManager.CheckPasswordAsync(user ,model.Password);
					if (flag) 
					{
						var resalt = await _signInManager.PasswordSignInAsync(user, model.Password, model.RemmeberMe, false);
						if (resalt.IsLockedOut)
						{
							ModelState.AddModelError(string.Empty, "Your Account Is Loced!");
						}

						///if(resalt.IsNotAllowed)
						///{
						///	ModelState.AddModelError(string.Empty, "Your Account is Not Confirmed yet!");
						///}.
			
						if (resalt.Succeeded)
						{
							return RedirectToAction(nameof(HomeController.Index),"Home");
						}
					}
				}
				ModelState.AddModelError(string.Empty, "Invalid Login");
			}
			return View(model);
		}
		#endregion

		#region SignOut
		public async new Task<IActionResult> SignOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction(nameof(Signin));
		}
		#endregion

		#region ForgetPassword

		public IActionResult ForgetPassword()
		{
			return View();
		}
		public async Task<IActionResult> SendResetPasswordEmail(ForgetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				if(user is not null)
				{
					var restPasswordToken =await _userManager.GeneratePasswordResetTokenAsync(user); //UNiQUE Token

					var resetPasswordUrl = Url.Action("ResetPassword", "Account", new {email = user.Email ,token = restPasswordToken},"https:", "localhost:44331");
					//localhost:44331/Account/ResetPassword?email=Omar@gmail.com
					await _emailSender.SendAsync(
						from: _configuration["EmailSettings:SenderEmail"],
						recipents: model.Email,
						subject: "Rest Your Passowrd",
						body: resetPasswordUrl);
					return RedirectToAction(nameof(CheckYourInbox));
				}
				ModelState.AddModelError(string.Empty, "There Is No Account With This Email.");
			}
			return View("ForgetPassword");
		}

		public IActionResult CheckYourInbox()
		{
			return View();
		}
		#endregion

		#region ResetPassword
		[HttpGet]
		public IActionResult ResetPassword( string email , string token)
		{
			TempData["email"] = email;
			TempData["token"] = token;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ResetPassword( ResetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var email = TempData["email"] as string;
				var token = TempData["token"] as string;

				var user = await _userManager.FindByEmailAsync(email);
				
				if( user is not null)
				{
					var rs = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
					return RedirectToAction(nameof(SignIn));
				}
				ModelState.AddModelError(string.Empty, "Url Is Not Valid");
			}
			return View();	
		}
		#endregion
	}
}
