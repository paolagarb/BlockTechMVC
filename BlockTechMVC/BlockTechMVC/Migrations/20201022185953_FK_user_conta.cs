using Microsoft.EntityFrameworkCore.Migrations;

namespace BlockTechMVC.Migrations
{
    public partial class FK_user_conta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserID",
                table: "ContaCliente",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ContaCliente_AspNetUsers_ApplicationUserId",
                table: "ContaCliente",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
