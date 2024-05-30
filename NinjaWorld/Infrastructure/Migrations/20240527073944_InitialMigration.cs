using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NinjaWorld.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ninja",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    rank = table.Column<int>(type: "integer", nullable: false),
                    village = table.Column<int>(type: "integer", nullable: false),
                    power = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ninja", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tool",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    power = table.Column<int>(type: "integer", nullable: false),
                    ninja_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tool", x => x.id);
                    table.ForeignKey(
                        name: "fk_tool_ninja_ninja_id",
                        column: x => x.ninja_id,
                        principalTable: "ninja",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_tool_ninja_id",
                table: "tool",
                column: "ninja_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tool");

            migrationBuilder.DropTable(
                name: "ninja");
        }
    }
}
