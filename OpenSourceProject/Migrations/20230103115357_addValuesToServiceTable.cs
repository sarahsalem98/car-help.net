using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OpenSourceProject.Migrations
{
    /// <inheritdoc />
    public partial class addValuesToServiceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "services",
                columns: new[] { "Id", "CreatedAt", "Name_AR", "Name_EN", "PhotoUrl", "UpdatedAt" },
                values: new object[,]
                {
                    { "cbe9be46-341a-4193-afcb-96631a160925", new DateTime(2023, 1, 3, 13, 53, 56, 466, DateTimeKind.Local).AddTicks(7379), "الصيانه العامه", "public repair", null, new DateTime(2023, 1, 3, 13, 53, 56, 466, DateTimeKind.Local).AddTicks(7393) },
                    { "ddf7efad-b456-46e4-acb3-84a8b4825c4c", new DateTime(2023, 1, 3, 13, 53, 56, 466, DateTimeKind.Local).AddTicks(7407), "العيادات", "clincs", null, new DateTime(2023, 1, 3, 13, 53, 56, 466, DateTimeKind.Local).AddTicks(7409) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "services",
                keyColumn: "Id",
                keyValue: "cbe9be46-341a-4193-afcb-96631a160925");

            migrationBuilder.DeleteData(
                table: "services",
                keyColumn: "Id",
                keyValue: "ddf7efad-b456-46e4-acb3-84a8b4825c4c");
        }
    }
}
