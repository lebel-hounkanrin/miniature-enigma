using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace parc.Migrations
{
    /// <inheritdoc />
    public partial class DeviceVariable1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "DeviceVariables");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeviceId",
                table: "DeviceVariables",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
