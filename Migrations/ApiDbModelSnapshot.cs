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

            modelBuilder.Entity("Broker.Models.Banco", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("cuentaId")
                        .HasColumnType("integer");

                    b.Property<int>("estadoId")
                        .HasColumnType("integer");

                    b.Property<string>("razonSocial")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasIndex("cuentaId");

                    b.HasIndex("estadoId");

                    b.ToTable("Banco");
                });

            modelBuilder.Entity("Broker.Models.Cuenta", b =>
                {
                    b.Property<int>("cuentaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("numero")
                        .HasColumnType("bigint");

                    b.HasKey("cuentaId");

                    b.ToTable("Cuenta");
                });

            modelBuilder.Entity("Broker.Models.EstadoBanco", b =>
                {
                    b.Property<int>("estadoBancoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("descripcion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("estadoBancoId");

                    b.ToTable("EstadoBanco");
                });

            modelBuilder.Entity("Broker.Models.EstadoTransaccion", b =>
                {
                    b.Property<int>("estadoTransaccionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("descripcion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("estadoTransaccionId");

                    b.ToTable("EstadoTransaccion");
                });

            modelBuilder.Entity("Broker.Models.TipoTransaccion", b =>
                {
                    b.Property<int>("TipoTransaccionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("descripcion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("TipoTransaccionId");

                    b.ToTable("TipoTransaccion");
                });

            modelBuilder.Entity("Broker.Models.Transaccion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("cuentaDestinoId")
                        .HasColumnType("integer");

                    b.Property<int>("cuentaOrigenId")
                        .HasColumnType("integer");

                    b.Property<int>("estadoId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("fechaHora")
                        .HasColumnType("timestamp without time zone");

                    b.Property<float>("monto")
                        .HasColumnType("real");

                    b.Property<int>("tipoId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("cuentaDestinoId");

                    b.HasIndex("cuentaOrigenId");

                    b.HasIndex("estadoId");

                    b.HasIndex("tipoId");

                    b.ToTable("Transaccion");
                });

            modelBuilder.Entity("Broker.Models.Banco", b =>
                {
                    b.HasOne("Broker.Models.Cuenta", "cuenta")
                        .WithMany()
                        .HasForeignKey("cuentaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Broker.Models.EstadoBanco", "estado")
                        .WithMany()
                        .HasForeignKey("estadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("cuenta");

                    b.Navigation("estado");
                });

            modelBuilder.Entity("Broker.Models.Transaccion", b =>
                {
                    b.HasOne("Broker.Models.Cuenta", "cuentaDestino")
                        .WithMany()
                        .HasForeignKey("cuentaDestinoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Broker.Models.Cuenta", "cuentaOrigen")
                        .WithMany()
                        .HasForeignKey("cuentaOrigenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Broker.Models.EstadoTransaccion", "estado")
                        .WithMany()
                        .HasForeignKey("estadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Broker.Models.TipoTransaccion", "tipo")
                        .WithMany()
                        .HasForeignKey("tipoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("cuentaDestino");

                    b.Navigation("cuentaOrigen");

                    b.Navigation("estado");

                    b.Navigation("tipo");
                });
#pragma warning restore 612, 618
        }
    }
}
