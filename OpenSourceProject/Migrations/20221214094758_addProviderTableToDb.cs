using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenSourceProject.Migrations
{
    public partial class addProviderTableToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EngineerName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NextStep",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rate",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegisterationFile",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkShopName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkShopPhotoUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EngineerName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NextStep",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Rate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RegisterationFile",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "WorkShopName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "WorkShopPhotoUrl",
                table: "AspNetUsers");
        }
    }
}
