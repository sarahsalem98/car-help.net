using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenSourceProject.Migrations
{
    public partial class addClientTableToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
              name: "CityId",
              table: "AspNetUsers",
              type: "int",
              nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePhotoUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CityId",
                table: "AspNetUsers",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_cities_CityId",
                table: "AspNetUsers",
                column: "CityId",
                principalTable: "cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_cities_CityId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CityId",
                table: "AspNetUsers");

        

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
            name: "CityId",
            table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProfilePhotoUrl",
                table: "AspNetUsers");
        }
    }
}
