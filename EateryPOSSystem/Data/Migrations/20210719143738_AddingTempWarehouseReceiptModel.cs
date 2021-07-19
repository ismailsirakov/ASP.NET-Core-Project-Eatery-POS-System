using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EateryPOSSystem.Data.Migrations
{
    public partial class AddingTempWarehouseReceiptModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Cities_CityId",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_CityId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Addresses");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Providers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TempWarehouseReceipts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceiptNumber = table.Column<int>(type: "int", nullable: false),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    DocumentTypeId = table.Column<int>(type: "int", nullable: false),
                    DocumentNumber = table.Column<int>(type: "int", nullable: false),
                    DocumentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempWarehouseReceipts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Providers_CityId",
                table: "Providers",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Providers_Cities_CityId",
                table: "Providers",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Providers_Cities_CityId",
                table: "Providers");

            migrationBuilder.DropTable(
                name: "TempWarehouseReceipts");

            migrationBuilder.DropIndex(
                name: "IX_Providers_CityId",
                table: "Providers");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Providers");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Addresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CityId",
                table: "Addresses",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Cities_CityId",
                table: "Addresses",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
