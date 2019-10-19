using Microsoft.EntityFrameworkCore.Migrations;

namespace Fotoplastykon.DAL.Migrations
{
    public partial class AddRelativeAndAbsolutePathtoFiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Path",
                table: "files",
                newName: "RelativePath");

            migrationBuilder.AddColumn<string>(
                name: "AbsolutePath",
                table: "files",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AbsolutePath",
                table: "files");

            migrationBuilder.RenameColumn(
                name: "RelativePath",
                table: "files",
                newName: "Path");
        }
    }
}
