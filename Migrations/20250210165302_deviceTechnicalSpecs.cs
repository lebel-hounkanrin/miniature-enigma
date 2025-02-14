using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace parc.Migrations
{
    /// <inheritdoc />
    public partial class deviceTechnicalSpecs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedAp",
                table: "Devices",
                newName: "UpdatedAt");

            migrationBuilder.CreateTable(
                name: "DeviceTechnicalSpecs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DeviceId = table.Column<int>(type: "integer", nullable: false),
                    OperatingSystem = table.Column<string>(type: "text", nullable: true),
                    Processor = table.Column<string>(type: "text", nullable: true),
                    TotalRamSize = table.Column<int>(type: "integer", nullable: true),
                    TotalStorage = table.Column<string>(type: "text", nullable: true),
                    GraphicsCard = table.Column<string>(type: "text", nullable: true),
                    FreeRamSize = table.Column<int>(type: "integer", nullable: true),
                    FreeStorage = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceTechnicalSpecs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceTechnicalSpecs_DeviceGenralInfos_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "DeviceGenralInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceTechnicalSpecs_DeviceId",
                table: "DeviceTechnicalSpecs",
                column: "DeviceId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceTechnicalSpecs");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Devices",
                newName: "UpdatedAp");
        }
    }
}
