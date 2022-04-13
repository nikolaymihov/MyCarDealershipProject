using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCarDealershipProject.Migrations
{
    public partial class AddingIsPublicPropertyToPostEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "Posts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "Posts");
        }
    }
}
