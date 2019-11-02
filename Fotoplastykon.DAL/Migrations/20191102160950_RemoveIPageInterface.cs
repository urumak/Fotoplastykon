using Microsoft.EntityFrameworkCore.Migrations;

namespace Fotoplastykon.DAL.Migrations
{
    public partial class RemoveIPageInterface : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PagePublicId",
                table: "films",
                newName: "PublicId");

            migrationBuilder.RenameColumn(
                name: "PagePublicId",
                table: "film_people",
                newName: "PublicId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PublicId",
                table: "films",
                newName: "PagePublicId");

            migrationBuilder.RenameColumn(
                name: "PublicId",
                table: "film_people",
                newName: "PagePublicId");
        }
    }
}
