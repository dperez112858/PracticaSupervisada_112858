using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class Migracion8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CantidadProductos",
                table: "DetalleProducto");

            migrationBuilder.RenameColumn(
                name: "CostoUnitario",
                table: "Insumo",
                newName: "Precio");

            migrationBuilder.RenameColumn(
                name: "cantidad",
                table: "DetalleProducto",
                newName: "Cantidad");

            migrationBuilder.RenameColumn(
                name: "PrecioUnitario",
                table: "DetalleProducto",
                newName: "Total");

            migrationBuilder.AlterColumn<double>(
                name: "Utilidad",
                table: "Producto",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<double>(
                name: "CostoTotal",
                table: "Producto",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<double>(
                name: "CostoProducto",
                table: "Producto",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AddColumn<double>(
                name: "PrecioInsumo",
                table: "DetalleProducto",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrecioInsumo",
                table: "DetalleProducto");

            migrationBuilder.RenameColumn(
                name: "Precio",
                table: "Insumo",
                newName: "CostoUnitario");

            migrationBuilder.RenameColumn(
                name: "Cantidad",
                table: "DetalleProducto",
                newName: "cantidad");

            migrationBuilder.RenameColumn(
                name: "Total",
                table: "DetalleProducto",
                newName: "PrecioUnitario");

            migrationBuilder.AlterColumn<double>(
                name: "Utilidad",
                table: "Producto",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "CostoTotal",
                table: "Producto",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "CostoProducto",
                table: "Producto",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CantidadProductos",
                table: "DetalleProducto",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
