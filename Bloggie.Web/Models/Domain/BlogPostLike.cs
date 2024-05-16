namespace Bloggie.Web.Models.Domain
{
	public class BlogPostLike
	{
		public Guid Id { get; set; }
		public Guid BLogPostId { get; set; }
		public Guid UserId { get; set; }
	}
}
