using Microsoft.EntityFrameworkCore.Migrations;

namespace Site.Models.Migrations
{
    public partial class ColumnNameChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Decription",
                table: "Templates",
                newName: "Description");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Templates",
                newName: "Decription");
        }
    }
}
