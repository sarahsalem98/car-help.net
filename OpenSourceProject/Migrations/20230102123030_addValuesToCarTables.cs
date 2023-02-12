using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OpenSourceProject.Migrations
{
    /// <inheritdoc />
    public partial class addValuesToCarTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "carModels",
                columns: new[] { "Id", "CreatedAt", "Name_AR", "Name_EN", "UpdatedAt" },
                values: new object[,]
                {
                    { "287d0cb5-8dc2-447c-a961-d3178a766b29", new DateTime(2023, 1, 2, 14, 30, 29, 709, DateTimeKind.Local).AddTicks(6454), "تويوتا", "toyota", new DateTime(2023, 1, 2, 14, 30, 29, 709, DateTimeKind.Local).AddTicks(6456) },
                    { "4d8df18c-577d-46f9-8420-074ffe4ac88f", new DateTime(2023, 1, 2, 14, 30, 29, 709, DateTimeKind.Local).AddTicks(6424), "مرسيدس", "marceds", new DateTime(2023, 1, 2, 14, 30, 29, 709, DateTimeKind.Local).AddTicks(6441) }
                });

            migrationBuilder.InsertData(
                table: "carTypes",
                columns: new[] { "Id", "CreatedAt", "Name_AR", "Name_EN", "UpdatedAt" },
                values: new object[,]
                {
                    { "784d2957-30ae-4758-9320-660316e7b87e", new DateTime(2023, 1, 2, 14, 30, 29, 709, DateTimeKind.Local).AddTicks(7095), "قديمة", "old", new DateTime(2023, 1, 2, 14, 30, 29, 709, DateTimeKind.Local).AddTicks(7100) },
                    { "8e71ff69-2391-414a-bb92-255be4dd5ea9", new DateTime(2023, 1, 2, 14, 30, 29, 709, DateTimeKind.Local).AddTicks(7110), "جديدة", "new", new DateTime(2023, 1, 2, 14, 30, 29, 709, DateTimeKind.Local).AddTicks(7112) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "carModels",
                keyColumn: "Id",
                keyValue: "287d0cb5-8dc2-447c-a961-d3178a766b29");

            migrationBuilder.DeleteData(
                table: "carModels",
                keyColumn: "Id",
                keyValue: "4d8df18c-577d-46f9-8420-074ffe4ac88f");

            migrationBuilder.DeleteData(
                table: "carTypes",
                keyColumn: "Id",
                keyValue: "784d2957-30ae-4758-9320-660316e7b87e");

            migrationBuilder.DeleteData(
                table: "carTypes",
                keyColumn: "Id",
                keyValue: "8e71ff69-2391-414a-bb92-255be4dd5ea9");
        }
    }
}
