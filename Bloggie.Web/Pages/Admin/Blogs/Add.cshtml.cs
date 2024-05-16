using System.ComponentModel.DataAnnotations;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModel;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Bloggie.Web.Pages.Admin.Blogs
{
	[Authorize(Roles = "Admin")]
	public class AddModel : PageModel
	{
		private readonly IBlogPostRepository blogPostRepository;

		[BindProperty]
		public AddBlogPost AddBlogPostRequest { get; set; }
		[BindProperty]
		public IFormFile FeaturedImage { get; set; }
		[BindProperty]
		[Required]
		public string Tags { get; set; }

		public AddModel(IBlogPostRepository blogPostRepository)
		{
			this.blogPostRepository = blogPostRepository;
		}

		public void OnGet()
		{
		}

		public async Task<IActionResult> OnPost()
		{
			ValidateBlogPost();
			if (ModelState.IsValid)
			{
				var blogPost = new BlogPost()
				{
					Heading = this.AddBlogPostRequest.Heading,
					PageTitle = this.AddBlogPostRequest.PageTitle,
					Content = this.AddBlogPostRequest.Content,
					ShortDescription = this.AddBlogPostRequest.ShortDescription,
					FeaturedImageUrl = this.AddBlogPostRequest.FeaturedImageUrl,
					UrlHandle = this.AddBlogPostRequest.UrlHandle,
					PublishedDate = this.AddBlogPostRequest.PublishedDate,
					Author = this.AddBlogPostRequest.Author,
					Visible = this.AddBlogPostRequest.Visible,
					Tags = new List<Tag>(Tags.Split(',').Select(x => new Tag()
					{
						Name = x.Trim()
					}))
				};
				await blogPostRepository.AddAsync(blogPost);

				var notification = new Notification
				{
					Type = Enum.NotificationType.Success,
					Message = "New blog created successfully."
				};

				TempData["Notification"] = JsonSerializer.Serialize(notification);

				return RedirectToPage("/Admin/Blogs/List");
			}
			return Page();
		}

		private void ValidateBlogPost()
		{
			if (AddBlogPostRequest.PublishedDate.Date < DateTime.Now.Date)
			{
				ModelState.AddModelError("AddBlogPostRequest.PublishedDate", $"PublishedDate can only be today's date of a future date");
			}
		}
	}
}
