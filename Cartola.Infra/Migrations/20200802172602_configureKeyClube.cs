using Microsoft.EntityFrameworkCore.Migrations;

namespace Cartola.Infra.Migrations
{
    public partial class configureKeyClube : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Escudos_Clube_ClubeId",
                table: "Escudos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clube",
                table: "Clube");

            migrationBuilder.DropColumn(
                name: "ClubeId",
                table: "Clube");

            migrationBuilder.AddColumn<int>(
                name: "ClubeCartolaId",
                table: "Clube",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clube",
                table: "Clube",
                column: "ClubeCartolaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Escudos_Clube_ClubeId",
                table: "Escudos",
                column: "ClubeId",
                principalTable: "Clube",
                principalColumn: "ClubeCartolaId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Escudos_Clube_ClubeId",
                table: "Escudos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clube",
                table: "Clube");

            migrationBuilder.DropColumn(
                name: "ClubeCartolaId",
                table: "Clube");

            migrationBuilder.AddColumn<int>(
                name: "ClubeId",
                table: "Clube",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clube",
                table: "Clube",
                column: "ClubeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Escudos_Clube_ClubeId",
                table: "Escudos",
                column: "ClubeId",
                principalTable: "Clube",
                principalColumn: "ClubeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
