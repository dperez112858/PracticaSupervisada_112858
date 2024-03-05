using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class Migracion35 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallePresupuesto_Presupuesto_PresupuestoId",
                table: "DetallePresupuesto");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallePresupuesto_Producto_ProductoId",
                table: "DetallePresupuesto");

            migrationBuilder.DropIndex(
                name: "IX_DetallePresupuesto_PresupuestoId",
                table: "DetallePresupuesto");

            migrationBuilder.DropIndex(
                name: "IX_DetallePresupuesto_ProductoId",
                table: "DetallePresupuesto");

            migrationBuilder.AddColumn<double>(
                name: "Total",
                table: "DetallePresupuesto",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total",
                table: "DetallePresupuesto");

            migrationBuilder.CreateIndex(
                name: "IX_DetallePresupuesto_PresupuestoId",
                table: "DetallePresupuesto",
                column: "PresupuestoId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallePresupuesto_ProductoId",
                table: "DetallePresupuesto",
                column: "ProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallePresupuesto_Presupuesto_PresupuestoId",
                table: "DetallePresupuesto",
                column: "PresupuestoId",
                principalTable: "Presupuesto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallePresupuesto_Producto_ProductoId",
                table: "DetallePresupuesto",
                column: "ProductoId",
                principalTable: "Producto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
