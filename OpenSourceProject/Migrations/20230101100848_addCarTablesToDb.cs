using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenSourceProject.Migrations
{
    public partial class addCarTablesToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.CreateTable(
                name: "carModels",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Name_AR = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name_EN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "carTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Name_AR = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name_EN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cars",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChassisNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarModelId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CarTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cars_carModels_CarModelId",
                        column: x => x.CarModelId,
                        principalTable: "carModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cars_carTypes_CarTypeId",
                        column: x => x.CarTypeId,
                        principalTable: "carTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cars_CarModelId",
                table: "cars",
                column: "CarModelId");

            migrationBuilder.CreateIndex(
                name: "IX_cars_CarTypeId",
                table: "cars",
                column: "CarTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cars");

            migrationBuilder.DropTable(
                name: "carModels");

            migrationBuilder.DropTable(
                name: "carTypes");
          }
    }
}
