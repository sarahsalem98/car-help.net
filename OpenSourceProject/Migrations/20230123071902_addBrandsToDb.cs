using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OpenSourceProject.Migrations
{
    /// <inheritdoc />
    public partial class addBrandsToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "brands",
                columns: new[] { "Id", "CreatedAt", "Name_AR", "Name_EN", "UpdatedAt" },
                values: new object[,]
                {
                    { "45c5f5d3-d732-4620-a6ef-fba41b8ec582", new DateTime(2023, 1, 23, 9, 19, 1, 606, DateTimeKind.Local).AddTicks(2357), "تويوتا", "toyota", new DateTime(2023, 1, 23, 9, 19, 1, 606, DateTimeKind.Local).AddTicks(2369) },
                    { "772469fb-f48d-408b-9e99-bc90b1ea49e7", new DateTime(2023, 1, 23, 9, 19, 1, 606, DateTimeKind.Local).AddTicks(2385), "BMW", "BMW", new DateTime(2023, 1, 23, 9, 19, 1, 606, DateTimeKind.Local).AddTicks(2386) },
                    { "83a68baa-629f-4f73-b38a-fc7d8705afd4", new DateTime(2023, 1, 23, 9, 19, 1, 606, DateTimeKind.Local).AddTicks(2377), "مرسيدس", "marcedes", new DateTime(2023, 1, 23, 9, 19, 1, 606, DateTimeKind.Local).AddTicks(2378) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "brands",
                keyColumn: "Id",
                keyValue: "45c5f5d3-d732-4620-a6ef-fba41b8ec582");

            migrationBuilder.DeleteData(
                table: "brands",
                keyColumn: "Id",
                keyValue: "772469fb-f48d-408b-9e99-bc90b1ea49e7");

            migrationBuilder.DeleteData(
                table: "brands",
                keyColumn: "Id",
                keyValue: "83a68baa-629f-4f73-b38a-fc7d8705afd4");
        }
    }
}
