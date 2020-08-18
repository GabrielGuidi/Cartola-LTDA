using Microsoft.EntityFrameworkCore.Migrations;

namespace Cartola.Infra.Migrations
{
    public partial class createColumnConsolidado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Consolidado",
                table: "Scout",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Consolidado",
                table: "JogadorHistorico",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Consolidado",
                table: "Jogador",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Consolidado",
                table: "Scout");

            migrationBuilder.DropColumn(
                name: "Consolidado",
                table: "JogadorHistorico");

            migrationBuilder.DropColumn(
                name: "Consolidado",
                table: "Jogador");
        }
    }
}
