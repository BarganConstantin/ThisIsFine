using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThiIsFine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Bargan_User_FreeAttemtpsProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FreeTrialAttempts",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FreeTrialAttempts",
                table: "AspNetUsers");
        }
    }
}
