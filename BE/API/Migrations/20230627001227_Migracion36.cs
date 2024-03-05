using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class Migracion36 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalleCobranza_Cobranza_CobranzaId",
                table: "DetalleCobranza");

            migrationBuilder.DropForeignKey(
                name: "FK_DetalleProducto_Producto_ProductoId",
                table: "DetalleProducto");

            migrationBuilder.DropForeignKey(
                name: "FK_Presupuesto_Cliente_ClienteId",
                table: "Presupuesto");

            // migrationBuilder.DropIndex(
            //     name: "IX_DetalleProducto_ProductoId",
            //     table: "DetalleProducto");

            migrationBuilder.DropIndex(
                name: "IX_DetalleCobranza_CobranzaId",
                table: "DetalleCobranza");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClienteId",
                table: "Presupuesto",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateIndex(
                name: "IX_DetallePresupuesto_ProductoId",
                table: "DetallePresupuesto",
                column: "ProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallePresupuesto_Producto_ProductoId",
                table: "DetallePresupuesto",
                column: "ProductoId",
                principalTable: "Producto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Presupuesto_Cliente_ClienteId",
                table: "Presupuesto",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallePresupuesto_Producto_ProductoId",
                table: "DetallePresupuesto");

            migrationBuilder.DropForeignKey(
                name: "FK_Presupuesto_Cliente_ClienteId",
                table: "Presupuesto");

            migrationBuilder.DropIndex(
                name: "IX_DetallePresupuesto_ProductoId",
                table: "DetallePresupuesto");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClienteId",
                table: "Presupuesto",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DetalleProducto_ProductoId",
                table: "DetalleProducto",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleCobranza_CobranzaId",
                table: "DetalleCobranza",
                column: "CobranzaId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleCobranza_Cobranza_CobranzaId",
                table: "DetalleCobranza",
                column: "CobranzaId",
                principalTable: "Cobranza",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleProducto_Producto_ProductoId",
                table: "DetalleProducto",
                column: "ProductoId",
                principalTable: "Producto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Presupuesto_Cliente_ClienteId",
                table: "Presupuesto",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
