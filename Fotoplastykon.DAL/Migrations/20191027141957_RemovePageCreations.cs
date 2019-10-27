using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fotoplastykon.DAL.Migrations
{
    public partial class RemovePageCreations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "film_page_creations");

            migrationBuilder.DropTable(
                name: "page_creation_requests");

            migrationBuilder.DropTable(
                name: "person_page_creations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "film_page_creations",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FilmId = table.Column<long>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_film_page_creations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_film_page_creations_films_FilmId",
                        column: x => x.FilmId,
                        principalTable: "films",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_film_page_creations_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "page_creation_requests",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PagePublicId = table.Column<string>(nullable: true),
                    PageType = table.Column<int>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_page_creation_requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_page_creation_requests_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "person_page_creations",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PersonId = table.Column<long>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_person_page_creations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_person_page_creations_film_people_PersonId",
                        column: x => x.PersonId,
                        principalTable: "film_people",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_person_page_creations_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_film_page_creations_FilmId",
                table: "film_page_creations",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_film_page_creations_UserId",
                table: "film_page_creations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_page_creation_requests_UserId",
                table: "page_creation_requests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_person_page_creations_PersonId",
                table: "person_page_creations",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_person_page_creations_UserId",
                table: "person_page_creations",
                column: "UserId");
        }
    }
}
