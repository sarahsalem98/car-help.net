using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OpenSourceProject.Migrations
{
    /// <inheritdoc />
    public partial class addRecordsToCategoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "Id", "CreatedAt", "Name_AR", "Name_EN", "UpdatedAt" },
                values: new object[,]
                {
                    { "212b2f4c-f096-463a-b12b-64c95f29ff01", new DateTime(2023, 1, 31, 9, 35, 17, 626, DateTimeKind.Local).AddTicks(6968), "عجل", "wheels", new DateTime(2023, 1, 31, 9, 35, 17, 626, DateTimeKind.Local).AddTicks(6987) },
                    { "d5f49a10-e81b-4858-83a4-6510e60cfc48", new DateTime(2023, 1, 31, 9, 35, 17, 626, DateTimeKind.Local).AddTicks(7003), "زجاج", "glass", new DateTime(2023, 1, 31, 9, 35, 17, 626, DateTimeKind.Local).AddTicks(7004) },
                    { "ef091177-b4c5-4f12-ac55-21fd8ea9525e", new DateTime(2023, 1, 31, 9, 35, 17, 626, DateTimeKind.Local).AddTicks(7013), "مرايات", "mirror", new DateTime(2023, 1, 31, 9, 35, 17, 626, DateTimeKind.Local).AddTicks(7014) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: "212b2f4c-f096-463a-b12b-64c95f29ff01");

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: "d5f49a10-e81b-4858-83a4-6510e60cfc48");

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: "ef091177-b4c5-4f12-ac55-21fd8ea9525e");
        }
    }
}
