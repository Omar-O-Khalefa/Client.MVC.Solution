using Client.DAL.Models;
using Client.PL.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Client.PL.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
			_userManager = userManager;
			_signInManager = signInManager;
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
	}
}
