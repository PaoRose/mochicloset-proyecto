using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mochi_closet.Migrations
{
    /// <inheritdoc />
    public partial class AgregarReportes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reportes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportanteId = table.Column<int>(type: "int", nullable: false),
                    PublicacionId = table.Column<int>(type: "int", nullable: true),
                    ReportadaId = table.Column<int>(type: "int", nullable: true),
                    Razon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaReporte = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reportes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reportes_Publicaciones_PublicacionId",
                        column: x => x.PublicacionId,
                        principalTable: "Publicaciones",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reportes_Usuarios_ReportadaId",
                        column: x => x.ReportadaId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reportes_Usuarios_ReportanteId",
                        column: x => x.ReportanteId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reportes_PublicacionId",
                table: "Reportes",
                column: "PublicacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Reportes_ReportadaId",
                table: "Reportes",
                column: "ReportadaId");

            migrationBuilder.CreateIndex(
                name: "IX_Reportes_ReportanteId",
                table: "Reportes",
                column: "ReportanteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reportes");
        }
    }
}
