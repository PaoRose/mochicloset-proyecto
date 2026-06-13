using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mochi_closet.Migrations
{
    /// <inheritdoc />
    public partial class AgregarFotoPerfilUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FotoPerfil",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Favoritos_PublicacionId",
                table: "Favoritos",
                column: "PublicacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Compras_PublicacionId",
                table: "Compras",
                column: "PublicacionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_Publicaciones_PublicacionId",
                table: "Compras",
                column: "PublicacionId",
                principalTable: "Publicaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Favoritos_Publicaciones_PublicacionId",
                table: "Favoritos",
                column: "PublicacionId",
                principalTable: "Publicaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compras_Publicaciones_PublicacionId",
                table: "Compras");

            migrationBuilder.DropForeignKey(
                name: "FK_Favoritos_Publicaciones_PublicacionId",
                table: "Favoritos");

            migrationBuilder.DropIndex(
                name: "IX_Favoritos_PublicacionId",
                table: "Favoritos");

            migrationBuilder.DropIndex(
                name: "IX_Compras_PublicacionId",
                table: "Compras");

            migrationBuilder.DropColumn(
                name: "FotoPerfil",
                table: "Usuarios");
        }
    }
}
