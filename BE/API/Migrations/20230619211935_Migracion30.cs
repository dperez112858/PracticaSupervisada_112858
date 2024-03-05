using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class Migracion30 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ClienteId",
                table: "Cobranza",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Cobranza_ClienteId",
                table: "Cobranza",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cobranza_Cliente_ClienteId",
                table: "Cobranza",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cobranza_Cliente_ClienteId",
                table: "Cobranza");

            migrationBuilder.DropIndex(
                name: "IX_Cobranza_ClienteId",
                table: "Cobranza");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Cobranza");
        }
    }
}
