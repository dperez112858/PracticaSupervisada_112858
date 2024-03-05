using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class Migracion32 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalleCobranza_Factura_FacturaId",
                table: "DetalleCobranza");

            migrationBuilder.DropForeignKey(
                name: "FK_DetalleCobranza_TipoPago_TipoPagoId",
                table: "DetalleCobranza");

            migrationBuilder.DropForeignKey(
                name: "FK_DetalleProducto_Insumo_InsumoId",
                table: "DetalleProducto");

            migrationBuilder.DropIndex(
                name: "IX_DetalleProducto_InsumoId",
                table: "DetalleProducto");

            migrationBuilder.DropIndex(
                name: "IX_DetalleCobranza_FacturaId",
                table: "DetalleCobranza");

            migrationBuilder.DropIndex(
                name: "IX_DetalleCobranza_TipoPagoId",
                table: "DetalleCobranza");

            migrationBuilder.DropColumn(
                name: "Banio",
                table: "Producto");

            migrationBuilder.RenameColumn(
                name: "Utilidad",
                table: "Producto",
                newName: "utilidad");

            migrationBuilder.RenameColumn(
                name: "CostoTotal",
                table: "Producto",
                newName: "costoTotal");

            migrationBuilder.RenameColumn(
                name: "CostoProducto",
                table: "Producto",
                newName: "costoProducto");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Producto",
                newName: "nombreProducto");

            migrationBuilder.RenameColumn(
                name: "Total",
                table: "DetalleProducto",
                newName: "total");

            migrationBuilder.RenameColumn(
                name: "PrecioInsumo",
                table: "DetalleProducto",
                newName: "precioInsumo");

            migrationBuilder.AddColumn<string>(
                name: "banioProducto",
                table: "Producto",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "banioProducto",
                table: "Producto");

            migrationBuilder.RenameColumn(
                name: "utilidad",
                table: "Producto",
                newName: "Utilidad");

            migrationBuilder.RenameColumn(
                name: "costoTotal",
                table: "Producto",
                newName: "CostoTotal");

            migrationBuilder.RenameColumn(
                name: "costoProducto",
                table: "Producto",
                newName: "CostoProducto");

            migrationBuilder.RenameColumn(
                name: "nombreProducto",
                table: "Producto",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "total",
                table: "DetalleProducto",
                newName: "Total");

            migrationBuilder.RenameColumn(
                name: "precioInsumo",
                table: "DetalleProducto",
                newName: "PrecioInsumo");

            migrationBuilder.AddColumn<string>(
                name: "Banio",
                table: "Producto",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DetalleProducto_InsumoId",
                table: "DetalleProducto",
                column: "InsumoId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleCobranza_FacturaId",
                table: "DetalleCobranza",
                column: "FacturaId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleCobranza_TipoPagoId",
                table: "DetalleCobranza",
                column: "TipoPagoId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleCobranza_Factura_FacturaId",
                table: "DetalleCobranza",
                column: "FacturaId",
                principalTable: "Factura",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleCobranza_TipoPago_TipoPagoId",
                table: "DetalleCobranza",
                column: "TipoPagoId",
                principalTable: "TipoPago",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleProducto_Insumo_InsumoId",
                table: "DetalleProducto",
                column: "InsumoId",
                principalTable: "Insumo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
