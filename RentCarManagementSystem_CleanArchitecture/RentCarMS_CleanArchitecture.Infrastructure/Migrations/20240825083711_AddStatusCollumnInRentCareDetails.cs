using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentCarMS_CleanArchitecture.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusCollumnInRentCareDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "rentCarDetails",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "rentCarDetails");
        }
    }
}
