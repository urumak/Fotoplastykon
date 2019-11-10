using Microsoft.EntityFrameworkCore.Migrations;

namespace Fotoplastykon.DAL.Migrations
{
    public partial class ForeginKeyFixInformationComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_information_comments_informations_ParentId",
                table: "information_comments");

            migrationBuilder.CreateIndex(
                name: "IX_information_comments_InformationId",
                table: "information_comments",
                column: "InformationId");

            migrationBuilder.AddForeignKey(
                name: "FK_information_comments_informations_InformationId",
                table: "information_comments",
                column: "InformationId",
                principalTable: "informations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_information_comments_informations_InformationId",
                table: "information_comments");

            migrationBuilder.DropIndex(
                name: "IX_information_comments_InformationId",
                table: "information_comments");

            migrationBuilder.AddForeignKey(
                name: "FK_information_comments_informations_ParentId",
                table: "information_comments",
                column: "ParentId",
                principalTable: "informations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
