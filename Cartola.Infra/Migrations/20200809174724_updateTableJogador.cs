using Microsoft.EntityFrameworkCore.Migrations;

namespace Cartola.Infra.Migrations
{
    public partial class updateTableJogador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Jogador_ScoutAtualId_RodadaId",
                table: "Jogador");

            migrationBuilder.AlterColumn<int>(
                name: "ScoutAtualId",
                table: "Jogador",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Jogador_ScoutAtualId_RodadaId",
                table: "Jogador",
                columns: new[] { "ScoutAtualId", "RodadaId" },
                unique: true,
                filter: "[ScoutAtualId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Jogador_ScoutAtualId_RodadaId",
                table: "Jogador");

            migrationBuilder.AlterColumn<int>(
                name: "ScoutAtualId",
                table: "Jogador",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Jogador_ScoutAtualId_RodadaId",
                table: "Jogador",
                columns: new[] { "ScoutAtualId", "RodadaId" },
                unique: true);
        }
    }
}
