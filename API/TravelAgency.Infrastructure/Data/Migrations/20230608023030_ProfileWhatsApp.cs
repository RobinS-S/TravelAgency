using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgency.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProfileWhatsApp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasWhatsApp",
                table: "AspNetUsers",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasWhatsApp",
                table: "AspNetUsers");
        }
    }
}
