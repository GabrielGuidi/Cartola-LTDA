using Microsoft.EntityFrameworkCore.Migrations;

namespace Cartola.Infra.Migrations
{
    public partial class createTablePontuacaoParcial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PontuacaoParcial",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Apelido = table.Column<int>(nullable: false),
                    Pontuacao = table.Column<decimal>(nullable: false),
                    ScoutId = table.Column<int>(nullable: true),
                    JogadorId = table.Column<int>(nullable: false),
                    RodadaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PontuacaoParcial", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PontuacaoParcial_Jogador_JogadorId",
                        column: x => x.JogadorId,
                        principalTable: "Jogador",
                        principalColumn: "JogadorId");
                    table.ForeignKey(
                        name: "FK_PontuacaoParcial_Rodada_RodadaId",
                        column: x => x.RodadaId,
                        principalTable: "Rodada",
                        principalColumn: "RodadaId");
                    table.ForeignKey(
                        name: "FK_PontuacaoParcial_Scout_ScoutId",
                        column: x => x.ScoutId,
                        principalTable: "Scout",
                        principalColumn: "ScoutId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PontuacaoParcial_JogadorId",
                table: "PontuacaoParcial",
                column: "JogadorId");

            migrationBuilder.CreateIndex(
                name: "IX_PontuacaoParcial_RodadaId",
                table: "PontuacaoParcial",
                column: "RodadaId");

            migrationBuilder.CreateIndex(
                name: "IX_PontuacaoParcial_ScoutId",
                table: "PontuacaoParcial",
                column: "ScoutId",
                unique: true,
                filter: "[ScoutId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PontuacaoParcial");
        }
    }
}
