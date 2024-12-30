using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Playmor_Asp.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusToUserGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "UserGames",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "UserGames");
        }
    }
}
