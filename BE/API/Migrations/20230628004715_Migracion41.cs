using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class Migracion41 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Insumo_Producto_ProductoId",
                table: "Insumo");

            migrationBuilder.DropIndex(
                name: "IX_Insumo_ProductoId",
                table: "Insumo");

            migrationBuilder.DropColumn(
                name: "ProductoId",
                table: "Insumo");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleProducto_ProductoId",
                table: "DetalleProducto",
                column: "ProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleProducto_Producto_ProductoId",
                table: "DetalleProducto",
                column: "ProductoId",
                principalTable: "Producto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalleProducto_Producto_ProductoId",
                table: "DetalleProducto");

            migrationBuilder.DropIndex(
                name: "IX_DetalleProducto_ProductoId",
                table: "DetalleProducto");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductoId",
                table: "Insumo",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Insumo_ProductoId",
                table: "Insumo",
                column: "ProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Insumo_Producto_ProductoId",
                table: "Insumo",
                column: "ProductoId",
                principalTable: "Producto",
                principalColumn: "Id");
        }
    }
}
