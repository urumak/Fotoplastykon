using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fotoplastykon.DAL.Migrations
{
    public partial class AddFilPeoplePages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "page_creations");

            migrationBuilder.CreateTable(
                name: "film_page_creations",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<long>(nullable: false),
                    FilmId = table.Column<long>(nullable: false)
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
                name: "film_people",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Profession = table.Column<int>(nullable: false),
                    PagePublicId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_film_people", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "people_in_roles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PersonId = table.Column<long>(nullable: false),
                    FilmId = table.Column<long>(nullable: false),
                    Role = table.Column<int>(nullable: false),
                    CharacterName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_people_in_roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_people_in_roles_films_FilmId",
                        column: x => x.FilmId,
                        principalTable: "films",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_people_in_roles_film_people_PersonId",
                        column: x => x.PersonId,
                        principalTable: "film_people",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "person_page_creations",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<long>(nullable: false),
                    PersonId = table.Column<long>(nullable: false)
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
                name: "IX_people_in_roles_FilmId",
                table: "people_in_roles",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_people_in_roles_PersonId",
                table: "people_in_roles",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_person_page_creations_PersonId",
                table: "person_page_creations",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_person_page_creations_UserId",
                table: "person_page_creations",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "film_page_creations");

            migrationBuilder.DropTable(
                name: "people_in_roles");

            migrationBuilder.DropTable(
                name: "person_page_creations");

            migrationBuilder.DropTable(
                name: "film_people");

            migrationBuilder.CreateTable(
                name: "page_creations",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FilmId = table.Column<long>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_page_creations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_page_creations_films_FilmId",
                        column: x => x.FilmId,
                        principalTable: "films",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_page_creations_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_page_creations_FilmId",
                table: "page_creations",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_page_creations_UserId",
                table: "page_creations",
                column: "UserId");
        }
    }
}
