using Microsoft.EntityFrameworkCore.Migrations;

namespace Fotoplastykon.DAL.Migrations
{
    public partial class AddPhotosToInformation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PhotoId",
                table: "informations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_informations_PhotoId",
                table: "informations",
                column: "PhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_informations_files_PhotoId",
                table: "informations",
                column: "PhotoId",
                principalTable: "files",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_informations_files_PhotoId",
                table: "informations");

            migrationBuilder.DropIndex(
                name: "IX_informations_PhotoId",
                table: "informations");

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "informations");
        }
    }
}
