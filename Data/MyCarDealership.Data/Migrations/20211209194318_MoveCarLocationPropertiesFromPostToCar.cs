namespace MyCarDealershipProject.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class MoveCarLocationPropertiesFromPostToCar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarLocationCity",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "CarLocationCountry",
                table: "Posts");

            migrationBuilder.AddColumn<string>(
                name: "LocationCity",
                table: "Cars",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LocationCountry",
                table: "Cars",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocationCity",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "LocationCountry",
                table: "Cars");

            migrationBuilder.AddColumn<string>(
                name: "CarLocationCity",
                table: "Posts",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CarLocationCountry",
                table: "Posts",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }
    }
}
