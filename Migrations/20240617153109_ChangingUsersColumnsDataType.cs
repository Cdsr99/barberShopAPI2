using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberShopAPI2.Migrations
{
    /// <inheritdoc />
    public partial class ChangingUsersColumnsDataType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_EmailConfirmed",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_EmailConfirmed",
                table: "Users",
                column: "EmailConfirmed",
                unique: true);
        }
    }
}
