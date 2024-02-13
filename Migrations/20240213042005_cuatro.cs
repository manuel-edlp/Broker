using Microsoft.EntityFrameworkCore.Migrations;

namespace Broker.Migrations
{
    public partial class cuatro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cuenta_Banco_bancoid",
                table: "Cuenta");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaccion_RegistroEstado_idRegistroEstado",
                table: "Transaccion");

            migrationBuilder.DropIndex(
                name: "IX_Transaccion_idRegistroEstado",
                table: "Transaccion");

            migrationBuilder.DropIndex(
                name: "IX_Cuenta_bancoid",
                table: "Cuenta");

            migrationBuilder.DropColumn(
                name: "idRegistroEstado",
                table: "Transaccion");

            migrationBuilder.DropColumn(
                name: "bancoid",
                table: "Cuenta");

            migrationBuilder.AddColumn<int>(
                name: "idTransaccion",
                table: "RegistroEstado",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RegistroEstado_idTransaccion",
                table: "RegistroEstado",
                column: "idTransaccion");

            migrationBuilder.CreateIndex(
                name: "IX_Cuenta_idBanco",
                table: "Cuenta",
                column: "idBanco");

            migrationBuilder.AddForeignKey(
                name: "FK_Cuenta_Banco_idBanco",
                table: "Cuenta",
                column: "idBanco",
                principalTable: "Banco",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RegistroEstado_Transaccion_idTransaccion",
                table: "RegistroEstado",
                column: "idTransaccion",
                principalTable: "Transaccion",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cuenta_Banco_idBanco",
                table: "Cuenta");

            migrationBuilder.DropForeignKey(
                name: "FK_RegistroEstado_Transaccion_idTransaccion",
                table: "RegistroEstado");

            migrationBuilder.DropIndex(
                name: "IX_RegistroEstado_idTransaccion",
                table: "RegistroEstado");

            migrationBuilder.DropIndex(
                name: "IX_Cuenta_idBanco",
                table: "Cuenta");

            migrationBuilder.DropColumn(
                name: "idTransaccion",
                table: "RegistroEstado");

            migrationBuilder.AddColumn<int>(
                name: "idRegistroEstado",
                table: "Transaccion",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "bancoid",
                table: "Cuenta",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transaccion_idRegistroEstado",
                table: "Transaccion",
                column: "idRegistroEstado");

            migrationBuilder.CreateIndex(
                name: "IX_Cuenta_bancoid",
                table: "Cuenta",
                column: "bancoid");

            migrationBuilder.AddForeignKey(
                name: "FK_Cuenta_Banco_bancoid",
                table: "Cuenta",
                column: "bancoid",
                principalTable: "Banco",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaccion_RegistroEstado_idRegistroEstado",
                table: "Transaccion",
                column: "idRegistroEstado",
                principalTable: "RegistroEstado",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
