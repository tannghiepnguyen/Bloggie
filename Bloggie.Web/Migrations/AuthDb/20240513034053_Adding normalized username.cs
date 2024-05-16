using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bloggie.Web.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class Addingnormalizedusername : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "47ceadf8-1137-4786-81b8-1f98c0adf245",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b914ee61-17e7-4731-84be-cd0b9057b90f", "SUPERADMIN@BLOGGIE.COM", "SUPERADMIN@BLOGGIE.COM", "AQAAAAIAAYagAAAAELOXcJrdFpEJBDHQ4KsII9wNFbMnLW3QA9xQQi3jzprepSpqIedBam6EBGLup14MXg==", "4fda0628-b1ca-4304-8ffe-ed3df349ad86" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "47ceadf8-1137-4786-81b8-1f98c0adf245",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cad58947-488b-4f3a-979f-b460d82364c2", null, null, "AQAAAAIAAYagAAAAEHHeyrQsv5c2FhWN9Ru140dEryv5tWrX0ZuiI9wopCtVSvl0nvPOU1oqksZIYLgN2Q==", "400f167e-7917-465e-a319-74f58dd3cfb4" });
        }
    }
}
