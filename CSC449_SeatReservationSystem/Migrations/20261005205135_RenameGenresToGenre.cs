using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSC449_SeatReservationSystem.Migrations
{
    /// <inheritdoc />
    public partial class RenameGenresToGenre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Genres",
                table: "Movies",
                newName: "Genre");

            migrationBuilder.AddColumn<int>(
                name: "MovieTheaterTheaterId",
                table: "Movies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_MovieTheaterTheaterId",
                table: "Movies",
                column: "MovieTheaterTheaterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_MovieTheaters_MovieTheaterTheaterId",
                table: "Movies",
                column: "MovieTheaterTheaterId",
                principalTable: "MovieTheaters",
                principalColumn: "TheaterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_MovieTheaters_MovieTheaterTheaterId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_MovieTheaterTheaterId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "MovieTheaterTheaterId",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "Genre",
                table: "Movies",
                newName: "Genres");
        }
    }
}
