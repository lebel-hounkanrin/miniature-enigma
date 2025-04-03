using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace parc.Migrations
{
    /// <inheritdoc />
    public partial class addDeviceIdToToTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeviceId",
                table: "Tickets",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "Tickets");
        }
    }
}
