using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace parc.Migrations
{
    /// <inheritdoc />
    public partial class updateDeviceGenInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SalleId",
                table: "DeviceGenralInfos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceGenralInfos_SalleId",
                table: "DeviceGenralInfos",
                column: "SalleId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceGenralInfos_Salles_SalleId",
                table: "DeviceGenralInfos",
                column: "SalleId",
                principalTable: "Salles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceGenralInfos_Salles_SalleId",
                table: "DeviceGenralInfos");

            migrationBuilder.DropIndex(
                name: "IX_DeviceGenralInfos_SalleId",
                table: "DeviceGenralInfos");

            migrationBuilder.DropColumn(
                name: "SalleId",
                table: "DeviceGenralInfos");
        }
    }
}
