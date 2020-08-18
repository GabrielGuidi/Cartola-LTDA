using Microsoft.EntityFrameworkCore.Migrations;

namespace Cartola.Infra.Migrations
{
    public partial class UpdateTablePontuacaoParcial04 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PontuacaoParcial",
                table: "PontuacaoParcial");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PontuacaoParcial");

            migrationBuilder.AddColumn<int>(
                name: "PontuacaoParcialId",
                table: "PontuacaoParcial",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PontuacaoParcial",
                table: "PontuacaoParcial",
                column: "PontuacaoParcialId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PontuacaoParcial",
                table: "PontuacaoParcial");

            migrationBuilder.DropColumn(
                name: "PontuacaoParcialId",
                table: "PontuacaoParcial");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PontuacaoParcial",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PontuacaoParcial",
                table: "PontuacaoParcial",
                column: "Id");
        }
    }
}
