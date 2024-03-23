using Microsoft.EntityFrameworkCore.Migrations;

namespace Client.DAL.Data.Migrations
{
    public partial class Solv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Dpartments",
                table: "Dpartments");

            migrationBuilder.RenameTable(
                name: "Dpartments",
                newName: "Departments");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Departments",
                table: "Departments",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Departments",
                table: "Departments");

            migrationBuilder.RenameTable(
                name: "Departments",
                newName: "Dpartments");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dpartments",
                table: "Dpartments",
                column: "Id");
        }
    }
}
