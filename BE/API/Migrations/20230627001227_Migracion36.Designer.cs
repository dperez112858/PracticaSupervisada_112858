﻿// <auto-generated />
using System;
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20230627001227_Migracion36")]
    partial class Migracion36
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("API.Models.Cliente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Activo")
                        .HasColumnType("boolean");

                    b.Property<string>("Calle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CodigoPostal")
                        .HasColumnType("text");

                    b.Property<string>("Comentario")
                        .HasColumnType("text");

                    b.Property<Guid>("CondicionIvaId")
                        .HasColumnType("uuid");

                    b.Property<string>("Cuit")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<Guid?>("LocalidadId")
                        .HasColumnType("uuid");

                    b.Property<string>("NombreContacto")
                        .HasColumnType("text");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RazonSocial")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Telefono")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CondicionIvaId");

                    b.HasIndex("LocalidadId");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("API.Models.Cobranza", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ClienteId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("FechaLiquidacion")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("Total")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("Cobranza");
                });

            modelBuilder.Entity("API.Models.CondicionIva", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("CondicionIva");
                });

            modelBuilder.Entity("API.Models.DetalleCobranza", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CobranzaId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("FacturaId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("FechaPago")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("ImporteCobrado")
                        .HasColumnType("double precision");

                    b.Property<double>("ImporteRetenido")
                        .HasColumnType("double precision");

                    b.Property<double>("ImporteTotal")
                        .HasColumnType("double precision");

                    b.Property<Guid>("TipoPagoId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("DetalleCobranza");
                });

            modelBuilder.Entity("API.Models.DetallePresupuesto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("CantidadProductos")
                        .HasColumnType("integer");

                    b.Property<double>("PrecioUnitario")
                        .HasColumnType("double precision");

                    b.Property<Guid>("PresupuestoId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProductoId")
                        .HasColumnType("uuid");

                    b.Property<double>("Total")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("ProductoId");

                    b.ToTable("DetallePresupuesto");
                });

            modelBuilder.Entity("API.Models.DetalleProducto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Cantidad")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("FechaCreacion")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("InsumoId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProductoId")
                        .HasColumnType("uuid");

                    b.Property<double>("precioInsumo")
                        .HasColumnType("double precision");

                    b.Property<double>("total")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("DetalleProducto");
                });

            modelBuilder.Entity("API.Models.Factura", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool?>("Cancelada")
                        .HasColumnType("boolean");

                    b.Property<Guid>("ClienteId")
                        .HasColumnType("uuid");

                    b.Property<bool?>("Dolar")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("Iva")
                        .HasColumnType("double precision");

                    b.Property<double>("NetoGravado")
                        .HasColumnType("double precision");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Saldo")
                        .HasColumnType("double precision");

                    b.Property<double?>("TipoCambio")
                        .HasColumnType("double precision");

                    b.Property<string>("TipoComprobante")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Total")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("Factura");
                });

            modelBuilder.Entity("API.Models.Insumo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Activo")
                        .HasColumnType("boolean");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("FechaCompra")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("Precio")
                        .HasColumnType("double precision");

                    b.Property<Guid>("ProveedorId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ProveedorId");

                    b.ToTable("Insumo");
                });

            modelBuilder.Entity("API.Models.Localidad", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ProvinciaId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ProvinciaId");

                    b.ToTable("Localidad");
                });

            modelBuilder.Entity("API.Models.Presupuesto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Aceptado")
                        .HasColumnType("boolean");

                    b.Property<bool>("Activo")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("ClienteId")
                        .HasColumnType("uuid");

                    b.Property<int>("Numero")
                        .HasColumnType("integer");

                    b.Property<double>("Total")
                        .HasColumnType("double precision");

                    b.Property<string>("campania")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("Presupuesto");
                });

            modelBuilder.Entity("API.Models.Producto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("FechaCreacion")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("banioProducto")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("costoProducto")
                        .HasColumnType("double precision");

                    b.Property<double>("costoTotal")
                        .HasColumnType("double precision");

                    b.Property<string>("nombreProducto")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("utilidad")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("Producto");
                });

            modelBuilder.Entity("API.Models.Proveedor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Activo")
                        .HasColumnType("boolean");

                    b.Property<string>("Calle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CodigoPostal")
                        .HasColumnType("text");

                    b.Property<string>("Comentario")
                        .HasColumnType("text");

                    b.Property<Guid>("CondicionIvaId")
                        .HasColumnType("uuid");

                    b.Property<string>("Cuit")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<Guid>("LocalidadId")
                        .HasColumnType("uuid");

                    b.Property<string>("NombreContacto")
                        .HasColumnType("text");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RazonSocial")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Telefono")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CondicionIvaId");

                    b.HasIndex("LocalidadId");

                    b.ToTable("Proveedor");
                });

            modelBuilder.Entity("API.Models.Provincia", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Provincia");
                });

            modelBuilder.Entity("API.Models.TipoPago", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("TipoPago");
                });

            modelBuilder.Entity("API.Models.Cliente", b =>
                {
                    b.HasOne("API.Models.CondicionIva", "CondicionIva")
                        .WithMany()
                        .HasForeignKey("CondicionIvaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.Localidad", "Localidad")
                        .WithMany()
                        .HasForeignKey("LocalidadId");

                    b.Navigation("CondicionIva");

                    b.Navigation("Localidad");
                });

            modelBuilder.Entity("API.Models.Cobranza", b =>
                {
                    b.HasOne("API.Models.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("API.Models.DetallePresupuesto", b =>
                {
                    b.HasOne("API.Models.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producto");
                });

            modelBuilder.Entity("API.Models.Factura", b =>
                {
                    b.HasOne("API.Models.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("API.Models.Insumo", b =>
                {
                    b.HasOne("API.Models.Proveedor", "Proveedor")
                        .WithMany()
                        .HasForeignKey("ProveedorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Proveedor");
                });

            modelBuilder.Entity("API.Models.Localidad", b =>
                {
                    b.HasOne("API.Models.Provincia", "Provincia")
                        .WithMany()
                        .HasForeignKey("ProvinciaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Provincia");
                });

            modelBuilder.Entity("API.Models.Presupuesto", b =>
                {
                    b.HasOne("API.Models.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId");

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("API.Models.Proveedor", b =>
                {
                    b.HasOne("API.Models.CondicionIva", "CondicionIva")
                        .WithMany()
                        .HasForeignKey("CondicionIvaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.Localidad", "Localidad")
                        .WithMany()
                        .HasForeignKey("LocalidadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CondicionIva");

                    b.Navigation("Localidad");
                });
#pragma warning restore 612, 618
        }
    }
}