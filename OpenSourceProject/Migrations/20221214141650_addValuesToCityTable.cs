using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenSourceProject.Migrations
{
    public partial class addValuesToCityTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "cities",
                columns: new[] { "Id", "CreatedAt", "Name_AR", "Name_EN", "UpdatedAt" },
                values: new object[] { 1, new DateTime(2022, 12, 14, 16, 16, 49, 795, DateTimeKind.Local).AddTicks(8044), "الرياض", "El-Reyad", new DateTime(2022, 12, 14, 16, 16, 49, 795, DateTimeKind.Local).AddTicks(8060) });

            migrationBuilder.InsertData(
                table: "cities",
                columns: new[] { "Id", "CreatedAt", "Name_AR", "Name_EN", "UpdatedAt" },
                values: new object[] { 2, new DateTime(2022, 12, 14, 16, 16, 49, 795, DateTimeKind.Local).AddTicks(8065), "جدة", "gada", new DateTime(2022, 12, 14, 16, 16, 49, 795, DateTimeKind.Local).AddTicks(8067) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
