﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Cartola.Infra.Migrations
{
    public partial class updateColumnScoutIdJogador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jogador_Scout_ScoutAtualId_RodadaId",
                table: "Jogador");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Scout_ScoutId_RodadaId",
                table: "Scout");

            migrationBuilder.DropIndex(
                name: "IX_JogadorHistorico_ScoutId",
                table: "JogadorHistorico");

            migrationBuilder.DropIndex(
                name: "IX_Jogador_ScoutAtualId_RodadaId",
                table: "Jogador");

            migrationBuilder.AlterColumn<int>(
                name: "ScoutId",
                table: "JogadorHistorico",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_JogadorHistorico_ScoutId",
                table: "JogadorHistorico",
                column: "ScoutId",
                unique: true,
                filter: "[ScoutId] IS NOT NULL");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jogador_Scout_ScoutAtualId",
                table: "Jogador");

            migrationBuilder.DropIndex(
                name: "IX_JogadorHistorico_ScoutId",
                table: "JogadorHistorico");

            migrationBuilder.DropIndex(
                name: "IX_Jogador_ScoutAtualId",
                table: "Jogador");

            migrationBuilder.AlterColumn<int>(
                name: "ScoutId",
                table: "JogadorHistorico",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Scout_ScoutId_RodadaId",
                table: "Scout",
                columns: new[] { "ScoutId", "RodadaId" });

            migrationBuilder.CreateIndex(
                name: "IX_JogadorHistorico_ScoutId",
                table: "JogadorHistorico",
                column: "ScoutId",
                unique: true);

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
        }
    }
}
