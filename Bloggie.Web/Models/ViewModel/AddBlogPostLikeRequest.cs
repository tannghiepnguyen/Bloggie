namespace Bloggie.Web.Models.ViewModel
{
	public class AddBlogPostLikeRequest
	{
		public Guid BlogPostId { get; set; }
		public Guid UserId { get; set; }
	}
}
