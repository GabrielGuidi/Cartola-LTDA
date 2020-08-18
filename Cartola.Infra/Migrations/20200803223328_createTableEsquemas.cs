using Microsoft.EntityFrameworkCore.Migrations;

namespace Cartola.Infra.Migrations
{
    public partial class createTableEsquemas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Esquema",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EsquemaId = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(maxLength: 16, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Esquema", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EsquemaPosicoes",
                columns: table => new
                {
                    PosicoesEscalacaoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EsquemaId = table.Column<int>(nullable: false),
                    Goleiro = table.Column<int>(nullable: false),
                    Laterais = table.Column<int>(nullable: false),
                    Zagueiros = table.Column<int>(nullable: false),
                    Meias = table.Column<int>(nullable: false),
                    Atacantes = table.Column<int>(nullable: false),
                    Tecnico = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EsquemaPosicoes", x => x.PosicoesEscalacaoId);
                    table.ForeignKey(
                        name: "FK_EsquemaPosicoes_Esquema_EsquemaId",
                        column: x => x.EsquemaId,
                        principalTable: "Esquema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EsquemaPosicoes_EsquemaId",
                table: "EsquemaPosicoes",
                column: "EsquemaId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EsquemaPosicoes");

            migrationBuilder.DropTable(
                name: "Esquema");
        }
    }
}
