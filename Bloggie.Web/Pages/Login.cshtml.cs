using Bloggie.Web.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.Web.Pages
{
	public class LoginModel : PageModel
	{
		private readonly SignInManager<IdentityUser> signInManager;

		public LoginModel(SignInManager<IdentityUser> signInManager)
		{
			this.signInManager = signInManager;
		}
		[BindProperty]
		public Login LoginViewModel { get; set; }
		public void OnGet()
		{

		}

		public async Task<IActionResult> OnPost(string? ReturnUrl)
		{
			if (ModelState.IsValid)
			{
				var signInResult = await signInManager.PasswordSignInAsync(LoginViewModel.Username, LoginViewModel.Password, false, false);
				if (signInResult.Succeeded)
				{
					if (!string.IsNullOrEmpty(ReturnUrl))
					{
						return RedirectToPage(ReturnUrl);
					}
					return RedirectToPage("/Index");
				}
				else
				{
					ViewData["Notification"] = new Notification
					{
						Type = Enum.NotificationType.Error,
						Message = "Invalid username or password"
					};
					return Page();
				}
			}
			return Page();
		}
	}
}
