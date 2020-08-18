using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cartola.Infra.Migrations
{
    public partial class updateTableRodada : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fim",
                table: "Rodada");

            migrationBuilder.DropColumn(
                name: "Inicio",
                table: "Rodada");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataFim",
                table: "Rodada",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataInicio",
                table: "Rodada",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataFim",
                table: "Rodada");

            migrationBuilder.DropColumn(
                name: "DataInicio",
                table: "Rodada");

            migrationBuilder.AddColumn<DateTime>(
                name: "Fim",
                table: "Rodada",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Inicio",
                table: "Rodada",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
