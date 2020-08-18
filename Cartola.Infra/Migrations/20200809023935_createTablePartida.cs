using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cartola.Infra.Migrations
{
    public partial class createTablePartida : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_Clube_ClubeId",
                table: "Clube",
                column: "ClubeId");

            migrationBuilder.CreateTable(
                name: "Partida",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartidaId = table.Column<int>(nullable: false),
                    Rodada = table.Column<int>(nullable: false),
                    ClubeCasaId = table.Column<int>(nullable: false),
                    ClubeCasaPosicao = table.Column<int>(nullable: false),
                    ClubeVisitanteId = table.Column<int>(nullable: false),
                    AproveitamentoClubeMandante = table.Column<string>(maxLength: 16, nullable: true),
                    AproveitamentoClubeVisitante = table.Column<string>(maxLength: 16, nullable: true),
                    ClubeVisitantePosicao = table.Column<int>(nullable: false),
                    DataPartida = table.Column<DateTime>(nullable: false),
                    LocalPartida = table.Column<string>(maxLength: 64, nullable: true),
                    PartidaValida = table.Column<bool>(nullable: false),
                    PlacarOficialMandante = table.Column<int>(nullable: false),
                    PlacarOficialVisitante = table.Column<int>(nullable: false),
                    UrlConfronto = table.Column<string>(maxLength: 256, nullable: true),
                    UrlTransmissao = table.Column<string>(maxLength: 256, nullable: true),
                    DataModificacao = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partida", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Partida_Clube_ClubeCasaId",
                        column: x => x.ClubeCasaId,
                        principalTable: "Clube",
                        principalColumn: "ClubeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Partida_Clube_ClubeVisitanteId",
                        column: x => x.ClubeVisitanteId,
                        principalTable: "Clube",
                        principalColumn: "ClubeId");
                });

            migrationBuilder.CreateTable(
                name: "Transmissao",
                columns: table => new
                {
                    TransmissaoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartidaId = table.Column<int>(nullable: false),
                    Label = table.Column<string>(maxLength: 128, nullable: true),
                    Url = table.Column<string>(maxLength: 256, nullable: true),
                    DataModificacao = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transmissao", x => x.TransmissaoId);
                    table.ForeignKey(
                        name: "FK_Transmissao_Partida_PartidaId",
                        column: x => x.PartidaId,
                        principalTable: "Partida",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Status_StatusId",
                table: "Status",
                column: "StatusId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Esquema_EsquemaId",
                table: "Esquema",
                column: "EsquemaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Partida_ClubeCasaId",
                table: "Partida",
                column: "ClubeCasaId");

            migrationBuilder.CreateIndex(
                name: "IX_Partida_ClubeVisitanteId",
                table: "Partida",
                column: "ClubeVisitanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Partida_PartidaId",
                table: "Partida",
                column: "PartidaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transmissao_PartidaId",
                table: "Transmissao",
                column: "PartidaId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transmissao");

            migrationBuilder.DropTable(
                name: "Partida");

            migrationBuilder.DropIndex(
                name: "IX_Status_StatusId",
                table: "Status");

            migrationBuilder.DropIndex(
                name: "IX_Esquema_EsquemaId",
                table: "Esquema");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Clube_ClubeId",
                table: "Clube");
        }
    }
}
