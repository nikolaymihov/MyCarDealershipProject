namespace MyCarDealershipProject.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;
    
    public partial class PostEntityExtension : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<string>(
                name: "SellerName",
                table: "Posts",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SellerPhoneNumber",
                table: "Posts",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarLocationCity",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "CarLocationCountry",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "SellerName",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "SellerPhoneNumber",
                table: "Posts");
        }
    }
}
