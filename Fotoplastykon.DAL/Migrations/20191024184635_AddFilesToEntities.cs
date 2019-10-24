using Microsoft.EntityFrameworkCore.Migrations;

namespace Fotoplastykon.DAL.Migrations
{
    public partial class AddFilesToEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PhotoId",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PhotoId",
                table: "films",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PhotoId",
                table: "film_people",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_PhotoId",
                table: "users",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_films_PhotoId",
                table: "films",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_film_people_PhotoId",
                table: "film_people",
                column: "PhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_film_people_files_PhotoId",
                table: "film_people",
                column: "PhotoId",
                principalTable: "files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_films_files_PhotoId",
                table: "films",
                column: "PhotoId",
                principalTable: "files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_users_files_PhotoId",
                table: "users",
                column: "PhotoId",
                principalTable: "files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_film_people_files_PhotoId",
                table: "film_people");

            migrationBuilder.DropForeignKey(
                name: "FK_films_files_PhotoId",
                table: "films");

            migrationBuilder.DropForeignKey(
                name: "FK_users_files_PhotoId",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_PhotoId",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_films_PhotoId",
                table: "films");

            migrationBuilder.DropIndex(
                name: "IX_film_people_PhotoId",
                table: "film_people");

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "films");

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "film_people");
        }
    }
}
