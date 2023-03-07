using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SetupShop.Migrations.SetupShop
{
    /// <inheritdoc />
    public partial class AddUserSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserSetup",
                columns: table => new
                {
                    SetupsId = table.Column<int>(type: "INTEGER", nullable: false),
                    UsersId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSetup", x => new { x.SetupsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_UserSetup_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSetup_Setups_SetupsId",
                        column: x => x.SetupsId,
                        principalTable: "Setups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserSetup_UsersId",
                table: "UserSetup",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserSetups",
                columns: table => new
                {
                    UsersId = table.Column<string>(type: "TEXT", nullable: false),
                    SetupsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSetups", x => new { x.UsersId, x.SetupsId });
                    table.ForeignKey(
                        name: "FK_UserSetups_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSetups_Setups_SetupsId",
                        column: x => x.SetupsId,
                        principalTable: "Setups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
