using Microsoft.EntityFrameworkCore.Migrations;

namespace EateryPOSSystem.Data.Migrations
{
    public partial class AddingCostPropToSoldProductEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                table: "SoldProducts",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cost",
                table: "SoldProducts");
        }
    }
}
