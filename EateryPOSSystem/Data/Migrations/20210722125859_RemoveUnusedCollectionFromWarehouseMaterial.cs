namespace EateryPOSSystem.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class RemoveUnusedCollectionFromWarehouseMaterial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseReceipts_WarehouseMaterials_WarehouseMaterialWarehouseId_WarehouseMaterialMaterialId",
                table: "WarehouseReceipts");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseReceipts_WarehouseMaterialWarehouseId_WarehouseMaterialMaterialId",
                table: "WarehouseReceipts");

            migrationBuilder.DropColumn(
                name: "WarehouseMaterialMaterialId",
                table: "WarehouseReceipts");

            migrationBuilder.DropColumn(
                name: "WarehouseMaterialWarehouseId",
                table: "WarehouseReceipts");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "WarehouseMaterials",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WarehouseMaterialMaterialId",
                table: "WarehouseReceipts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WarehouseMaterialWarehouseId",
                table: "WarehouseReceipts",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "WarehouseMaterials",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseReceipts_WarehouseMaterialWarehouseId_WarehouseMaterialMaterialId",
                table: "WarehouseReceipts",
                columns: new[] { "WarehouseMaterialWarehouseId", "WarehouseMaterialMaterialId" });

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseReceipts_WarehouseMaterials_WarehouseMaterialWarehouseId_WarehouseMaterialMaterialId",
                table: "WarehouseReceipts",
                columns: new[] { "WarehouseMaterialWarehouseId", "WarehouseMaterialMaterialId" },
                principalTable: "WarehouseMaterials",
                principalColumns: new[] { "WarehouseId", "MaterialId" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
