using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonoTask.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserVehicle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserVehicle",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVehicle", x => new { x.UserId, x.VehicleId });
                    table.ForeignKey(
                        name: "FK_UserVehicle_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserVehicle_VehicleModels_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "VehicleModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserVehicle_VehicleId",
                table: "UserVehicle",
                column: "VehicleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserVehicle");
        }
    }
}
