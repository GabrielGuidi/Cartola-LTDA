using Microsoft.EntityFrameworkCore.Migrations;

namespace Cartola.Infra.Migrations
{
    public partial class updateTablePartida02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClubeVencedorId",
                table: "Partida",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Partida_ClubeVencedorId",
                table: "Partida",
                column: "ClubeVencedorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Partida_Clube_ClubeVencedorId",
                table: "Partida",
                column: "ClubeVencedorId",
                principalTable: "Clube",
                principalColumn: "ClubeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Partida_Clube_ClubeVencedorId",
                table: "Partida");

            migrationBuilder.DropIndex(
                name: "IX_Partida_ClubeVencedorId",
                table: "Partida");

            migrationBuilder.DropColumn(
                name: "ClubeVencedorId",
                table: "Partida");
        }
    }
}
