using Microsoft.EntityFrameworkCore.Migrations;

namespace Fotoplastykon.DAL.Migrations
{
    public partial class AddAcceptedToNotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Accepted",
                table: "invitation_notifications",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Accepted",
                table: "invitation_notifications");
        }
    }
}
