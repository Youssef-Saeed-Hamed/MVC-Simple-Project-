using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PersentationLayer.ModelsView;
using PersentationLayer.Utilities;

namespace PersentationLayer.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<AppUser> userManager;
		private readonly SignInManager<AppUser> signInManager;

		public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
		}

		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Register(RegisterVM model)
		{
			if(!ModelState.IsValid) 
				return View(model);
			var user = new AppUser
			{
				FirstName = model.FirstName,
				LastName = model.LastName,
				Email = model.Email,
				IsAgree = model.IsAgree,
				UserName = model.FirstName + model.LastName,
			};

			var result = await userManager.CreateAsync(user, model.Password);
			if(result.Succeeded)
				return RedirectToAction(nameof(Login));
			

			foreach(var error in result.Errors)
				ModelState.AddModelError("", error.Description);
		
			return View(model);
		}

		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]

		public async Task<IActionResult> Login(SignInVM model) 
		{ 
			if(!ModelState.IsValid) return View(model);
			var user = await userManager.FindByEmailAsync(model.Email);
			if(user is not null)
			{
				if(await userManager.CheckPasswordAsync(user, model.Password))
				{
					var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
					if (result.Succeeded)
						return RedirectToAction("Index", "Home");
				}
			}
			ModelState.AddModelError(  "" , "Incorrect Email Or Password");
			return View();
		}

		public new async Task<IActionResult> SignOut()
		{
			await signInManager.SignOutAsync();
			return RedirectToAction(nameof(Login));
		}

		public IActionResult ForgetPassword()
		{
			return View();
		}

		[HttpPost]
		public async Task <IActionResult> ForgetPassword(ForgetPasswordVM model)
		{
			if (!ModelState.IsValid)
				return View(model);

			var user = await userManager.FindByEmailAsync(model.Email);
			if (user is not null)
			{
				var token = await userManager.GeneratePasswordResetTokenAsync(user);
				var url = Url.Action("ResetPassword", "Account",
					new { email = model.Email, token = token } , Request.Scheme);
				var email = new Email
				{
					Subject = "Reset Password",
					Body = url,
					Recepient = model.Email
				};
				EmailSetting.SendEmail(email);
				return RedirectToAction(nameof(CheckYourInBox));
			}
			ModelState.AddModelError("", "Email Doesn't Exists");
			

			return View();
		}

		public IActionResult CheckYourInBox()
		{
			return View();
		}

		public IActionResult ResetPassword(string email , string token)
		{
			TempData["email"] = email;
			TempData["token"] = token;
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
		{
			if (!ModelState.IsValid) return View();
			var email = TempData["email"] as string;
			var token = TempData["token"] as string;

			var user = await userManager.FindByEmailAsync(email);
			if (user is not null)
			{
				var result = await userManager.ResetPasswordAsync(user,token , model.Password);
				if (result.Succeeded)				
					return RedirectToAction(nameof(Login));
				foreach (var error in result.Errors)
					ModelState.AddModelError("",error.Description);                                  
            }
			return View(model);
		}

	}
}
