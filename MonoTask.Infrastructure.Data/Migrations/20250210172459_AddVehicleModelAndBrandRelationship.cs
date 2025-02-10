using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonoTask.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddVehicleModelAndBrandRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleModels_VehicleBrands_VehicleBrandId",
                table: "VehicleModels");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "VehicleModels",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "VehicleBrands",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "VehicleBrands",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleModels_VehicleBrands_VehicleBrandId",
                table: "VehicleModels",
                column: "VehicleBrandId",
                principalTable: "VehicleBrands",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleModels_VehicleBrands_VehicleBrandId",
                table: "VehicleModels");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "VehicleModels",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "VehicleBrands",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "VehicleBrands",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleModels_VehicleBrands_VehicleBrandId",
                table: "VehicleModels",
                column: "VehicleBrandId",
                principalTable: "VehicleBrands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
