using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenSourceProject.Migrations
{
    /// <inheritdoc />
    public partial class AddClientIdToCarTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "cars",
                type: "nvarchar(450)",
                nullable: true
                
);

            migrationBuilder.CreateIndex(
                name: "IX_cars_ClientId",
                table: "cars",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_cars_AspNetUsers_Id",
                table: "cars",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cars_AspNetUsers_Id",
                table: "cars");

            migrationBuilder.DropIndex(
                name: "IX_cars_ClientId",
                table: "cars");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "cars");
        }
    }
}
