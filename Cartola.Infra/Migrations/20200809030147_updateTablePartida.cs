using Microsoft.EntityFrameworkCore.Migrations;

namespace Cartola.Infra.Migrations
{
    public partial class updateTablePartida : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AproveitamentoClubeMandante",
                table: "Partida");

            migrationBuilder.DropColumn(
                name: "AproveitamentoClubeVisitante",
                table: "Partida");

            migrationBuilder.AlterColumn<int>(
                name: "PlacarOficialVisitante",
                table: "Partida",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PlacarOficialMandante",
                table: "Partida",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "AproveitamentoMandante",
                table: "Partida",
                maxLength: 16,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AproveitamentoVisitante",
                table: "Partida",
                maxLength: 16,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AproveitamentoMandante",
                table: "Partida");

            migrationBuilder.DropColumn(
                name: "AproveitamentoVisitante",
                table: "Partida");

            migrationBuilder.AlterColumn<int>(
                name: "PlacarOficialVisitante",
                table: "Partida",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PlacarOficialMandante",
                table: "Partida",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AproveitamentoClubeMandante",
                table: "Partida",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AproveitamentoClubeVisitante",
                table: "Partida",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: true);
        }
    }
}
