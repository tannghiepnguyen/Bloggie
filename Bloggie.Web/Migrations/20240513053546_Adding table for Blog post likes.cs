using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bloggie.Web.Migrations
{
	/// <inheritdoc />
	public partial class AddingtableforBlogpostlikes : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "BlogPostLike",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					BLogPostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_BlogPostLike", x => x.Id);
					table.ForeignKey(
						name: "FK_BlogPostLike_BlogPosts_BLogPostId",
						column: x => x.BLogPostId,
						principalTable: "BlogPosts",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_BlogPostLike_BLogPostId",
				table: "BlogPostLike",
				column: "BLogPostId");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "BlogPostLike");
		}
	}
}
