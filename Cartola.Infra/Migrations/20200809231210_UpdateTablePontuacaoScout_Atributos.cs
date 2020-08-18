using Microsoft.EntityFrameworkCore.Migrations;

namespace Cartola.Infra.Migrations
{
    public partial class UpdateTablePontuacaoScout_Atributos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Assistencia",
                table: "Scout",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CartaoAmarelo",
                table: "Scout",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CartaoVermelho",
                table: "Scout",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DefesaDePenalti",
                table: "Scout",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DefesaDificil",
                table: "Scout",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Desarme",
                table: "Scout",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FaltaCometida",
                table: "Scout",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FaltaSofrida",
                table: "Scout",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FinalizacaoDefendida",
                table: "Scout",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FinalizacaoNaTrave",
                table: "Scout",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FinalizacaoParaFora",
                table: "Scout",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Gol",
                table: "Scout",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GolContra",
                table: "Scout",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GolSofrido",
                table: "Scout",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Impedimento",
                table: "Scout",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "JogoSemSofrerGols",
                table: "Scout",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PasseIncompleto",
                table: "Scout",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PenaltiPerdido",
                table: "Scout",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Assistencia",
                table: "Scout");

            migrationBuilder.DropColumn(
                name: "CartaoAmarelo",
                table: "Scout");

            migrationBuilder.DropColumn(
                name: "CartaoVermelho",
                table: "Scout");

            migrationBuilder.DropColumn(
                name: "DefesaDePenalti",
                table: "Scout");

            migrationBuilder.DropColumn(
                name: "DefesaDificil",
                table: "Scout");

            migrationBuilder.DropColumn(
                name: "Desarme",
                table: "Scout");

            migrationBuilder.DropColumn(
                name: "FaltaCometida",
                table: "Scout");

            migrationBuilder.DropColumn(
                name: "FaltaSofrida",
                table: "Scout");

            migrationBuilder.DropColumn(
                name: "FinalizacaoDefendida",
                table: "Scout");

            migrationBuilder.DropColumn(
                name: "FinalizacaoNaTrave",
                table: "Scout");

            migrationBuilder.DropColumn(
                name: "FinalizacaoParaFora",
                table: "Scout");

            migrationBuilder.DropColumn(
                name: "Gol",
                table: "Scout");

            migrationBuilder.DropColumn(
                name: "GolContra",
                table: "Scout");

            migrationBuilder.DropColumn(
                name: "GolSofrido",
                table: "Scout");

            migrationBuilder.DropColumn(
                name: "Impedimento",
                table: "Scout");

            migrationBuilder.DropColumn(
                name: "JogoSemSofrerGols",
                table: "Scout");

            migrationBuilder.DropColumn(
                name: "PasseIncompleto",
                table: "Scout");

            migrationBuilder.DropColumn(
                name: "PenaltiPerdido",
                table: "Scout");
        }
    }
}
