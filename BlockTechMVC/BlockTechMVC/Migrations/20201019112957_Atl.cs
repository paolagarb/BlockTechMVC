using Microsoft.EntityFrameworkCore.Migrations;

namespace BlockTechMVC.Migrations
{
    public partial class Atl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NomeDestinatario",
                table: "Conta");

            migrationBuilder.AlterColumn<decimal>(
                name: "quantidadeCripo",
                table: "Saldo",
                type: "decimal(20,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "SaldoAtualRS",
                table: "Saldo",
                type: "decimal(20,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "Valor",
                table: "CriptomoedaHoje",
                type: "decimal(20,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "quantidadeCripo",
                table: "Saldo",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,2)");

            migrationBuilder.AlterColumn<double>(
                name: "SaldoAtualRS",
                table: "Saldo",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,2)");

            migrationBuilder.AlterColumn<double>(
                name: "Valor",
                table: "CriptomoedaHoje",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,2)");

            migrationBuilder.AddColumn<string>(
                name: "NomeDestinatario",
                table: "Conta",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
