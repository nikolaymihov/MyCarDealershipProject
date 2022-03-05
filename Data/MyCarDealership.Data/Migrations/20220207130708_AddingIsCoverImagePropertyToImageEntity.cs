namespace MyCarDealershipProject.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;
    public partial class AddingIsCoverImagePropertyToImageEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCoverImage",
                table: "Images",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCoverImage",
                table: "Images");
        }
    }
}
