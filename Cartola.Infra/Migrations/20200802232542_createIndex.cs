using Microsoft.EntityFrameworkCore.Migrations;

namespace Cartola.Infra.Migrations
{
    public partial class createIndex : Migration
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
                name: "ClubeCartolaId",
                table: "Clube");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Clube",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ClubeId",
                table: "Clube",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clube",
                table: "Clube",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Clube_ClubeId",
                table: "Clube",
                column: "ClubeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Escudos_Clube_ClubeId",
                table: "Escudos",
                column: "ClubeId",
                principalTable: "Clube",
                principalColumn: "Id",
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

            migrationBuilder.DropIndex(
                name: "IX_Clube_ClubeId",
                table: "Clube");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Clube");

            migrationBuilder.DropColumn(
                name: "ClubeId",
                table: "Clube");

            migrationBuilder.AddColumn<int>(
                name: "ClubeCartolaId",
                table: "Clube",
                type: "int",
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
    }
}
