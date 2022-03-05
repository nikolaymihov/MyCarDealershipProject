namespace MyCarDealershipProject.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;
    public partial class AddingModifiedOnPropertyToPostEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Posts",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Posts");
        }
    }
}
