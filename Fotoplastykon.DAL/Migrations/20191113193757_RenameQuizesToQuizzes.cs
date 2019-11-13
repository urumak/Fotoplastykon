using Microsoft.EntityFrameworkCore.Migrations;

namespace Fotoplastykon.DAL.Migrations
{
    public partial class RenameQuizesToQuizzes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_quiz_questions_quizes_QuizId",
                table: "quiz_questions");

            migrationBuilder.DropForeignKey(
                name: "FK_quiz_scores_quizes_QuizId",
                table: "quiz_scores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_quizes",
                table: "quizes");

            migrationBuilder.RenameTable(
                name: "quizes",
                newName: "quizzes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_quizzes",
                table: "quizzes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_quiz_questions_quizzes_QuizId",
                table: "quiz_questions",
                column: "QuizId",
                principalTable: "quizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_quiz_scores_quizzes_QuizId",
                table: "quiz_scores",
                column: "QuizId",
                principalTable: "quizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_quiz_questions_quizzes_QuizId",
                table: "quiz_questions");

            migrationBuilder.DropForeignKey(
                name: "FK_quiz_scores_quizzes_QuizId",
                table: "quiz_scores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_quizzes",
                table: "quizzes");

            migrationBuilder.RenameTable(
                name: "quizzes",
                newName: "quizes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_quizes",
                table: "quizes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_quiz_questions_quizes_QuizId",
                table: "quiz_questions",
                column: "QuizId",
                principalTable: "quizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_quiz_scores_quizes_QuizId",
                table: "quiz_scores",
                column: "QuizId",
                principalTable: "quizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
