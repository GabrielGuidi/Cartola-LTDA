using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cartola.Infra.Migrations
{
    public partial class createTableRodada : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rodada",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RodadaId = table.Column<int>(maxLength: 256, nullable: false),
                    Inicio = table.Column<DateTime>(nullable: false),
                    Fim = table.Column<DateTime>(nullable: false),
                    DataModificacao = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rodada", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rodada_RodadaId",
                table: "Rodada",
                column: "RodadaId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rodada");
        }
    }
}
