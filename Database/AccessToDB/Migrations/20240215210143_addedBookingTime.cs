using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccessToDB.Migrations
{
    /// <inheritdoc />
    public partial class addedBookingTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookingTime",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 30);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookingTime",
                table: "Books");
        }
    }
}
