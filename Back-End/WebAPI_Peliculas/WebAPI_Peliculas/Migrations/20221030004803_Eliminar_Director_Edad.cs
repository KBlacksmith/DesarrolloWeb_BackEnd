using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI_Peliculas.Migrations
{
    public partial class Eliminar_Director_Edad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Edad",
                table: "Directores");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Directores",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1)",
                oldMaxLength: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Directores",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "Edad",
                table: "Directores",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
