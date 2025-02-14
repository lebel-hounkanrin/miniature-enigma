using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace parc.Migrations
{
    /// <inheritdoc />
    public partial class DeviceNetworkInfo1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceNetworkInfo_DeviceGenralInfos_DeviceId",
                table: "DeviceNetworkInfo");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceNetworkInfo_DeviceGenralInfos_DeviceId",
                table: "DeviceNetworkInfo",
                column: "DeviceId",
                principalTable: "DeviceGenralInfos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceNetworkInfo_DeviceGenralInfos_DeviceId",
                table: "DeviceNetworkInfo");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceNetworkInfo_DeviceGenralInfos_DeviceId",
                table: "DeviceNetworkInfo",
                column: "DeviceId",
                principalTable: "DeviceGenralInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
