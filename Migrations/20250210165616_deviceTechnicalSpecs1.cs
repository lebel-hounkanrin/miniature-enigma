using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace parc.Migrations
{
    /// <inheritdoc />
    public partial class deviceTechnicalSpecs1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceTechnicalSpecs_DeviceGenralInfos_DeviceId",
                table: "DeviceTechnicalSpecs");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceTechnicalSpecs_DeviceGenralInfos_DeviceId",
                table: "DeviceTechnicalSpecs",
                column: "DeviceId",
                principalTable: "DeviceGenralInfos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceTechnicalSpecs_DeviceGenralInfos_DeviceId",
                table: "DeviceTechnicalSpecs");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceTechnicalSpecs_DeviceGenralInfos_DeviceId",
                table: "DeviceTechnicalSpecs",
                column: "DeviceId",
                principalTable: "DeviceGenralInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
