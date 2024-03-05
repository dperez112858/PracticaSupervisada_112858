using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class Migracion7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Insumo_Producto_ProductoId",
                table: "Insumo");

            migrationBuilder.DropForeignKey(
                name: "FK_Producto_Presupuesto_PresupuestoId",
                table: "Producto");

            migrationBuilder.DropIndex(
                name: "IX_Producto_PresupuestoId",
                table: "Producto");

            migrationBuilder.DropIndex(
                name: "IX_Insumo_ProductoId",
                table: "Insumo");

            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "PresupuestoId",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "ProductoId",
                table: "Insumo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Cantidad",
                table: "Producto",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<Guid>(
                name: "PresupuestoId",
                table: "Producto",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductoId",
                table: "Insumo",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Producto_PresupuestoId",
                table: "Producto",
                column: "PresupuestoId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Producto_Presupuesto_PresupuestoId",
                table: "Producto",
                column: "PresupuestoId",
                principalTable: "Presupuesto",
                principalColumn: "Id");
        }
    }
}
