using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tariqi.Migrations
{
    /// <inheritdoc />
    public partial class AddDatabaseTariqi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_AspNetUsers_DriverId",
                table: "Vehicles");

            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_AreaId",
                table: "Vehicles",
                column: "AreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Areas_AreaId",
                table: "Vehicles",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_AspNetUsers_DriverId",
                table: "Vehicles",
                column: "DriverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Areas_AreaId",
                table: "Vehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_AspNetUsers_DriverId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_AreaId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "Vehicles");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_AspNetUsers_DriverId",
                table: "Vehicles",
                column: "DriverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
