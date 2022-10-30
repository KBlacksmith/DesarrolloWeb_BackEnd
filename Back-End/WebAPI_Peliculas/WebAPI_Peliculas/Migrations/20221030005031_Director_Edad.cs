using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI_Peliculas.Migrations
{
    public partial class Director_Edad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Edad",
                table: "Directores",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Edad",
                table: "Directores");
        }
    }
}
