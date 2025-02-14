using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace parc.Migrations
{
    /// <inheritdoc />
    public partial class DeviceVariable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeviceVariables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DeviceId = table.Column<int>(type: "integer", nullable: false),
                    FreeStorage = table.Column<int>(type: "integer", nullable: true),
                    FreeRamSize = table.Column<int>(type: "integer", nullable: true),
                    DiskRead = table.Column<int>(type: "integer", nullable: true),
                    DiskWrite = table.Column<int>(type: "integer", nullable: true),
                    NetSend = table.Column<int>(type: "integer", nullable: true),
                    NetReceive = table.Column<int>(type: "integer", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeviceGenralInfoId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceVariables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceVariables_DeviceGenralInfos_DeviceGenralInfoId",
                        column: x => x.DeviceGenralInfoId,
                        principalTable: "DeviceGenralInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceVariables_DeviceGenralInfoId",
                table: "DeviceVariables",
                column: "DeviceGenralInfoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceVariables");
        }
    }
}
