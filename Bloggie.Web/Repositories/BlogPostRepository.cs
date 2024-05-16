﻿using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories
{
	public class BlogPostRepository : IBlogPostRepository
	{
		private readonly BloggieDbContext bloggieDbContext;

		public BlogPostRepository(BloggieDbContext bloggieDbContext)
		{
			this.bloggieDbContext = bloggieDbContext;
		}
		public async Task<BlogPost> AddAsync(BlogPost blogPost)
		{
			await this.bloggieDbContext.BlogPosts.AddAsync(blogPost);
			await this.bloggieDbContext.SaveChangesAsync();
			return blogPost;
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			var existingBlogPost = await bloggieDbContext.BlogPosts.FindAsync(id);
			if (existingBlogPost != null)
			{
				bloggieDbContext.BlogPosts.Remove(existingBlogPost);
				await bloggieDbContext.SaveChangesAsync();
				return true;
			}
			return false;
		}

		public async Task<IEnumerable<BlogPost>> GetAllAsync()
		{
			return await bloggieDbContext.BlogPosts.Include(nameof(BlogPost.Tags)).ToListAsync();
		}

		public async Task<IEnumerable<BlogPost>> GetAllAsync(string tagName)
		{
			return await bloggieDbContext.BlogPosts.Include(nameof(BlogPost.Tags)).Where(x => x.Tags.Any(a => a.Name.Equals(tagName))).ToListAsync();
		}

		public async Task<BlogPost> GetAsync(Guid id)
		{
			return await bloggieDbContext.BlogPosts.Include(nameof(BlogPost.Tags)).FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<BlogPost> GetAsync(string urlHandle)
		{
			return await bloggieDbContext.BlogPosts.Include(nameof(BlogPost.Tags)).FirstOrDefaultAsync(x => x.UrlHandle.Equals(urlHandle));
		}

		public async Task<BlogPost> UpdateAsync(BlogPost blogPost)
		{
			var existingBlogPost = await bloggieDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == blogPost.Id);
			if (existingBlogPost != null)
			{
				existingBlogPost.Heading = blogPost.Heading;
				existingBlogPost.PageTitle = blogPost.PageTitle;
				existingBlogPost.Content = blogPost.Content;
				existingBlogPost.ShortDescription = blogPost.ShortDescription;
				existingBlogPost.FeaturedImageUrl = blogPost.FeaturedImageUrl;
				existingBlogPost.UrlHandle = blogPost.UrlHandle;
				existingBlogPost.PublishedDate = blogPost.PublishedDate;
				existingBlogPost.Author = blogPost.Author;
				existingBlogPost.Visible = blogPost.Visible;

				//Delete the existing tags
				if (blogPost.Tags != null && blogPost.Tags.Any())
				{
					bloggieDbContext.Tags.RemoveRange(existingBlogPost.Tags);
				}

				//Add new tags
				blogPost.Tags.ToList().ForEach(x => x.BlogPostId = existingBlogPost.Id);
				await bloggieDbContext.Tags.AddRangeAsync(blogPost.Tags);
			}
			await bloggieDbContext.SaveChangesAsync();
			return existingBlogPost;
		}
	}
}
