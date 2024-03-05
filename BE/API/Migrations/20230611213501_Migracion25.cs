using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class Migracion25 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Moneda",
                table: "Factura");

            migrationBuilder.AddColumn<bool>(
                name: "Dolar",
                table: "Factura",
                type: "boolean",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dolar",
                table: "Factura");

            migrationBuilder.AddColumn<int>(
                name: "Moneda",
                table: "Factura",
                type: "integer",
                nullable: true);
        }
    }
}
