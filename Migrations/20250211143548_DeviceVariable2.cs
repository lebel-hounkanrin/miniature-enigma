using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace parc.Migrations
{
    /// <inheritdoc />
    public partial class DeviceVariable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceNetworkInfo_DeviceGenralInfos_DeviceId",
                table: "DeviceNetworkInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_DeviceTechnicalSpecs_DeviceGenralInfos_DeviceId",
                table: "DeviceTechnicalSpecs");

            migrationBuilder.DropForeignKey(
                name: "FK_DeviceVariables_DeviceGenralInfos_DeviceGenralInfoId",
                table: "DeviceVariables");

            migrationBuilder.DropIndex(
                name: "IX_DeviceVariables_DeviceGenralInfoId",
                table: "DeviceVariables");

            migrationBuilder.DropColumn(
                name: "DeviceGenralInfoId",
                table: "DeviceVariables");

            migrationBuilder.AddColumn<int>(
                name: "DeviceId",
                table: "DeviceVariables",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceVariables_DeviceId",
                table: "DeviceVariables",
                column: "DeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceNetworkInfo_DeviceGenralInfos_DeviceId",
                table: "DeviceNetworkInfo",
                column: "DeviceId",
                principalTable: "DeviceGenralInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceTechnicalSpecs_DeviceGenralInfos_DeviceId",
                table: "DeviceTechnicalSpecs",
                column: "DeviceId",
                principalTable: "DeviceGenralInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceVariables_DeviceGenralInfos_DeviceId",
                table: "DeviceVariables",
                column: "DeviceId",
                principalTable: "DeviceGenralInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceNetworkInfo_DeviceGenralInfos_DeviceId",
                table: "DeviceNetworkInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_DeviceTechnicalSpecs_DeviceGenralInfos_DeviceId",
                table: "DeviceTechnicalSpecs");

            migrationBuilder.DropForeignKey(
                name: "FK_DeviceVariables_DeviceGenralInfos_DeviceId",
                table: "DeviceVariables");

            migrationBuilder.DropIndex(
                name: "IX_DeviceVariables_DeviceId",
                table: "DeviceVariables");

            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "DeviceVariables");

            migrationBuilder.AddColumn<int>(
                name: "DeviceGenralInfoId",
                table: "DeviceVariables",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceVariables_DeviceGenralInfoId",
                table: "DeviceVariables",
                column: "DeviceGenralInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceNetworkInfo_DeviceGenralInfos_DeviceId",
                table: "DeviceNetworkInfo",
                column: "DeviceId",
                principalTable: "DeviceGenralInfos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceTechnicalSpecs_DeviceGenralInfos_DeviceId",
                table: "DeviceTechnicalSpecs",
                column: "DeviceId",
                principalTable: "DeviceGenralInfos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceVariables_DeviceGenralInfos_DeviceGenralInfoId",
                table: "DeviceVariables",
                column: "DeviceGenralInfoId",
                principalTable: "DeviceGenralInfos",
                principalColumn: "Id");
        }
    }
}
