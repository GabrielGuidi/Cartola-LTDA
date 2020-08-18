using Microsoft.EntityFrameworkCore.Migrations;

namespace Cartola.Infra.Migrations
{
    public partial class createTablePosicao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posicao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PosicaoId = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Abreviacao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posicao", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posicao_PosicaoId",
                table: "Posicao",
                column: "PosicaoId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posicao");
        }
    }
}
