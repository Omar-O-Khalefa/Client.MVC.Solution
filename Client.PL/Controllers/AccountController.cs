using Client.DAL.Models;
using Client.PL.ViewModels.User;
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
        public IActionResult SingUp()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> SingUp(SignUpViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByNameAsync(model.Username);
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
				ModelState.AddModelError(string.Empty, "This User Name Is Already in Use For Another Account!");

			}
			return View(model);
		}
		#endregion

		#region Sign In

		//
		#endregion
	}
}
