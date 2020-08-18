using Microsoft.EntityFrameworkCore.Migrations;

namespace Cartola.Infra.Migrations
{
    public partial class CreateIndexJogadorHistorico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_JogadorHistorico_JogadorId",
                table: "JogadorHistorico");

            migrationBuilder.CreateIndex(
                name: "IX_JogadorHistorico_JogadorId_RodadaId",
                table: "JogadorHistorico",
                columns: new[] { "JogadorId", "RodadaId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_JogadorHistorico_JogadorId_RodadaId",
                table: "JogadorHistorico");

            migrationBuilder.CreateIndex(
                name: "IX_JogadorHistorico_JogadorId",
                table: "JogadorHistorico",
                column: "JogadorId");
        }
    }
}
