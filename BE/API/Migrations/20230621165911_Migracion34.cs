using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class Migracion34 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Saldo",
                table: "Factura",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_Presupuesto_ClienteId",
                table: "Presupuesto",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Presupuesto_Cliente_ClienteId",
                table: "Presupuesto",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Presupuesto_Cliente_ClienteId",
                table: "Presupuesto");

            migrationBuilder.DropIndex(
                name: "IX_Presupuesto_ClienteId",
                table: "Presupuesto");

            migrationBuilder.DropColumn(
                name: "Saldo",
                table: "Factura");
        }
    }
}
