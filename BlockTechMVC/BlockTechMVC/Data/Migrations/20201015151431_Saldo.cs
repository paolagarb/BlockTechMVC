using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlockTechMVC.Data.Migrations
{
    public partial class Saldo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContaCliente",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroConta = table.Column<string>(nullable: true),
                    ApplicationUserId1 = table.Column<string>(nullable: true),
                    ApplicationUserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContaCliente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContaCliente_AspNetUsers_ApplicationUserId1",
                        column: x => x.ApplicationUserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompraCriptomoeda",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(nullable: false),
                    ContaClienteId = table.Column<int>(nullable: false),
                    CriptomoedaHojeId = table.Column<int>(nullable: false),
                    ValorAplicado = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompraCriptomoeda", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompraCriptomoeda_ContaCliente_ContaClienteId",
                        column: x => x.ContaClienteId,
                        principalTable: "ContaCliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompraCriptomoeda_CriptomoedaHoje_CriptomoedaHojeId",
                        column: x => x.CriptomoedaHojeId,
                        principalTable: "CriptomoedaHoje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaldoCriptomoedaHoje",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(nullable: false),
                    CompraCriptomoedaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaldoCriptomoedaHoje", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaldoCriptomoedaHoje_CompraCriptomoeda_CompraCriptomoedaId",
                        column: x => x.CompraCriptomoedaId,
                        principalTable: "CompraCriptomoeda",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompraCriptomoeda_ContaClienteId",
                table: "CompraCriptomoeda",
                column: "ContaClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_CompraCriptomoeda_CriptomoedaHojeId",
                table: "CompraCriptomoeda",
                column: "CriptomoedaHojeId");

            migrationBuilder.CreateIndex(
                name: "IX_ContaCliente_ApplicationUserId1",
                table: "ContaCliente",
                column: "ApplicationUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_SaldoCriptomoedaHoje_CompraCriptomoedaId",
                table: "SaldoCriptomoedaHoje",
                column: "CompraCriptomoedaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SaldoCriptomoedaHoje");

            migrationBuilder.DropTable(
                name: "CompraCriptomoeda");

            migrationBuilder.DropTable(
                name: "ContaCliente");
        }
    }
}
