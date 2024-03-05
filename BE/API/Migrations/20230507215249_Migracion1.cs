using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class Migracion1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cobranza",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FechaLiquidacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Total = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cobranza", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CondicionIva",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CondicionIva", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Provincia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provincia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoPago",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoPago", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Localidad",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    ProvinciaId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localidad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Localidad_Provincia_ProvinciaId",
                        column: x => x.ProvinciaId,
                        principalTable: "Provincia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CobranzaItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ImporteCobrado = table.Column<double>(type: "double precision", nullable: false),
                    ImporteRetenido = table.Column<double>(type: "double precision", nullable: false),
                    ImporteTotal = table.Column<double>(type: "double precision", nullable: false),
                    NroFacturaAfip = table.Column<string>(type: "text", nullable: true),
                    FechaPago = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IdTipoPagoNavigationId = table.Column<Guid>(type: "uuid", nullable: false),
                    IdTipoPago = table.Column<Guid>(type: "uuid", nullable: false),
                    CobranzaId = table.Column<Guid>(type: "uuid", nullable: true)
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
                        name: "FK_CobranzaItem_TipoPago_IdTipoPagoNavigationId",
                        column: x => x.IdTipoPagoNavigationId,
                        principalTable: "TipoPago",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RazonSocial = table.Column<string>(type: "text", nullable: false),
                    Cuit = table.Column<string>(type: "text", nullable: false),
                    Calle = table.Column<string>(type: "text", nullable: false),
                    Numero = table.Column<string>(type: "text", nullable: false),
                    CodigoPostal = table.Column<string>(type: "text", nullable: true),
                    Telefono = table.Column<string>(type: "text", nullable: true),
                    NombreContacto = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Comentario = table.Column<string>(type: "text", nullable: true),
                    LocalidadId = table.Column<Guid>(type: "uuid", nullable: false),
                    CondicionIvaId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cliente_CondicionIva_CondicionIvaId",
                        column: x => x.CondicionIvaId,
                        principalTable: "CondicionIva",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cliente_Localidad_LocalidadId",
                        column: x => x.LocalidadId,
                        principalTable: "Localidad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Proveedor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RazonSocial = table.Column<string>(type: "text", nullable: false),
                    Cuit = table.Column<string>(type: "text", nullable: false),
                    Calle = table.Column<string>(type: "text", nullable: false),
                    Numero = table.Column<string>(type: "text", nullable: false),
                    CodigoPostal = table.Column<string>(type: "text", nullable: true),
                    Telefono = table.Column<string>(type: "text", nullable: true),
                    NombreContacto = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Comentario = table.Column<string>(type: "text", nullable: true),
                    IdLocalidad = table.Column<Guid>(type: "uuid", nullable: false),
                    IdCondicionIva = table.Column<Guid>(type: "uuid", nullable: false),
                    IdlocalidadNavigationId = table.Column<Guid>(type: "uuid", nullable: false),
                    IdCondicionIvaNavigationId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proveedor_CondicionIva_IdCondicionIvaNavigationId",
                        column: x => x.IdCondicionIvaNavigationId,
                        principalTable: "CondicionIva",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Proveedor_Localidad_IdlocalidadNavigationId",
                        column: x => x.IdlocalidadNavigationId,
                        principalTable: "Localidad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Presupuesto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Numero = table.Column<int>(type: "integer", nullable: false),
                    Campania = table.Column<string>(type: "text", nullable: true),
                    Total = table.Column<double>(type: "double precision", nullable: false),
                    IdCliente = table.Column<Guid>(type: "uuid", nullable: false),
                    IdClienteNavigationId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Presupuesto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Presupuesto_Cliente_IdClienteNavigationId",
                        column: x => x.IdClienteNavigationId,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Banio = table.Column<string>(type: "text", nullable: true),
                    Cantidad = table.Column<double>(type: "double precision", nullable: false),
                    CostoProducto = table.Column<double>(type: "double precision", nullable: false),
                    Utilidad = table.Column<double>(type: "double precision", nullable: false),
                    CostoTotal = table.Column<double>(type: "double precision", nullable: false),
                    PresupuestoId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Producto_Presupuesto_PresupuestoId",
                        column: x => x.PresupuestoId,
                        principalTable: "Presupuesto",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Insumo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    CostoUnitario = table.Column<double>(type: "double precision", nullable: false),
                    FechaCompra = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IdProveedorNavigationId = table.Column<Guid>(type: "uuid", nullable: false),
                    IdProveedor = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductoId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insumo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Insumo_Producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Insumo_Proveedor_IdProveedorNavigationId",
                        column: x => x.IdProveedorNavigationId,
                        principalTable: "Proveedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_CondicionIvaId",
                table: "Cliente",
                column: "CondicionIvaId");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_LocalidadId",
                table: "Cliente",
                column: "LocalidadId");

            migrationBuilder.CreateIndex(
                name: "IX_CobranzaItem_CobranzaId",
                table: "CobranzaItem",
                column: "CobranzaId");

            migrationBuilder.CreateIndex(
                name: "IX_CobranzaItem_IdTipoPagoNavigationId",
                table: "CobranzaItem",
                column: "IdTipoPagoNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_Insumo_IdProveedorNavigationId",
                table: "Insumo",
                column: "IdProveedorNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_Insumo_ProductoId",
                table: "Insumo",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Localidad_ProvinciaId",
                table: "Localidad",
                column: "ProvinciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Presupuesto_IdClienteNavigationId",
                table: "Presupuesto",
                column: "IdClienteNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_PresupuestoId",
                table: "Producto",
                column: "PresupuestoId");

            migrationBuilder.CreateIndex(
                name: "IX_Proveedor_IdCondicionIvaNavigationId",
                table: "Proveedor",
                column: "IdCondicionIvaNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_Proveedor_IdlocalidadNavigationId",
                table: "Proveedor",
                column: "IdlocalidadNavigationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CobranzaItem");

            migrationBuilder.DropTable(
                name: "Insumo");

            migrationBuilder.DropTable(
                name: "Cobranza");

            migrationBuilder.DropTable(
                name: "TipoPago");

            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "Proveedor");

            migrationBuilder.DropTable(
                name: "Presupuesto");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "CondicionIva");

            migrationBuilder.DropTable(
                name: "Localidad");

            migrationBuilder.DropTable(
                name: "Provincia");
        }
    }
}
