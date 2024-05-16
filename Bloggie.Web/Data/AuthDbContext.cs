using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Data
{
	public class AuthDbContext : IdentityDbContext
	{
		public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
		{
		}
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			var superAdminRoleId = "435bc4b7-f200-4984-88d8-001d345f9ac3";
			var adminRoleId = "49307f5d-bcef-439e-857a-f8d6bce8c02f";
			var userRoleId = "8e653df7-1c2f-44d1-8470-c9bcfdae3302";

			//Seed Roles (User, Admin, Super Admin)
			var roles = new List<IdentityRole>
			{
				new IdentityRole { Name = "User", NormalizedName = "User", Id = userRoleId, ConcurrencyStamp = userRoleId },
				new IdentityRole { Name = "Admin", NormalizedName = "Admin", Id=adminRoleId, ConcurrencyStamp=adminRoleId },
				new IdentityRole { Name = "SuperAdmin", NormalizedName = "SuperAdmin", Id = superAdminRoleId, ConcurrencyStamp = superAdminRoleId }
			};
			builder.Entity<IdentityRole>().HasData(roles);

			//Seed Super Admin User
			var superAdminId = "47ceadf8-1137-4786-81b8-1f98c0adf245";
			var superAdminUser = new IdentityUser
			{
				Id = superAdminId,
				UserName = "superadmin@bloggie.com",
				Email = "superadmin@bloggie.com",
				NormalizedEmail = "superadmin@bloggie.com".ToUpper(),
				NormalizedUserName = "superadmin@bloggie.com".ToUpper(),
			};
			superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdminUser, "SuperAdmin@123");
			builder.Entity<IdentityUser>().HasData(superAdminUser);

			//Add All Roles to Super Admin 
			var superAdminRoles = new List<IdentityUserRole<string>>
			{
				new IdentityUserRole<string> { RoleId = userRoleId, UserId = superAdminId },
				new IdentityUserRole<string> { RoleId = adminRoleId, UserId = superAdminId },
				new IdentityUserRole<string> { RoleId = superAdminRoleId, UserId = superAdminId }
			};
			builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
		}
	}
}
