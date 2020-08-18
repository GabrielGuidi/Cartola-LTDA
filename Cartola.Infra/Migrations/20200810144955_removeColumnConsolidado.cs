using Microsoft.EntityFrameworkCore.Migrations;

namespace Cartola.Infra.Migrations
{
    public partial class removeColumnConsolidado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Consolidado",
                table: "Jogador");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Consolidado",
                table: "Jogador",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
