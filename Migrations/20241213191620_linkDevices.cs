using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace parc.Migrations
{
    /// <inheritdoc />
    public partial class linkDevices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Devices_SalleId",
                table: "Devices",
                column: "SalleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Salles_SalleId",
                table: "Devices",
                column: "SalleId",
                principalTable: "Salles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Salles_SalleId",
                table: "Devices");

            migrationBuilder.DropIndex(
                name: "IX_Devices_SalleId",
                table: "Devices");
        }
    }
}
