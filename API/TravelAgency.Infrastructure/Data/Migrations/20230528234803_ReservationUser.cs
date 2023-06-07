using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgency.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ReservationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_AspNetUsers_UserId",
                table: "Reservation");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Reservation",
                newName: "TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_UserId",
                table: "Reservation",
                newName: "IX_Reservation_TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_AspNetUsers_TenantId",
                table: "Reservation",
                column: "TenantId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_AspNetUsers_TenantId",
                table: "Reservation");

            migrationBuilder.RenameColumn(
                name: "TenantId",
                table: "Reservation",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_TenantId",
                table: "Reservation",
                newName: "IX_Reservation_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_AspNetUsers_UserId",
                table: "Reservation",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
