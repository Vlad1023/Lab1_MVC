using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lab1_MVC.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Workers",
                columns: table => new
                {
                    WorkersId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Patronymic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Payment = table.Column<double>(type: "float", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => x.WorkersId);
                });

            migrationBuilder.CreateTable(
                name: "WorkTypes",
                columns: table => new
                {
                    WorkTypesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentPerDay = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkTypes", x => x.WorkTypesId);
                });

            migrationBuilder.CreateTable(
                name: "Works",
                columns: table => new
                {
                    WorksId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkersId = table.Column<int>(type: "int", nullable: false),
                    WorkTypesId = table.Column<int>(type: "int", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Works", x => x.WorksId);
                    table.ForeignKey(
                        name: "FK_Works_Workers_WorkersId",
                        column: x => x.WorkersId,
                        principalTable: "Workers",
                        principalColumn: "WorkersId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Works_WorkTypes_WorkTypesId",
                        column: x => x.WorkTypesId,
                        principalTable: "WorkTypes",
                        principalColumn: "WorkTypesId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "WorkTypes",
                columns: new[] { "WorkTypesId", "Description", "PaymentPerDay" },
                values: new object[,]
                {
                    { 1, "Manage documents", 4.0 },
                    { 2, "Work till night", 10.0 },
                    { 3, "Clean toilet", 1.0 },
                    { 4, "Deal with clients", 15.0 },
                    { 5, "Clean windows", 3.0 },
                    { 6, "Clean toilet till night", 6.0 },
                    { 7, "Wash cups", 2.0 },
                    { 8, "Work with documents", 8.0 },
                    { 9, "Clean floor", 5.0 }
                });

            migrationBuilder.InsertData(
                table: "Workers",
                columns: new[] { "WorkersId", "Name", "Patronymic", "Payment", "Surname" },
                values: new object[,]
                {
                    { 1, "Vladyslav", "Vadymovich", 1000.0, "Volkovskyi" },
                    { 2, "Roman", "KotikPojiloy", 888.0, "Kotenko" },
                    { 3, "Valeriy", "Albertovich", 568.0, "Fekalis" },
                    { 4, "Leonid", "Vladimirovych", 1200.0, "Machenko" }
                });

            migrationBuilder.InsertData(
                table: "Works",
                columns: new[] { "WorksId", "EndDate", "StartDate", "WorkTypesId", "WorkersId" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 2, new DateTime(2021, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1 },
                    { 3, new DateTime(2021, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, 2 },
                    { 4, new DateTime(2021, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 2 },
                    { 5, new DateTime(2021, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 3 },
                    { 6, new DateTime(2021, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 3 },
                    { 7, new DateTime(2021, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 4 },
                    { 8, new DateTime(2021, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2015, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 4 },
                    { 9, new DateTime(2021, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Works_WorkersId",
                table: "Works",
                column: "WorkersId");

            migrationBuilder.CreateIndex(
                name: "IX_Works_WorkTypesId",
                table: "Works",
                column: "WorkTypesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Works");

            migrationBuilder.DropTable(
                name: "Workers");

            migrationBuilder.DropTable(
                name: "WorkTypes");
        }
    }
}
