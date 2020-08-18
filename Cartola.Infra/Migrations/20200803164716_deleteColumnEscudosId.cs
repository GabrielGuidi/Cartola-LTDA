using Microsoft.EntityFrameworkCore.Migrations;

namespace Cartola.Infra.Migrations
{
    public partial class deleteColumnEscudosId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EscudosId",
                table: "Clube");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EscudosId",
                table: "Clube",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
