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
    [Migration("20230516013623_Migracion7")]
    partial class Migracion7
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

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("API.Models.Cobranza", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("FechaLiquidacion")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("Total")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("Cobranza");
                });

            modelBuilder.Entity("API.Models.CobranzaItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CobranzaId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("FechaPago")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("ImporteCobrado")
                        .HasColumnType("double precision");

                    b.Property<double>("ImporteRetenido")
                        .HasColumnType("double precision");

                    b.Property<double>("ImporteTotal")
                        .HasColumnType("double precision");

                    b.Property<string>("NroFacturaAfip")
                        .HasColumnType("text");

                    b.Property<Guid>("TipoPagoId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CobranzaId");

                    b.HasIndex("TipoPagoId");

                    b.ToTable("CobranzaItem");
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

            modelBuilder.Entity("API.Models.Insumo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("CostoUnitario")
                        .HasColumnType("double precision");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("FechaCompra")
                        .HasColumnType("timestamp with time zone");

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

                    b.Property<string>("Campania")
                        .HasColumnType("text");

                    b.Property<Guid>("ClienteId")
                        .HasColumnType("uuid");

                    b.Property<int>("Numero")
                        .HasColumnType("integer");

                    b.Property<double>("Total")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("Presupuesto");
                });

            modelBuilder.Entity("API.Models.Producto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Banio")
                        .HasColumnType("text");

                    b.Property<double>("CostoProducto")
                        .HasColumnType("double precision");

                    b.Property<double>("CostoTotal")
                        .HasColumnType("double precision");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Utilidad")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("Producto");
                });

            modelBuilder.Entity("API.Models.Proveedor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

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
                        .HasForeignKey("LocalidadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CondicionIva");

                    b.Navigation("Localidad");
                });

            modelBuilder.Entity("API.Models.CobranzaItem", b =>
                {
                    b.HasOne("API.Models.Cobranza", null)
                        .WithMany("CobranzaItems")
                        .HasForeignKey("CobranzaId");

                    b.HasOne("API.Models.TipoPago", "TipoPago")
                        .WithMany()
                        .HasForeignKey("TipoPagoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TipoPago");
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
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

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

            modelBuilder.Entity("API.Models.Cobranza", b =>
                {
                    b.Navigation("CobranzaItems");
                });
#pragma warning restore 612, 618
        }
    }
}
