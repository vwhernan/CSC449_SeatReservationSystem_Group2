using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSC449_SeatReservationSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditoriumSeatShowTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieTheaters_Addresses_AddressId",
                table: "MovieTheaters");

            migrationBuilder.DropIndex(
                name: "IX_MovieTheaters_AddressId",
                table: "MovieTheaters");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "MovieTheaters");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "Addresses",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "MovieTheaters",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TheaterId",
                table: "Addresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Movie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Genres = table.Column<int>(type: "int", nullable: false),
                    MovieLengthMinutes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TheaterAuditoriums",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TheaterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheaterAuditoriums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TheaterAuditoriums_MovieTheaters_TheaterId",
                        column: x => x.TheaterId,
                        principalTable: "MovieTheaters",
                        principalColumn: "TheaterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Row = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SeatNumber = table.Column<int>(type: "int", nullable: false),
                    SeatType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    AuditoriumId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seats_TheaterAuditoriums_AuditoriumId",
                        column: x => x.AuditoriumId,
                        principalTable: "TheaterAuditoriums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Showtime",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TicketPrice = table.Column<double>(type: "float", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    AuditoriumId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Showtime", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Showtime_Movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Showtime_TheaterAuditoriums_AuditoriumId",
                        column: x => x.AuditoriumId,
                        principalTable: "TheaterAuditoriums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_TheaterId",
                table: "Addresses",
                column: "TheaterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Seats_AuditoriumId",
                table: "Seats",
                column: "AuditoriumId");

            migrationBuilder.CreateIndex(
                name: "IX_Showtime_AuditoriumId",
                table: "Showtime",
                column: "AuditoriumId");

            migrationBuilder.CreateIndex(
                name: "IX_Showtime_MovieId",
                table: "Showtime",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_TheaterAuditoriums_TheaterId",
                table: "TheaterAuditoriums",
                column: "TheaterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_MovieTheaters_TheaterId",
                table: "Addresses",
                column: "TheaterId",
                principalTable: "MovieTheaters",
                principalColumn: "TheaterId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_MovieTheaters_TheaterId",
                table: "Addresses");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "Showtime");

            migrationBuilder.DropTable(
                name: "Movie");

            migrationBuilder.DropTable(
                name: "TheaterAuditoriums");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_TheaterId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "MovieTheaters");

            migrationBuilder.DropColumn(
                name: "TheaterId",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Addresses",
                newName: "AddressId");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "MovieTheaters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MovieTheaters_AddressId",
                table: "MovieTheaters",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieTheaters_Addresses_AddressId",
                table: "MovieTheaters",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
