using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace parc.Migrations
{
    /// <inheritdoc />
    public partial class deviceGeneralInfos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedAp",
                table: "DeviceGenralInfos",
                newName: "UpdatedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "DeviceGenralInfos",
                newName: "UpdatedAp");
        }
    }
}
