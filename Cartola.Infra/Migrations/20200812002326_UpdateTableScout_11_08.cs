using Microsoft.EntityFrameworkCore.Migrations;

namespace Cartola.Infra.Migrations
{
    public partial class UpdateTableScout_11_08 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scout_Jogador_JogadorId",
                table: "Scout");

            migrationBuilder.DropForeignKey(
                name: "FK_Scout_Jogador_JogadorId1",
                table: "Scout");

            migrationBuilder.DropIndex(
                name: "IX_Scout_JogadorId1",
                table: "Scout");

            migrationBuilder.DropColumn(
                name: "JogadorId1",
                table: "Scout");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scout_Jogador_JogadorId",
                table: "Scout");

            migrationBuilder.AddColumn<int>(
                name: "JogadorId1",
                table: "Scout",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Scout_JogadorId1",
                table: "Scout",
                column: "JogadorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Scout_Jogador_JogadorId",
                table: "Scout",
                column: "JogadorId",
                principalTable: "Jogador",
                principalColumn: "JogadorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Scout_Jogador_JogadorId1",
                table: "Scout",
                column: "JogadorId1",
                principalTable: "Jogador",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
