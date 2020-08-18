using Microsoft.EntityFrameworkCore.Migrations;

namespace Cartola.Infra.Migrations
{
    public partial class changeNameTableClube : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Produto",
                table: "Produto");

            migrationBuilder.RenameTable(
                name: "Produto",
                newName: "Clube");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clube",
                table: "Clube",
                column: "ClubeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Clube",
                table: "Clube");

            migrationBuilder.RenameTable(
                name: "Clube",
                newName: "Produto");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Produto",
                table: "Produto",
                column: "ClubeId");
        }
    }
}
