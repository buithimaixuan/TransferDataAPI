using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerA.Migrations
{
    public partial class InitialDatabaseMigrationServerA : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Facilities",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facilities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Residents",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    first_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    last_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dob = table.Column<DateTime>(type: "datetime2", nullable: true),
                    facility_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Residents", x => x.id);
                    table.ForeignKey(
                        name: "FK_Residents_Facilities_facility_id",
                        column: x => x.facility_id,
                        principalTable: "Facilities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgressNotes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    resident_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgressNotes", x => x.id);
                    table.ForeignKey(
                        name: "FK_ProgressNotes_Residents_resident_id",
                        column: x => x.resident_id,
                        principalTable: "Residents",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProgressNotes_resident_id",
                table: "ProgressNotes",
                column: "resident_id");

            migrationBuilder.CreateIndex(
                name: "IX_Residents_facility_id",
                table: "Residents",
                column: "facility_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProgressNotes");

            migrationBuilder.DropTable(
                name: "Residents");

            migrationBuilder.DropTable(
                name: "Facilities");
        }
    }
}
