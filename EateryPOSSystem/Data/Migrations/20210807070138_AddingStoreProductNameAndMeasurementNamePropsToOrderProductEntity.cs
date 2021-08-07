using Microsoft.EntityFrameworkCore.Migrations;

namespace EateryPOSSystem.Data.Migrations
{
    public partial class AddingStoreProductNameAndMeasurementNamePropsToOrderProductEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MeasurementName",
                table: "OrderProducts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StoreProductName",
                table: "OrderProducts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MeasurementName",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "StoreProductName",
                table: "OrderProducts");
        }
    }
}
