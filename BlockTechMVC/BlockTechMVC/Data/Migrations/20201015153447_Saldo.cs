using Microsoft.EntityFrameworkCore.Migrations;

namespace BlockTechMVC.Data.Migrations
{
    public partial class Saldo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Saldo",
                table: "SaldoCriptomoedaHoje",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "QuantidadeCriptomoeda",
                table: "CompraCriptomoeda",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Saldo",
                table: "SaldoCriptomoedaHoje");

            migrationBuilder.DropColumn(
                name: "QuantidadeCriptomoeda",
                table: "CompraCriptomoeda");
        }
    }
}
