using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SetupShop.Migrations.SetupShop
{
    /// <inheritdoc />
    public partial class ContextCleanUP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Setups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Author = table.Column<string>(type: "TEXT", nullable: true),
                    File = table.Column<byte[]>(type: "BLOB", nullable: true),
                    FileName = table.Column<string>(type: "TEXT", nullable: true),
                    FileType = table.Column<string>(type: "TEXT", nullable: true),
                    Car = table.Column<string>(type: "TEXT", nullable: false),
                    Track = table.Column<string>(type: "TEXT", nullable: false),
                    Season = table.Column<string>(type: "TEXT", nullable: false),
                    Week = table.Column<string>(type: "TEXT", nullable: false),
                    Series = table.Column<string>(type: "TEXT", nullable: false),
                    VideoUrl = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(8, 2)", nullable: false),
                    Image = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setups", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Setups");
        }
    }
}
