using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OpenSourceProject.Migrations
{
    /// <inheritdoc />
    public partial class addValuesToSubServiceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
         

            migrationBuilder.InsertData(
                table: "subServices",
                columns: new[] { "Id", "CreatedAt", "Name_AR", "Name_EN", "PhotoUrl", "ServiceId", "UpdatedAt" },
                values: new object[,]
                {
                    { "60798a41-b014-4fba-bb1b-fd630fe84743", new DateTime(2023, 1, 3, 14, 0, 46, 416, DateTimeKind.Local).AddTicks(7553), "دهان", "painting", null, "ddf7efad-b456-46e4-acb3-84a8b4825c4c", new DateTime(2023, 1, 3, 14, 0, 46, 416, DateTimeKind.Local).AddTicks(7572) },
                    { "7968baad-655c-4b9a-9f82-b85938c1af64", new DateTime(2023, 1, 3, 14, 0, 46, 416, DateTimeKind.Local).AddTicks(7590), "غسل سيارات", "car wash", null, "ddf7efad-b456-46e4-acb3-84a8b4825c4c", new DateTime(2023, 1, 3, 14, 0, 46, 416, DateTimeKind.Local).AddTicks(7591) },
                    { "c6d6cf71-6419-4f4b-9a32-b2be2ae97c9c", new DateTime(2023, 1, 3, 14, 0, 46, 416, DateTimeKind.Local).AddTicks(7581), "تغيير مواتيير", "motors change", null, "cbe9be46-341a-4193-afcb-96631a160925", new DateTime(2023, 1, 3, 14, 0, 46, 416, DateTimeKind.Local).AddTicks(7583) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "subServices",
                keyColumn: "Id",
                keyValue: "60798a41-b014-4fba-bb1b-fd630fe84743");

            migrationBuilder.DeleteData(
                table: "subServices",
                keyColumn: "Id",
                keyValue: "7968baad-655c-4b9a-9f82-b85938c1af64");

            migrationBuilder.DeleteData(
                table: "subServices",
                keyColumn: "Id",
                keyValue: "c6d6cf71-6419-4f4b-9a32-b2be2ae97c9c");

          
        }
    }
}
