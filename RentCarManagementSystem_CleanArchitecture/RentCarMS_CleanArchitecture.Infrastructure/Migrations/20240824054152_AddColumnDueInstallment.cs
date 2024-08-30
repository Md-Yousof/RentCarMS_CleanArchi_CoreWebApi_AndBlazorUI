using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentCarMS_CleanArchitecture.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnDueInstallment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DueInstallment",
                table: "duePayments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DueInstallment",
                table: "duePayments");
        }
    }
}
