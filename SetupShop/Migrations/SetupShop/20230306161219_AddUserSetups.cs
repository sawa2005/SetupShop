using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SetupShop.Migrations.SetupShop
{
    /// <inheritdoc />
    public partial class AddUserSetups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SetupSetupShopUser",
                columns: table => new
                {
                    SetupsId = table.Column<int>(type: "INTEGER", nullable: false),
                    UsersId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SetupSetupShopUser", x => new { x.SetupsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_SetupSetupShopUser_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SetupSetupShopUser_Setups_SetupsId",
                        column: x => x.SetupsId,
                        principalTable: "Setups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SetupSetupShopUser_UsersId",
                table: "SetupSetupShopUser",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SetupSetupShopUser");
        }
    }
}
