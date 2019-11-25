using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fotoplastykon.DAL.Migrations
{
    public partial class AlterSignalRConnections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Connected",
                table: "signalr_connections");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateDeleted",
                table: "signalr_connections",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateDeleted",
                table: "signalr_connections");

            migrationBuilder.AddColumn<bool>(
                name: "Connected",
                table: "signalr_connections",
                nullable: false,
                defaultValue: false);
        }
    }
}
