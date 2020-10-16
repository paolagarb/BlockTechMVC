using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlockTechMVC.Migrations
{
    public partial class DtCripto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Cadastro",
                table: "Criptomoeda",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cadastro",
                table: "Criptomoeda");
        }
    }
}
