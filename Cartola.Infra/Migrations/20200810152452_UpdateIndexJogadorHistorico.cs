using Microsoft.EntityFrameworkCore.Migrations;

namespace Cartola.Infra.Migrations
{
    public partial class UpdateIndexJogadorHistorico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_JogadorHistorico_JogadorHistoricoId",
                table: "JogadorHistorico",
                column: "JogadorHistoricoId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_JogadorHistorico_JogadorHistoricoId",
                table: "JogadorHistorico");
        }
    }
}
