﻿// <auto-generated />
using System;
using Broker.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Broker.Migrations
{
    [DbContext(typeof(ApiDb))]
    partial class ApiDbModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Broker.Models.AceptadoEstado", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("descripcion")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("estado")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.HasKey("id");

                    b.ToTable("AceptadoEstado");
                });

            modelBuilder.Entity("Broker.Models.Banco", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("idBancoEstado")
                        .HasColumnType("integer");

                    b.Property<int>("numero")
                        .HasColumnType("integer");

                    b.Property<string>("razonSocial")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasIndex("idBancoEstado");

                    b.ToTable("Banco");
                });

            modelBuilder.Entity("Broker.Models.BancoEstado", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("descripcion")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.HasKey("id");

                    b.ToTable("BancoEstado");
                });

            modelBuilder.Entity("Broker.Models.Cuenta", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("cbu")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("idBanco")
                        .HasColumnType("integer");

                    b.Property<long>("numero")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.HasIndex("idBanco");

                    b.ToTable("Cuenta");
                });

            modelBuilder.Entity("Broker.Models.RegistroEstado", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("descripcion")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<DateTime>("fechaHora")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("idAceptadoEstado")
                        .HasColumnType("integer");

                    b.Property<int>("idTransaccion")
                        .HasColumnType("integer");

                    b.Property<int>("idValidacionEstado")
                        .HasColumnType("integer");

                    b.HasKey("id");

                    b.HasIndex("idAceptadoEstado");

                    b.HasIndex("idTransaccion");

                    b.HasIndex("idValidacionEstado");

                    b.ToTable("RegistroEstado");
                });

            modelBuilder.Entity("Broker.Models.Tipo", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("descripcion")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.HasKey("id");

                    b.ToTable("Tipo");
                });

            modelBuilder.Entity("Broker.Models.Transaccion", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("fechaHora")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("idAceptadoEstado")
                        .HasColumnType("integer");

                    b.Property<int>("idCuentaDestino")
                        .HasColumnType("integer");

                    b.Property<int>("idCuentaOrigen")
                        .HasColumnType("integer");

                    b.Property<int>("idTipo")
                        .HasColumnType("integer");

                    b.Property<int>("idValidacionEstado")
                        .HasColumnType("integer");

                    b.Property<float>("monto")
                        .HasColumnType("real");

                    b.Property<long>("numero")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.HasIndex("idAceptadoEstado");

                    b.HasIndex("idCuentaDestino");

                    b.HasIndex("idCuentaOrigen");

                    b.HasIndex("idTipo");

                    b.HasIndex("idValidacionEstado");

                    b.ToTable("Transaccion");
                });

            modelBuilder.Entity("Broker.Models.ValidacionEstado", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("descripcion")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("estado")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)");

                    b.HasKey("id");

                    b.ToTable("ValidacionEstado");
                });

            modelBuilder.Entity("Broker.Models.Banco", b =>
                {
                    b.HasOne("Broker.Models.BancoEstado", "estado")
                        .WithMany()
                        .HasForeignKey("idBancoEstado")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("estado");
                });

            modelBuilder.Entity("Broker.Models.Cuenta", b =>
                {
                    b.HasOne("Broker.Models.Banco", "banco")
                        .WithMany()
                        .HasForeignKey("idBanco")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("banco");
                });

            modelBuilder.Entity("Broker.Models.RegistroEstado", b =>
                {
                    b.HasOne("Broker.Models.AceptadoEstado", "aceptadoEstado")
                        .WithMany()
                        .HasForeignKey("idAceptadoEstado")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Broker.Models.Transaccion", "transaccion")
                        .WithMany()
                        .HasForeignKey("idTransaccion")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Broker.Models.ValidacionEstado", "validacionEstado")
                        .WithMany()
                        .HasForeignKey("idValidacionEstado")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("aceptadoEstado");

                    b.Navigation("transaccion");

                    b.Navigation("validacionEstado");
                });

            modelBuilder.Entity("Broker.Models.Transaccion", b =>
                {
                    b.HasOne("Broker.Models.AceptadoEstado", "aceptadoEstado")
                        .WithMany()
                        .HasForeignKey("idAceptadoEstado")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Broker.Models.Cuenta", "cuentaDestino")
                        .WithMany()
                        .HasForeignKey("idCuentaDestino")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Broker.Models.Cuenta", "cuentaOrigen")
                        .WithMany()
                        .HasForeignKey("idCuentaOrigen")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Broker.Models.Tipo", "tipo")
                        .WithMany()
                        .HasForeignKey("idTipo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Broker.Models.ValidacionEstado", "validacionEstado")
                        .WithMany()
                        .HasForeignKey("idValidacionEstado")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("aceptadoEstado");

                    b.Navigation("cuentaDestino");

                    b.Navigation("cuentaOrigen");

                    b.Navigation("tipo");

                    b.Navigation("validacionEstado");
                });
#pragma warning restore 612, 618
        }
    }
}
