using Microsoft.EntityFrameworkCore.Migrations;

namespace Fotoplastykon.DAL.Migrations
{
    public partial class ChangeSomeOnDeleteBehaviours : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddForeignKey(
                name: "FK_film_people_files_PhotoId",
                table: "film_people",
                column: "PhotoId",
                principalTable: "files",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_films_files_PhotoId",
                table: "films",
                column: "PhotoId",
                principalTable: "files",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_users_files_PhotoId",
                table: "users",
                column: "PhotoId",
                principalTable: "files",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
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
    }
}
