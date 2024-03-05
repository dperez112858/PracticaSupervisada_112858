using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class Migracion29 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CobranzaItem");

            migrationBuilder.AddColumn<bool>(
                name: "Cancelada",
                table: "Factura",
                type: "boolean",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DetalleCobranza",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ImporteCobrado = table.Column<double>(type: "double precision", nullable: false),
                    ImporteRetenido = table.Column<double>(type: "double precision", nullable: false),
                    ImporteTotal = table.Column<double>(type: "double precision", nullable: false),
                    FacturaId = table.Column<Guid>(type: "uuid", nullable: false),
                    FechaPago = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TipoPagoId = table.Column<Guid>(type: "uuid", nullable: false),
                    CobranzaId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleCobranza", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetalleCobranza_Cobranza_CobranzaId",
                        column: x => x.CobranzaId,
                        principalTable: "Cobranza",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleCobranza_Factura_FacturaId",
                        column: x => x.FacturaId,
                        principalTable: "Factura",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleCobranza_TipoPago_TipoPagoId",
                        column: x => x.TipoPagoId,
                        principalTable: "TipoPago",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetalleCobranza_CobranzaId",
                table: "DetalleCobranza",
                column: "CobranzaId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleCobranza_FacturaId",
                table: "DetalleCobranza",
                column: "FacturaId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleCobranza_TipoPagoId",
                table: "DetalleCobranza",
                column: "TipoPagoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalleCobranza");

            migrationBuilder.DropColumn(
                name: "Cancelada",
                table: "Factura");

            migrationBuilder.CreateTable(
                name: "CobranzaItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TipoPagoId = table.Column<Guid>(type: "uuid", nullable: false),
                    CobranzaId = table.Column<Guid>(type: "uuid", nullable: true),
                    FechaPago = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ImporteCobrado = table.Column<double>(type: "double precision", nullable: false),
                    ImporteRetenido = table.Column<double>(type: "double precision", nullable: false),
                    ImporteTotal = table.Column<double>(type: "double precision", nullable: false),
                    NroFacturaAfip = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CobranzaItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CobranzaItem_Cobranza_CobranzaId",
                        column: x => x.CobranzaId,
                        principalTable: "Cobranza",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CobranzaItem_TipoPago_TipoPagoId",
                        column: x => x.TipoPagoId,
                        principalTable: "TipoPago",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CobranzaItem_CobranzaId",
                table: "CobranzaItem",
                column: "CobranzaId");

            migrationBuilder.CreateIndex(
                name: "IX_CobranzaItem_TipoPagoId",
                table: "CobranzaItem",
                column: "TipoPagoId");
        }
    }
}
