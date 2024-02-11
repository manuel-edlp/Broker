using Microsoft.EntityFrameworkCore.Migrations;

namespace Broker.Migrations
{
    public partial class segunda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "cbu",
                table: "Cuenta",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "numero",
                table: "Banco",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cbu",
                table: "Cuenta");

            migrationBuilder.DropColumn(
                name: "numero",
                table: "Banco");
        }
    }
}
