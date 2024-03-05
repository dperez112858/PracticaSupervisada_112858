using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class Migracion40 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
