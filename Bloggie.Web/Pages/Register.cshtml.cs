using Bloggie.Web.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.Web.Pages
{
	public class RegisterModel : PageModel
	{
		private readonly UserManager<IdentityUser> userManager;

		public RegisterModel(UserManager<IdentityUser> userManager)
		{
			this.userManager = userManager;
		}
		[BindProperty]
		public Register RegisterViewModel { get; set; }
		public void OnGet()
		{

		}

		public async Task<IActionResult> OnPost()
		{
			if (ModelState.IsValid)
			{
				var user = new IdentityUser
				{
					UserName = RegisterViewModel.Username,
					Email = RegisterViewModel.Email
				};

				var identityResult = await userManager.CreateAsync(user, RegisterViewModel.Password);

				if (identityResult.Succeeded)
				{
					var addRoleResult = await userManager.AddToRoleAsync(user, "User");
					if (addRoleResult.Succeeded)
					{
						ViewData["Notification"] = new Notification()
						{
							Type = Enum.NotificationType.Success,
							Message = "User registered successfully"
						};
					}
					return Page();
				}
				else
				{
					ViewData["Notification"] = new Notification()
					{
						Type = Enum.NotificationType.Error,
						Message = "Something went wrong"
					};

					return Page();
				}
			}
			else
			{
				return Page();
			}
		}
	}
}
