using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModel;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Bloggie.Web.Pages.Admin.Blogs
{
	[Authorize(Roles = "Admin")]
	public class ListModel : PageModel
	{
		private readonly IBlogPostRepository blogPostRepository;

		public List<BlogPost> BlogPosts { get; set; }

		public ListModel(IBlogPostRepository blogPostRepository)
		{
			this.blogPostRepository = blogPostRepository;
		}
		public async Task OnGet()
		{
			var notificationJson = (string)TempData["Notification"];
			if (notificationJson != null)
			{
				ViewData["Notification"] = JsonSerializer.Deserialize<Notification>(notificationJson.ToString());
			}
			BlogPosts = (await blogPostRepository.GetAllAsync()).ToList();
		}
	}
}
