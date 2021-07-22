namespace EateryPOSSystem.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ChangeMaterialReceiptToWarehouseReceiptEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "WarehouseMaterials",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReceiptNumber",
                table: "MaterialReceipts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                table: "MaterialReceipts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WarehouseMaterialMaterialId",
                table: "MaterialReceipts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WarehouseMaterialWarehouseId",
                table: "MaterialReceipts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaterialReceipts_WarehouseId",
                table: "MaterialReceipts",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialReceipts_WarehouseMaterialWarehouseId_WarehouseMaterialMaterialId",
                table: "MaterialReceipts",
                columns: new[] { "WarehouseMaterialWarehouseId", "WarehouseMaterialMaterialId" });

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialReceipts_WarehouseMaterials_WarehouseMaterialWarehouseId_WarehouseMaterialMaterialId",
                table: "MaterialReceipts",
                columns: new[] { "WarehouseMaterialWarehouseId", "WarehouseMaterialMaterialId" },
                principalTable: "WarehouseMaterials",
                principalColumns: new[] { "WarehouseId", "MaterialId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialReceipts_Warehouses_WarehouseId",
                table: "MaterialReceipts",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaterialReceipts_WarehouseMaterials_WarehouseMaterialWarehouseId_WarehouseMaterialMaterialId",
                table: "MaterialReceipts");

            migrationBuilder.DropForeignKey(
                name: "FK_MaterialReceipts_Warehouses_WarehouseId",
                table: "MaterialReceipts");

            migrationBuilder.DropIndex(
                name: "IX_MaterialReceipts_WarehouseId",
                table: "MaterialReceipts");

            migrationBuilder.DropIndex(
                name: "IX_MaterialReceipts_WarehouseMaterialWarehouseId_WarehouseMaterialMaterialId",
                table: "MaterialReceipts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "WarehouseMaterials");

            migrationBuilder.DropColumn(
                name: "ReceiptNumber",
                table: "MaterialReceipts");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "MaterialReceipts");

            migrationBuilder.DropColumn(
                name: "WarehouseMaterialMaterialId",
                table: "MaterialReceipts");

            migrationBuilder.DropColumn(
                name: "WarehouseMaterialWarehouseId",
                table: "MaterialReceipts");
        }
    }
}
