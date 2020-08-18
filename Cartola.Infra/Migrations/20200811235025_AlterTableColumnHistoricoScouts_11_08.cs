using Microsoft.EntityFrameworkCore.Migrations;

namespace Cartola.Infra.Migrations
{
    public partial class AlterTableColumnHistoricoScouts_11_08 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jogador_Scout_ScoutAtualId_RodadaId",
                table: "Jogador");

            migrationBuilder.DropForeignKey(
                name: "FK_Partida_Clube_ClubeCasaId",
                table: "Partida");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Scout_ScoutId_RodadaId",
                table: "Scout");

            migrationBuilder.DropIndex(
                name: "IX_Jogador_ScoutAtualId_RodadaId",
                table: "Jogador");

            migrationBuilder.AddColumn<int>(
                name: "JogadorId1",
                table: "Scout",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Scout_JogadorId1",
                table: "Scout",
                column: "JogadorId1");

            migrationBuilder.CreateIndex(
                name: "IX_Jogador_ScoutAtualId",
                table: "Jogador",
                column: "ScoutAtualId",
                unique: true,
                filter: "[ScoutAtualId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Jogador_Scout_ScoutAtualId",
                table: "Jogador",
                column: "ScoutAtualId",
                principalTable: "Scout",
                principalColumn: "ScoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_Partida_Clube_ClubeCasaId",
                table: "Partida",
                column: "ClubeCasaId",
                principalTable: "Clube",
                principalColumn: "ClubeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Scout_Jogador_JogadorId1",
                table: "Scout",
                column: "JogadorId1",
                principalTable: "Jogador",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jogador_Scout_ScoutAtualId",
                table: "Jogador");

            migrationBuilder.DropForeignKey(
                name: "FK_Partida_Clube_ClubeCasaId",
                table: "Partida");

            migrationBuilder.DropForeignKey(
                name: "FK_Scout_Jogador_JogadorId1",
                table: "Scout");

            migrationBuilder.DropIndex(
                name: "IX_Scout_JogadorId1",
                table: "Scout");

            migrationBuilder.DropIndex(
                name: "IX_Jogador_ScoutAtualId",
                table: "Jogador");

            migrationBuilder.DropColumn(
                name: "JogadorId1",
                table: "Scout");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Scout_ScoutId_RodadaId",
                table: "Scout",
                columns: new[] { "ScoutId", "RodadaId" });

            migrationBuilder.CreateIndex(
                name: "IX_Jogador_ScoutAtualId_RodadaId",
                table: "Jogador",
                columns: new[] { "ScoutAtualId", "RodadaId" },
                unique: true,
                filter: "[ScoutAtualId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Jogador_Scout_ScoutAtualId_RodadaId",
                table: "Jogador",
                columns: new[] { "ScoutAtualId", "RodadaId" },
                principalTable: "Scout",
                principalColumns: new[] { "ScoutId", "RodadaId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Partida_Clube_ClubeCasaId",
                table: "Partida",
                column: "ClubeCasaId",
                principalTable: "Clube",
                principalColumn: "ClubeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
