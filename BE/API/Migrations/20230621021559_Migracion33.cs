using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class Migracion33 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Presupuesto_Cliente_ClienteId",
                table: "Presupuesto");

            migrationBuilder.DropIndex(
                name: "IX_Presupuesto_ClienteId",
                table: "Presupuesto");

            migrationBuilder.RenameColumn(
                name: "Campania",
                table: "Presupuesto",
                newName: "campania");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "campania",
                table: "Presupuesto",
                newName: "Campania");

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
    }
}
