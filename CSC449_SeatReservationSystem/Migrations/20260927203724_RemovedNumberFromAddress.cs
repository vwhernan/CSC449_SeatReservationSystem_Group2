using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSC449_SeatReservationSystem.Migrations
{
    /// <inheritdoc />
    public partial class RemovedNumberFromAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "StreetName",
                table: "Addresses",
                newName: "Street");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Street",
                table: "Addresses",
                newName: "StreetName");

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "Addresses",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }
    }
}
