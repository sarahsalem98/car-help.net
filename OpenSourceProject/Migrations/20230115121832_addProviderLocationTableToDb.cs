using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenSourceProject.Migrations
{
    /// <inheritdoc />
    public partial class addProviderLocationTableToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "providerLocations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    ProviderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Lat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Long = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_providerLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_providerLocations_AspNetUsers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_providerLocations_cities_CityId",
                        column: x => x.CityId,
                        principalTable: "cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

     

            migrationBuilder.CreateIndex(
                name: "IX_providerLocations_ProviderId",
                table: "providerLocations",
                column: "ProviderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "providerLocations");
        }
    }
}
