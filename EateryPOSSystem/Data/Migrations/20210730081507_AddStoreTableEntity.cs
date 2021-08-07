using Microsoft.EntityFrameworkCore.Migrations;

namespace EateryPOSSystem.Data.Migrations
{
    public partial class AddStoreTableEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Materials_MaterialId",
                table: "Recipes");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Products_ProductId",
                table: "Recipes");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreProducts_Products_ProductId",
                table: "StoreProducts");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_MaterialId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_ProductId",
                table: "Recipes");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Recipes",
                newName: "WarehouseMaterialWarehouseId");

            migrationBuilder.RenameColumn(
                name: "MaterialId",
                table: "Recipes",
                newName: "WarehouseMaterialMaterialId");

            migrationBuilder.AddColumn<int>(
                name: "StoreProductId",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "StoreTables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableNumber = table.Column<int>(type: "int", nullable: false),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    StoreName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreTables", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_StoreProductId",
                table: "Recipes",
                column: "StoreProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_WarehouseMaterialWarehouseId_WarehouseMaterialMaterialId",
                table: "Recipes",
                columns: new[] { "WarehouseMaterialWarehouseId", "WarehouseMaterialMaterialId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_StoreProducts_StoreProductId",
                table: "Recipes",
                column: "StoreProductId",
                principalTable: "StoreProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_WarehouseMaterials_WarehouseMaterialWarehouseId_WarehouseMaterialMaterialId",
                table: "Recipes",
                columns: new[] { "WarehouseMaterialWarehouseId", "WarehouseMaterialMaterialId" },
                principalTable: "WarehouseMaterials",
                principalColumns: new[] { "WarehouseId", "MaterialId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreProducts_Products_ProductId",
                table: "StoreProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_StoreProducts_StoreProductId",
                table: "Recipes");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_WarehouseMaterials_WarehouseMaterialWarehouseId_WarehouseMaterialMaterialId",
                table: "Recipes");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreProducts_Products_ProductId",
                table: "StoreProducts");

            migrationBuilder.DropTable(
                name: "StoreTables");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_StoreProductId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_WarehouseMaterialWarehouseId_WarehouseMaterialMaterialId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "StoreProductId",
                table: "Recipes");

            migrationBuilder.RenameColumn(
                name: "WarehouseMaterialWarehouseId",
                table: "Recipes",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "WarehouseMaterialMaterialId",
                table: "Recipes",
                newName: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_MaterialId",
                table: "Recipes",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_ProductId",
                table: "Recipes",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Materials_MaterialId",
                table: "Recipes",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Products_ProductId",
                table: "Recipes",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreProducts_Products_ProductId",
                table: "StoreProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
