namespace MyCarDealershipProject.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;
    
    public partial class RemoveUnusedImageRemoteUrlProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RemoteImageUrl",
                table: "Images");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RemoteImageUrl",
                table: "Images",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
