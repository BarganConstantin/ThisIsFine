using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThiIsFine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Bargan_Purchase_RemainingAttemptsProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsUsed",
                table: "Purchases");

            migrationBuilder.AddColumn<int>(
                name: "RemainingAttempts",
                table: "Purchases",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RemainingAttempts",
                table: "Purchases");

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                table: "Purchases",
                type: "bit",
                nullable: true);
        }
    }
}
