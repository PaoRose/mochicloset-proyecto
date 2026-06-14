using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mochi_closet.Migrations
{
    /// <inheritdoc />
    public partial class AgregarFavoritos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Conversaciones_CompradoraId",
                table: "Conversaciones",
                column: "CompradoraId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversaciones_PublicacionId",
                table: "Conversaciones",
                column: "PublicacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversaciones_VendedoraId",
                table: "Conversaciones",
                column: "VendedoraId");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversaciones_Publicaciones_PublicacionId",
                table: "Conversaciones",
                column: "PublicacionId",
                principalTable: "Publicaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Conversaciones_Usuarios_CompradoraId",
                table: "Conversaciones",
                column: "CompradoraId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Conversaciones_Usuarios_VendedoraId",
                table: "Conversaciones",
                column: "VendedoraId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversaciones_Publicaciones_PublicacionId",
                table: "Conversaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Conversaciones_Usuarios_CompradoraId",
                table: "Conversaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Conversaciones_Usuarios_VendedoraId",
                table: "Conversaciones");

            migrationBuilder.DropIndex(
                name: "IX_Conversaciones_CompradoraId",
                table: "Conversaciones");

            migrationBuilder.DropIndex(
                name: "IX_Conversaciones_PublicacionId",
                table: "Conversaciones");

            migrationBuilder.DropIndex(
                name: "IX_Conversaciones_VendedoraId",
                table: "Conversaciones");
        }
    }
}
