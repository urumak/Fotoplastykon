using Microsoft.EntityFrameworkCore.Migrations;

namespace Fotoplastykon.DAL.Migrations
{
    public partial class AddPhotoToQuiz : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PhotoId",
                table: "quizzes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_quizzes_PhotoId",
                table: "quizzes",
                column: "PhotoId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_quizzes_files_PhotoId",
                table: "quizzes",
                column: "PhotoId",
                principalTable: "files",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_quizzes_files_PhotoId",
                table: "quizzes");

            migrationBuilder.DropIndex(
                name: "IX_quizzes_PhotoId",
                table: "quizzes");

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "quizzes");
        }
    }
}
