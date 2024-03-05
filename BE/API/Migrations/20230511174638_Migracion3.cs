using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class Migracion3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CobranzaItem_TipoPago_IdTipoPagoNavigationId",
                table: "CobranzaItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Insumo_Proveedor_IdProveedorNavigationId",
                table: "Insumo");

            migrationBuilder.DropForeignKey(
                name: "FK_Presupuesto_Cliente_IdClienteNavigationId",
                table: "Presupuesto");

            migrationBuilder.DropForeignKey(
                name: "FK_Proveedor_CondicionIva_IdCondicionIvaNavigationId",
                table: "Proveedor");

            migrationBuilder.DropForeignKey(
                name: "FK_Proveedor_Localidad_IdlocalidadNavigationId",
                table: "Proveedor");

            migrationBuilder.DropIndex(
                name: "IX_Proveedor_IdCondicionIvaNavigationId",
                table: "Proveedor");

            migrationBuilder.DropColumn(
                name: "IdCondicionIva",
                table: "Proveedor");

            migrationBuilder.DropColumn(
                name: "IdCondicionIvaNavigationId",
                table: "Proveedor");

            migrationBuilder.DropColumn(
                name: "IdCliente",
                table: "Presupuesto");

            migrationBuilder.DropColumn(
                name: "IdProveedor",
                table: "Insumo");

            migrationBuilder.DropColumn(
                name: "IdTipoPago",
                table: "CobranzaItem");

            migrationBuilder.RenameColumn(
                name: "IdlocalidadNavigationId",
                table: "Proveedor",
                newName: "LocalidadId");

            migrationBuilder.RenameColumn(
                name: "IdLocalidad",
                table: "Proveedor",
                newName: "CondicionIvaId");

            migrationBuilder.RenameIndex(
                name: "IX_Proveedor_IdlocalidadNavigationId",
                table: "Proveedor",
                newName: "IX_Proveedor_LocalidadId");

            migrationBuilder.RenameColumn(
                name: "IdClienteNavigationId",
                table: "Presupuesto",
                newName: "ClienteId");

            migrationBuilder.RenameIndex(
                name: "IX_Presupuesto_IdClienteNavigationId",
                table: "Presupuesto",
                newName: "IX_Presupuesto_ClienteId");

            migrationBuilder.RenameColumn(
                name: "IdProveedorNavigationId",
                table: "Insumo",
                newName: "ProveedorId");

            migrationBuilder.RenameIndex(
                name: "IX_Insumo_IdProveedorNavigationId",
                table: "Insumo",
                newName: "IX_Insumo_ProveedorId");

            migrationBuilder.RenameColumn(
                name: "IdTipoPagoNavigationId",
                table: "CobranzaItem",
                newName: "TipoPagoId");

            migrationBuilder.RenameIndex(
                name: "IX_CobranzaItem_IdTipoPagoNavigationId",
                table: "CobranzaItem",
                newName: "IX_CobranzaItem_TipoPagoId");

            migrationBuilder.CreateIndex(
                name: "IX_Proveedor_CondicionIvaId",
                table: "Proveedor",
                column: "CondicionIvaId");

            migrationBuilder.AddForeignKey(
                name: "FK_CobranzaItem_TipoPago_TipoPagoId",
                table: "CobranzaItem",
                column: "TipoPagoId",
                principalTable: "TipoPago",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Insumo_Proveedor_ProveedorId",
                table: "Insumo",
                column: "ProveedorId",
                principalTable: "Proveedor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Presupuesto_Cliente_ClienteId",
                table: "Presupuesto",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Proveedor_CondicionIva_CondicionIvaId",
                table: "Proveedor",
                column: "CondicionIvaId",
                principalTable: "CondicionIva",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Proveedor_Localidad_LocalidadId",
                table: "Proveedor",
                column: "LocalidadId",
                principalTable: "Localidad",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CobranzaItem_TipoPago_TipoPagoId",
                table: "CobranzaItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Insumo_Proveedor_ProveedorId",
                table: "Insumo");

            migrationBuilder.DropForeignKey(
                name: "FK_Presupuesto_Cliente_ClienteId",
                table: "Presupuesto");

            migrationBuilder.DropForeignKey(
                name: "FK_Proveedor_CondicionIva_CondicionIvaId",
                table: "Proveedor");

            migrationBuilder.DropForeignKey(
                name: "FK_Proveedor_Localidad_LocalidadId",
                table: "Proveedor");

            migrationBuilder.DropIndex(
                name: "IX_Proveedor_CondicionIvaId",
                table: "Proveedor");

            migrationBuilder.RenameColumn(
                name: "LocalidadId",
                table: "Proveedor",
                newName: "IdlocalidadNavigationId");

            migrationBuilder.RenameColumn(
                name: "CondicionIvaId",
                table: "Proveedor",
                newName: "IdLocalidad");

            migrationBuilder.RenameIndex(
                name: "IX_Proveedor_LocalidadId",
                table: "Proveedor",
                newName: "IX_Proveedor_IdlocalidadNavigationId");

            migrationBuilder.RenameColumn(
                name: "ClienteId",
                table: "Presupuesto",
                newName: "IdClienteNavigationId");

            migrationBuilder.RenameIndex(
                name: "IX_Presupuesto_ClienteId",
                table: "Presupuesto",
                newName: "IX_Presupuesto_IdClienteNavigationId");

            migrationBuilder.RenameColumn(
                name: "ProveedorId",
                table: "Insumo",
                newName: "IdProveedorNavigationId");

            migrationBuilder.RenameIndex(
                name: "IX_Insumo_ProveedorId",
                table: "Insumo",
                newName: "IX_Insumo_IdProveedorNavigationId");

            migrationBuilder.RenameColumn(
                name: "TipoPagoId",
                table: "CobranzaItem",
                newName: "IdTipoPagoNavigationId");

            migrationBuilder.RenameIndex(
                name: "IX_CobranzaItem_TipoPagoId",
                table: "CobranzaItem",
                newName: "IX_CobranzaItem_IdTipoPagoNavigationId");

            migrationBuilder.AddColumn<Guid>(
                name: "IdCondicionIva",
                table: "Proveedor",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "IdCondicionIvaNavigationId",
                table: "Proveedor",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "IdCliente",
                table: "Presupuesto",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "IdProveedor",
                table: "Insumo",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "IdTipoPago",
                table: "CobranzaItem",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Proveedor_IdCondicionIvaNavigationId",
                table: "Proveedor",
                column: "IdCondicionIvaNavigationId");

            migrationBuilder.AddForeignKey(
                name: "FK_CobranzaItem_TipoPago_IdTipoPagoNavigationId",
                table: "CobranzaItem",
                column: "IdTipoPagoNavigationId",
                principalTable: "TipoPago",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Insumo_Proveedor_IdProveedorNavigationId",
                table: "Insumo",
                column: "IdProveedorNavigationId",
                principalTable: "Proveedor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Presupuesto_Cliente_IdClienteNavigationId",
                table: "Presupuesto",
                column: "IdClienteNavigationId",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Proveedor_CondicionIva_IdCondicionIvaNavigationId",
                table: "Proveedor",
                column: "IdCondicionIvaNavigationId",
                principalTable: "CondicionIva",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Proveedor_Localidad_IdlocalidadNavigationId",
                table: "Proveedor",
                column: "IdlocalidadNavigationId",
                principalTable: "Localidad",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
