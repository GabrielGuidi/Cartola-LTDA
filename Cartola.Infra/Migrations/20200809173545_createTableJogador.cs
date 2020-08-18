using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cartola.Infra.Migrations
{
    public partial class createTableJogador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_Status_StatusId",
                table: "Status",
                column: "StatusId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Rodada_RodadaId",
                table: "Rodada",
                column: "RodadaId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Posicao_PosicaoId",
                table: "Posicao",
                column: "PosicaoId");

            migrationBuilder.CreateTable(
                name: "JogadorHistorico",
                columns: table => new
                {
                    JogadorHistoricoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PontosNum = table.Column<decimal>(nullable: false),
                    PrecoNum = table.Column<decimal>(nullable: false),
                    VariacaoNum = table.Column<decimal>(nullable: false),
                    MediaNum = table.Column<decimal>(nullable: false),
                    JogosNum = table.Column<int>(nullable: false),
                    DataModificacao = table.Column<DateTime>(nullable: false),
                    JogadorId = table.Column<int>(nullable: false),
                    RodadaId = table.Column<int>(nullable: false),
                    ClubeId = table.Column<int>(nullable: false),
                    PosicaoId = table.Column<int>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    ScoutId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JogadorHistorico", x => x.JogadorHistoricoId);
                    table.ForeignKey(
                        name: "FK_JogadorHistorico_Clube_ClubeId",
                        column: x => x.ClubeId,
                        principalTable: "Clube",
                        principalColumn: "ClubeId");
                    table.ForeignKey(
                        name: "FK_JogadorHistorico_Posicao_PosicaoId",
                        column: x => x.PosicaoId,
                        principalTable: "Posicao",
                        principalColumn: "PosicaoId");
                    table.ForeignKey(
                        name: "FK_JogadorHistorico_Rodada_RodadaId",
                        column: x => x.RodadaId,
                        principalTable: "Rodada",
                        principalColumn: "RodadaId");
                    table.ForeignKey(
                        name: "FK_JogadorHistorico_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "StatusId");
                });

            migrationBuilder.CreateTable(
                name: "Scout",
                columns: table => new
                {
                    ScoutId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataModificacao = table.Column<DateTime>(nullable: false),
                    JogadorId = table.Column<int>(nullable: false),
                    RodadaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scout", x => x.ScoutId);
                    table.UniqueConstraint("AK_Scout_ScoutId_RodadaId", x => new { x.ScoutId, x.RodadaId });
                    table.ForeignKey(
                        name: "FK_Scout_Rodada_RodadaId",
                        column: x => x.RodadaId,
                        principalTable: "Rodada",
                        principalColumn: "RodadaId");
                });

            migrationBuilder.CreateTable(
                name: "Jogador",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JogadorId = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(maxLength: 256, nullable: true),
                    Slug = table.Column<string>(maxLength: 64, nullable: true),
                    Apelido = table.Column<string>(maxLength: 64, nullable: true),
                    Foto = table.Column<string>(maxLength: 256, nullable: true),
                    PontosNum = table.Column<decimal>(nullable: false),
                    PrecoNum = table.Column<decimal>(nullable: false),
                    VariacaoNum = table.Column<decimal>(nullable: false),
                    MediaNum = table.Column<decimal>(nullable: false),
                    JogosNum = table.Column<int>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    DataModificacao = table.Column<DateTime>(nullable: false),
                    RodadaId = table.Column<int>(nullable: false),
                    ClubeId = table.Column<int>(nullable: false),
                    PosicaoId = table.Column<int>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    ScoutAtualId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jogador", x => x.Id);
                    table.UniqueConstraint("AK_Jogador_JogadorId", x => x.JogadorId);
                    table.ForeignKey(
                        name: "FK_Jogador_Clube_ClubeId",
                        column: x => x.ClubeId,
                        principalTable: "Clube",
                        principalColumn: "ClubeId");
                    table.ForeignKey(
                        name: "FK_Jogador_Posicao_PosicaoId",
                        column: x => x.PosicaoId,
                        principalTable: "Posicao",
                        principalColumn: "PosicaoId");
                    table.ForeignKey(
                        name: "FK_Jogador_Rodada_RodadaId",
                        column: x => x.RodadaId,
                        principalTable: "Rodada",
                        principalColumn: "RodadaId");
                    table.ForeignKey(
                        name: "FK_Jogador_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "StatusId");
                    table.ForeignKey(
                        name: "FK_Jogador_Scout_ScoutAtualId_RodadaId",
                        columns: x => new { x.ScoutAtualId, x.RodadaId },
                        principalTable: "Scout",
                        principalColumns: new[] { "ScoutId", "RodadaId" });
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jogador_ClubeId",
                table: "Jogador",
                column: "ClubeId");

            migrationBuilder.CreateIndex(
                name: "IX_Jogador_JogadorId",
                table: "Jogador",
                column: "JogadorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Jogador_PosicaoId",
                table: "Jogador",
                column: "PosicaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Jogador_RodadaId",
                table: "Jogador",
                column: "RodadaId");

            migrationBuilder.CreateIndex(
                name: "IX_Jogador_StatusId",
                table: "Jogador",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Jogador_ScoutAtualId_RodadaId",
                table: "Jogador",
                columns: new[] { "ScoutAtualId", "RodadaId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JogadorHistorico_ClubeId",
                table: "JogadorHistorico",
                column: "ClubeId");

            migrationBuilder.CreateIndex(
                name: "IX_JogadorHistorico_JogadorId",
                table: "JogadorHistorico",
                column: "JogadorId");

            migrationBuilder.CreateIndex(
                name: "IX_JogadorHistorico_PosicaoId",
                table: "JogadorHistorico",
                column: "PosicaoId");

            migrationBuilder.CreateIndex(
                name: "IX_JogadorHistorico_RodadaId",
                table: "JogadorHistorico",
                column: "RodadaId");

            migrationBuilder.CreateIndex(
                name: "IX_JogadorHistorico_ScoutId",
                table: "JogadorHistorico",
                column: "ScoutId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JogadorHistorico_StatusId",
                table: "JogadorHistorico",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Scout_JogadorId",
                table: "Scout",
                column: "JogadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Scout_RodadaId",
                table: "Scout",
                column: "RodadaId");

            migrationBuilder.AddForeignKey(
                name: "FK_JogadorHistorico_Scout_ScoutId",
                table: "JogadorHistorico",
                column: "ScoutId",
                principalTable: "Scout",
                principalColumn: "ScoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_JogadorHistorico_Jogador_JogadorId",
                table: "JogadorHistorico",
                column: "JogadorId",
                principalTable: "Jogador",
                principalColumn: "JogadorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Scout_Jogador_JogadorId",
                table: "Scout",
                column: "JogadorId",
                principalTable: "Jogador",
                principalColumn: "JogadorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jogador_Scout_ScoutAtualId_RodadaId",
                table: "Jogador");

            migrationBuilder.DropTable(
                name: "JogadorHistorico");

            migrationBuilder.DropTable(
                name: "Scout");

            migrationBuilder.DropTable(
                name: "Jogador");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Status_StatusId",
                table: "Status");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Rodada_RodadaId",
                table: "Rodada");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Posicao_PosicaoId",
                table: "Posicao");
        }
    }
}
