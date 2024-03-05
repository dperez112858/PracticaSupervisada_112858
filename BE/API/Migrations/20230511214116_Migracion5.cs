using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class Migracion5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cliente_Provincia_ProvinciaId",
                table: "Cliente");

            migrationBuilder.DropForeignKey(
                name: "FK_Proveedor_Provincia_ProvinciaId",
                table: "Proveedor");

            migrationBuilder.DropIndex(
                name: "IX_Proveedor_ProvinciaId",
                table: "Proveedor");

            migrationBuilder.DropIndex(
                name: "IX_Cliente_ProvinciaId",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "ProvinciaId",
                table: "Proveedor");

            migrationBuilder.DropColumn(
                name: "ProvinciaId",
                table: "Cliente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProvinciaId",
                table: "Proveedor",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProvinciaId",
                table: "Cliente",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Proveedor_ProvinciaId",
                table: "Proveedor",
                column: "ProvinciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_ProvinciaId",
                table: "Cliente",
                column: "ProvinciaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cliente_Provincia_ProvinciaId",
                table: "Cliente",
                column: "ProvinciaId",
                principalTable: "Provincia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Proveedor_Provincia_ProvinciaId",
                table: "Proveedor",
                column: "ProvinciaId",
                principalTable: "Provincia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
