using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fotoplastykon.DAL.Migrations
{
    public partial class CreateForumThreadsAndMarks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "film_watchings",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<long>(nullable: false),
                    FilmId = table.Column<long>(nullable: false),
                    Mark = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_film_watchings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_film_watchings_films_FilmId",
                        column: x => x.FilmId,
                        principalTable: "films",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_film_watchings_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "forum_threads",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedById = table.Column<long>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    PersonId = table.Column<long>(nullable: true),
                    FilmId = table.Column<long>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_forum_threads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_forum_threads_users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_forum_threads_films_FilmId",
                        column: x => x.FilmId,
                        principalTable: "films",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_forum_threads_film_people_PersonId",
                        column: x => x.PersonId,
                        principalTable: "film_people",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "person_marks",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<long>(nullable: false),
                    PersonId = table.Column<long>(nullable: false),
                    Mark = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_person_marks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_person_marks_film_people_PersonId",
                        column: x => x.PersonId,
                        principalTable: "film_people",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_person_marks_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "forum_thread_comments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedById = table.Column<long>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    ThreadId = table.Column<long>(nullable: false),
                    Subject = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    ParentId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_forum_thread_comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_forum_thread_comments_users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_forum_thread_comments_forum_thread_comments_ParentId",
                        column: x => x.ParentId,
                        principalTable: "forum_thread_comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_forum_thread_comments_forum_threads_ThreadId",
                        column: x => x.ThreadId,
                        principalTable: "forum_threads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_film_watchings_FilmId",
                table: "film_watchings",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_film_watchings_UserId",
                table: "film_watchings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_forum_thread_comments_CreatedById",
                table: "forum_thread_comments",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_forum_thread_comments_ParentId",
                table: "forum_thread_comments",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_forum_thread_comments_ThreadId",
                table: "forum_thread_comments",
                column: "ThreadId");

            migrationBuilder.CreateIndex(
                name: "IX_forum_threads_CreatedById",
                table: "forum_threads",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_forum_threads_FilmId",
                table: "forum_threads",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_forum_threads_PersonId",
                table: "forum_threads",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_person_marks_PersonId",
                table: "person_marks",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_person_marks_UserId",
                table: "person_marks",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "film_watchings");

            migrationBuilder.DropTable(
                name: "forum_thread_comments");

            migrationBuilder.DropTable(
                name: "person_marks");

            migrationBuilder.DropTable(
                name: "forum_threads");
        }
    }
}
