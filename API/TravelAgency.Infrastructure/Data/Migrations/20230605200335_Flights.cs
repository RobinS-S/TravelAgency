using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace TravelAgency.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Flights : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_AspNetUsers_OwnerId",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_AspNetUsers_TenantId",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Residences_ResidenceId",
                table: "Reservation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservation",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Images");

            migrationBuilder.RenameTable(
                name: "Reservation",
                newName: "Reservations");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_TenantId",
                table: "Reservations",
                newName: "IX_Reservations_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_ResidenceId",
                table: "Reservations",
                newName: "IX_Reservations_ResidenceId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_OwnerId",
                table: "Reservations",
                newName: "IX_Reservations_OwnerId");

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerDay",
                table: "Residences",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Flight",
                columns: table => new
                {
                    ReservationId = table.Column<long>(type: "bigint", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FromDeparture = table.Column<DateTime>(type: "datetime", nullable: false),
                    Until = table.Column<DateTime>(type: "datetime", nullable: false),
                    AirportCode = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DestinationAirportCode = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FlightNumber = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Coordinates = table.Column<Point>(type: "point", nullable: false),
                    OwnerId = table.Column<string>(type: "varchar(95)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flight", x => new { x.ReservationId, x.Id });
                    table.ForeignKey(
                        name: "FK_Flight_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Flight_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FlightSeat",
                columns: table => new
                {
                    FlightReservationId = table.Column<long>(type: "bigint", nullable: false),
                    FlightId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SeatNumber = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightSeat", x => new { x.FlightReservationId, x.FlightId, x.Id });
                    table.ForeignKey(
                        name: "FK_FlightSeat_Flight_FlightReservationId_FlightId",
                        columns: x => new { x.FlightReservationId, x.FlightId },
                        principalTable: "Flight",
                        principalColumns: new[] { "ReservationId", "Id" },
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Flight_OwnerId",
                table: "Flight",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_AspNetUsers_OwnerId",
                table: "Reservations",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_AspNetUsers_TenantId",
                table: "Reservations",
                column: "TenantId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Residences_ResidenceId",
                table: "Reservations",
                column: "ResidenceId",
                principalTable: "Residences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AspNetUsers_OwnerId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AspNetUsers_TenantId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Residences_ResidenceId",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "FlightSeat");

            migrationBuilder.DropTable(
                name: "Flight");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "PricePerDay",
                table: "Residences");

            migrationBuilder.RenameTable(
                name: "Reservations",
                newName: "Reservation");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_TenantId",
                table: "Reservation",
                newName: "IX_Reservation_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_ResidenceId",
                table: "Reservation",
                newName: "IX_Reservation_ResidenceId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_OwnerId",
                table: "Reservation",
                newName: "IX_Reservation_OwnerId");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Images",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservation",
                table: "Reservation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_AspNetUsers_OwnerId",
                table: "Reservation",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_AspNetUsers_TenantId",
                table: "Reservation",
                column: "TenantId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Residences_ResidenceId",
                table: "Reservation",
                column: "ResidenceId",
                principalTable: "Residences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
