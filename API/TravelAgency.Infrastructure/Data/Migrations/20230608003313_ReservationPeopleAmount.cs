using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgency.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ReservationPeopleAmount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AmountOfPeople",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountOfPeople",
                table: "Reservations");
        }
    }
}
