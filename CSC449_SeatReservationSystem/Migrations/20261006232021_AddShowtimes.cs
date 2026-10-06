using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSC449_SeatReservationSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddShowtimes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Showtime_Movies_MovieId",
                table: "Showtime");

            migrationBuilder.DropForeignKey(
                name: "FK_Showtime_TheaterAuditoriums_AuditoriumId",
                table: "Showtime");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Showtime",
                table: "Showtime");

            migrationBuilder.RenameTable(
                name: "Showtime",
                newName: "Showtimes");

            migrationBuilder.RenameIndex(
                name: "IX_Showtime_MovieId",
                table: "Showtimes",
                newName: "IX_Showtimes_MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_Showtime_AuditoriumId",
                table: "Showtimes",
                newName: "IX_Showtimes_AuditoriumId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Showtimes",
                table: "Showtimes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Showtimes_Movies_MovieId",
                table: "Showtimes",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Showtimes_TheaterAuditoriums_AuditoriumId",
                table: "Showtimes",
                column: "AuditoriumId",
                principalTable: "TheaterAuditoriums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Showtimes_Movies_MovieId",
                table: "Showtimes");

            migrationBuilder.DropForeignKey(
                name: "FK_Showtimes_TheaterAuditoriums_AuditoriumId",
                table: "Showtimes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Showtimes",
                table: "Showtimes");

            migrationBuilder.RenameTable(
                name: "Showtimes",
                newName: "Showtime");

            migrationBuilder.RenameIndex(
                name: "IX_Showtimes_MovieId",
                table: "Showtime",
                newName: "IX_Showtime_MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_Showtimes_AuditoriumId",
                table: "Showtime",
                newName: "IX_Showtime_AuditoriumId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Showtime",
                table: "Showtime",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Showtime_Movies_MovieId",
                table: "Showtime",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Showtime_TheaterAuditoriums_AuditoriumId",
                table: "Showtime",
                column: "AuditoriumId",
                principalTable: "TheaterAuditoriums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
