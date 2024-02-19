using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace dotNet_wepApi_entityFrameWork.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Code = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Code = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PositionCode = table.Column<int>(type: "int", nullable: true),
                    SalaryStatus = table.Column<int>(type: "int", nullable: false),
                    HiringDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Code);
                    table.ForeignKey(
                        name: "FK_Employees_Positions_PositionCode",
                        column: x => x.PositionCode,
                        principalTable: "Positions",
                        principalColumn: "Code");
                });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Code", "Name" },
                values: new object[,]
                {
                    { 1, "Manager" },
                    { 2, "HR" },
                    { 3, "Programmer" },
                    { 4, "Accountant" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PositionCode",
                table: "Employees",
                column: "PositionCode");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_Name",
                table: "Positions",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Positions");
        }
    }
}
