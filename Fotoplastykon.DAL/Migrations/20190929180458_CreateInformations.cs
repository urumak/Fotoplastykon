using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fotoplastykon.DAL.Migrations
{
    public partial class CreateInformations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "information_comments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    InformationId = table.Column<long>(nullable: false),
                    CreatedById = table.Column<long>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    ParentId = table.Column<long>(nullable: true),
                    Content = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_information_comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_information_comments_users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_information_comments_information_comments_ParentId",
                        column: x => x.ParentId,
                        principalTable: "information_comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "informations",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedById = table.Column<long>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_informations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_informations_users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_information_comments_CreatedById",
                table: "information_comments",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_information_comments_ParentId",
                table: "information_comments",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_informations_CreatedById",
                table: "informations",
                column: "CreatedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "information_comments");

            migrationBuilder.DropTable(
                name: "informations");
        }
    }
}
