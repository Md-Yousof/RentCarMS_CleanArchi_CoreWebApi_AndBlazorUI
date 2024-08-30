using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentCarMS_CleanArchitecture.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InicialCreateAddCascadeAndEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cars",
                columns: table => new
                {
                    CarID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LicensePlaete = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quentity = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cars", x => x.CarID);
                });

            migrationBuilder.CreateTable(
                name: "members",
                columns: table => new
                {
                    MemberID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JoinDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_members", x => x.MemberID);
                });

            migrationBuilder.CreateTable(
                name: "rentCars",
                columns: table => new
                {
                    RentCarID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberID = table.Column<int>(type: "int", nullable: false),
                    RentCarDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rentCars", x => x.RentCarID);
                    table.ForeignKey(
                        name: "FK_rentCars_members_MemberID",
                        column: x => x.MemberID,
                        principalTable: "members",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "payments",
                columns: table => new
                {
                    PaymentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RentCarID = table.Column<int>(type: "int", nullable: false),
                    RentCarDetailID = table.Column<int>(type: "int", nullable: false),
                    NofInstallMent = table.Column<int>(type: "int", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaidAmmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AdvanceInstallMent = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payments", x => x.PaymentID);
                    table.ForeignKey(
                        name: "FK_payments_rentCars_RentCarID",
                        column: x => x.RentCarID,
                        principalTable: "rentCars",
                        principalColumn: "RentCarID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rentCarDetails",
                columns: table => new
                {
                    RentCarDetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RentCarID = table.Column<int>(type: "int", nullable: false),
                    CarID = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    MonthlyFeeInstallment = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalFee = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NoOfInstallment = table.Column<int>(type: "int", nullable: true),
                    IsReturn = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rentCarDetails", x => x.RentCarDetailID);
                    table.ForeignKey(
                        name: "FK_rentCarDetails_cars_CarID",
                        column: x => x.CarID,
                        principalTable: "cars",
                        principalColumn: "CarID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_rentCarDetails_rentCars_RentCarID",
                        column: x => x.RentCarID,
                        principalTable: "rentCars",
                        principalColumn: "RentCarID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "duePayments",
                columns: table => new
                {
                    DuePaymentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RentCarID = table.Column<int>(type: "int", nullable: false),
                    RentCarDetailID = table.Column<int>(type: "int", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_duePayments", x => x.DuePaymentID);
                    table.ForeignKey(
                        name: "FK_duePayments_rentCarDetails_RentCarDetailID",
                        column: x => x.RentCarDetailID,
                        principalTable: "rentCarDetails",
                        principalColumn: "RentCarDetailID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_duePayments_rentCars_RentCarID",
                        column: x => x.RentCarID,
                        principalTable: "rentCars",
                        principalColumn: "RentCarID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_duePayments_RentCarDetailID",
                table: "duePayments",
                column: "RentCarDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_duePayments_RentCarID",
                table: "duePayments",
                column: "RentCarID");

            migrationBuilder.CreateIndex(
                name: "IX_payments_RentCarID",
                table: "payments",
                column: "RentCarID");

            migrationBuilder.CreateIndex(
                name: "IX_rentCarDetails_CarID",
                table: "rentCarDetails",
                column: "CarID");

            migrationBuilder.CreateIndex(
                name: "IX_rentCarDetails_RentCarID",
                table: "rentCarDetails",
                column: "RentCarID");

            migrationBuilder.CreateIndex(
                name: "IX_rentCars_MemberID",
                table: "rentCars",
                column: "MemberID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "duePayments");

            migrationBuilder.DropTable(
                name: "payments");

            migrationBuilder.DropTable(
                name: "rentCarDetails");

            migrationBuilder.DropTable(
                name: "cars");

            migrationBuilder.DropTable(
                name: "rentCars");

            migrationBuilder.DropTable(
                name: "members");
        }
    }
}
