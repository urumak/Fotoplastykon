using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fotoplastykon.DAL.Migrations
{
    public partial class AddPagePublicId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PagePublicId",
                table: "films",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PagePublicId",
                table: "films");
        }
    }
}
