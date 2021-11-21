using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCarDealershipProject.Data.Migrations
{
    public partial class ExtendingOfCarDbModelAndAddingNewDbModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Extras",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<int>(
                name: "FuelTypeId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Horsepower",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Kilometers",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TransmissionTypeId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FuelTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuelTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransmissionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransmissionTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_FuelTypeId",
                table: "Cars",
                column: "FuelTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_TransmissionTypeId",
                table: "Cars",
                column: "TransmissionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_FuelTypes_FuelTypeId",
                table: "Cars",
                column: "FuelTypeId",
                principalTable: "FuelTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_TransmissionTypes_TransmissionTypeId",
                table: "Cars",
                column: "TransmissionTypeId",
                principalTable: "TransmissionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_FuelTypes_FuelTypeId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_TransmissionTypes_TransmissionTypeId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "FuelTypes");

            migrationBuilder.DropTable(
                name: "TransmissionTypes");

            migrationBuilder.DropIndex(
                name: "IX_Cars_FuelTypeId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_TransmissionTypeId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "FuelTypeId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Horsepower",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Kilometers",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "TransmissionTypeId",
                table: "Cars");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Extras",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
