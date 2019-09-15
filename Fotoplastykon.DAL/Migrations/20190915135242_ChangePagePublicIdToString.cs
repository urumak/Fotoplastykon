using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fotoplastykon.DAL.Migrations
{
    public partial class ChangePagePublicIdToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PagePublicId",
                table: "films",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(Guid));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "PagePublicId",
                table: "films",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 80);
        }
    }
}
