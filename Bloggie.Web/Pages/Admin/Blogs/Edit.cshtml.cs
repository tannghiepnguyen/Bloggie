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
	public class EditModel : PageModel
	{
		private readonly IBlogPostRepository blogPostRepository;

		[BindProperty]
		public EditBlogPostRequest BlogPost { get; set; }
		[BindProperty]
		public IFormFile FeaturedImage { get; set; }
		[BindProperty]
		public string Tags { get; set; }

		public EditModel(IBlogPostRepository blogPostRepository)
		{
			this.blogPostRepository = blogPostRepository;
		}
		public async Task OnGet(Guid id)
		{
			var blogPostDomainModel = await blogPostRepository.GetAsync(id);
			if (blogPostDomainModel != null && blogPostDomainModel.Tags != null)
			{
				BlogPost = new EditBlogPostRequest()
				{
					Id = blogPostDomainModel.Id,
					Heading = blogPostDomainModel.Heading,
					PageTitle = blogPostDomainModel.PageTitle,
					Content = blogPostDomainModel.Content,
					ShortDescription = blogPostDomainModel.ShortDescription,
					FeaturedImageUrl = blogPostDomainModel.FeaturedImageUrl,
					UrlHandle = blogPostDomainModel.UrlHandle,
					PublishedDate = blogPostDomainModel.PublishedDate,
					Author = blogPostDomainModel.Author,
					Visible = blogPostDomainModel.Visible
				};
				Tags = string.Join(",", blogPostDomainModel.Tags.Select(x => x.Name));
			}
		}

		public async Task<IActionResult> OnPostEdit()
		{
			ValidateEditBlogPost();
			if (ModelState.IsValid)
			{
				try
				{
					var blogPostDomainModel = new BlogPost()
					{
						Id = BlogPost.Id,
						Heading = BlogPost.Heading,
						PageTitle = BlogPost.PageTitle,
						Content = BlogPost.Content,
						ShortDescription = BlogPost.ShortDescription,
						FeaturedImageUrl = BlogPost.FeaturedImageUrl,
						UrlHandle = BlogPost.UrlHandle,
						PublishedDate = BlogPost.PublishedDate,
						Author = BlogPost.Author,
						Visible = BlogPost.Visible,
						Tags = new List<Tag>(Tags.Split(',').Select(x => new Tag()
						{
							Name = x.Trim()
						}))

					};
					await blogPostRepository.UpdateAsync(blogPostDomainModel);
					ViewData["Notification"] = new Notification()
					{
						Message = "Record updated successfully",
						Type = Enum.NotificationType.Success
					};
				}
				catch (Exception)
				{
					ViewData["Notification"] = new Notification()
					{
						Message = "Something went wrong",
						Type = Enum.NotificationType.Error
					};
				}
				return Page();
			}

			return Page();
		}

		public async Task<IActionResult> OnPostDelete()
		{
			var deleted = await blogPostRepository.DeleteAsync(BlogPost.Id);
			if (deleted)
			{
				var notification = new Notification
				{
					Type = Enum.NotificationType.Success,
					Message = "New blog deleted successfully."
				};

				TempData["Notification"] = JsonSerializer.Serialize(notification);
				return RedirectToPage("/Admin/Blogs/List");
			}
			return Page();
		}

		private void ValidateEditBlogPost()
		{
			if (!string.IsNullOrEmpty(BlogPost.Heading))
			{
				if (BlogPost.Heading.Length < 10 || BlogPost.Heading.Length > 72)
				{
					ModelState.AddModelError("BlogPost.Heading", "Heading can only be between 10 and 72 characters.");
				}
			}
		}
	}
}
