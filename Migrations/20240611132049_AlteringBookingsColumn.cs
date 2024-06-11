using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberShopAPI2.Migrations
{
    /// <inheritdoc />
    public partial class AlteringBookingsColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Bookings_SchedulesId",
                table: "Bookings",
                column: "SchedulesId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bookings_SchedulesId",
                table: "Bookings");
        }
    }
}
