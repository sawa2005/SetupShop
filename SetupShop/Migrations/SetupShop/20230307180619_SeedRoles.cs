using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SetupShop.Migrations.SetupShop
{
    public partial class SeedRoles : Migration
    {
        private string AuthorRoleId = Guid.NewGuid().ToString();

        private string User1Id = "a98c79d1-1351-4b87-b70e-2adca958017a";

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { AuthorRoleId, "Author", "AUTHOR", null });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { User1Id, AuthorRoleId });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
            table: "AspNetRoles",
            keyColumn: "Id",
            keyValue: AuthorRoleId);

            migrationBuilder.DeleteData(
            table: "AspNetUserRoles",
            keyColumns: new[] { "UserId", "RoleId" },
            keyValues: new object[] { User1Id, AuthorRoleId });
        }
    }
}