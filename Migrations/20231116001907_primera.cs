using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Broker.Migrations
{
    public partial class primera : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cuenta",
                columns: table => new
                {
                    cuentaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    numero = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuenta", x => x.cuentaId);
                });

            migrationBuilder.CreateTable(
                name: "EstadoBanco",
                columns: table => new
                {
                    estadoBancoId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoBanco", x => x.estadoBancoId);
                });

            migrationBuilder.CreateTable(
                name: "EstadoTransaccion",
                columns: table => new
                {
                    estadoTransaccionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoTransaccion", x => x.estadoTransaccionId);
                });

            migrationBuilder.CreateTable(
                name: "TipoTransaccion",
                columns: table => new
                {
                    TipoTransaccionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoTransaccion", x => x.TipoTransaccionId);
                });

            migrationBuilder.CreateTable(
                name: "Banco",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    razonSocial = table.Column<string>(type: "text", nullable: true),
                    estadoId = table.Column<int>(type: "integer", nullable: false),
                    cuentaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banco", x => x.id);
                    table.ForeignKey(
                        name: "FK_Banco_Cuenta_cuentaId",
                        column: x => x.cuentaId,
                        principalTable: "Cuenta",
                        principalColumn: "cuentaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Banco_EstadoBanco_estadoId",
                        column: x => x.estadoId,
                        principalTable: "EstadoBanco",
                        principalColumn: "estadoBancoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transaccion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fechaHora = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    tipoId = table.Column<int>(type: "integer", nullable: false),
                    estadoId = table.Column<int>(type: "integer", nullable: false),
                    monto = table.Column<float>(type: "real", nullable: false),
                    cuentaOrigenId = table.Column<int>(type: "integer", nullable: false),
                    cuentaDestinoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaccion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaccion_Cuenta_cuentaDestinoId",
                        column: x => x.cuentaDestinoId,
                        principalTable: "Cuenta",
                        principalColumn: "cuentaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transaccion_Cuenta_cuentaOrigenId",
                        column: x => x.cuentaOrigenId,
                        principalTable: "Cuenta",
                        principalColumn: "cuentaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transaccion_EstadoTransaccion_estadoId",
                        column: x => x.estadoId,
                        principalTable: "EstadoTransaccion",
                        principalColumn: "estadoTransaccionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transaccion_TipoTransaccion_tipoId",
                        column: x => x.tipoId,
                        principalTable: "TipoTransaccion",
                        principalColumn: "TipoTransaccionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Banco_cuentaId",
                table: "Banco",
                column: "cuentaId");

            migrationBuilder.CreateIndex(
                name: "IX_Banco_estadoId",
                table: "Banco",
                column: "estadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaccion_cuentaDestinoId",
                table: "Transaccion",
                column: "cuentaDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaccion_cuentaOrigenId",
                table: "Transaccion",
                column: "cuentaOrigenId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaccion_estadoId",
                table: "Transaccion",
                column: "estadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaccion_tipoId",
                table: "Transaccion",
                column: "tipoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Banco");

            migrationBuilder.DropTable(
                name: "Transaccion");

            migrationBuilder.DropTable(
                name: "EstadoBanco");

            migrationBuilder.DropTable(
                name: "Cuenta");

            migrationBuilder.DropTable(
                name: "EstadoTransaccion");

            migrationBuilder.DropTable(
                name: "TipoTransaccion");
        }
    }
}
