using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Playmor_Asp.Migrations
{
    /// <inheritdoc />
    public partial class AddScoreFieldToUserGameTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "UserGames",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                table: "UserGames");
        }
    }
}
