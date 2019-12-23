using Microsoft.EntityFrameworkCore.Migrations;

namespace Fotoplastykon.DAL.Migrations
{
    public partial class AddTypeToNotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSender",
                table: "invitation_notifications");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "invitation_notifications",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "invitation_notifications");

            migrationBuilder.AddColumn<bool>(
                name: "IsSender",
                table: "invitation_notifications",
                nullable: false,
                defaultValue: false);
        }
    }
}
