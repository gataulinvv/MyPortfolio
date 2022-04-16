using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Apps.MVCApp.Models;
using Apps.MVCApp.ViewModels;
using System.Threading.Tasks;

namespace Apps.MVCApp.Controllers
{	
	public class AccountController : Controller
	{
		private SignInManager<AppUser> _signInManager { get; set; }
		public AccountController(SignInManager<AppUser> signInManager)
		{
			_signInManager = signInManager;
		}

		[HttpGet]
		public IActionResult AccessDenied()
		{
			var result = new JsonResultModel()
			{
				IsOk = false,
				URL = "*",
				ErrMessage = "Доступ запрещен!"
			};

			return new ObjectResult(result);
		}

		[HttpGet]
		public IActionResult Login(string returnUrl)
		{
			return View(new LoginViewModel { ReturnUrl = returnUrl });
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
				if (result.Succeeded)
				{
					var succsess = new JsonResultModel()
					{
						IsOk = result.Succeeded,
						URL = model.ReturnUrl,
						ErrMessage = "Успешный вход"
					};

					return new ObjectResult(succsess);
				}
				else
				{
					var error = new JsonResultModel()
					{
						IsOk = result.Succeeded,
						URL = model.ReturnUrl,
						ErrMessage = "Неправильный логин и (или) пароль!"
					};

					return new ObjectResult(error);
				}
			}
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
	}
}
