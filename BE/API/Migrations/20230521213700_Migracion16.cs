using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class Migracion16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DetalleProducto_InsumoId",
                table: "DetalleProducto",
                column: "InsumoId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleProducto_Insumo_InsumoId",
                table: "DetalleProducto",
                column: "InsumoId",
                principalTable: "Insumo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalleProducto_Insumo_InsumoId",
                table: "DetalleProducto");

            migrationBuilder.DropIndex(
                name: "IX_DetalleProducto_InsumoId",
                table: "DetalleProducto");
        }
    }
}
