using Microsoft.EntityFrameworkCore.Migrations;

namespace EateryPOSSystem.Data.Migrations
{
    public partial class AddTableEntityAndEditRecipeEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Materials_MaterialId",
                table: "Recipes");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Products_ProductId",
                table: "Recipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WarehouseMaterials",
                table: "WarehouseMaterials");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Recipes",
                newName: "WarehouseMaterialId");

            migrationBuilder.RenameColumn(
                name: "MaterialId",
                table: "Recipes",
                newName: "WarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_Recipes_ProductId",
                table: "Recipes",
                newName: "IX_Recipes_WarehouseMaterialId");

            migrationBuilder.RenameIndex(
                name: "IX_Recipes_MaterialId",
                table: "Recipes",
                newName: "IX_Recipes_WarehouseId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "WarehouseMaterials",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "StoreProductId",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarehouseMaterials",
                table: "WarehouseMaterials",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    TableNumber = table.Column<int>(type: "int", nullable: false),
                    BillNumber = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tables", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseMaterials_WarehouseId",
                table: "WarehouseMaterials",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_StoreProductId",
                table: "Recipes",
                column: "StoreProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_StoreProducts_StoreProductId",
                table: "Recipes",
                column: "StoreProductId",
                principalTable: "StoreProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_WarehouseMaterials_WarehouseMaterialId",
                table: "Recipes",
                column: "WarehouseMaterialId",
                principalTable: "WarehouseMaterials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Warehouses_WarehouseId",
                table: "Recipes",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_StoreProducts_StoreProductId",
                table: "Recipes");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_WarehouseMaterials_WarehouseMaterialId",
                table: "Recipes");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Warehouses_WarehouseId",
                table: "Recipes");

            migrationBuilder.DropTable(
                name: "Tables");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WarehouseMaterials",
                table: "WarehouseMaterials");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseMaterials_WarehouseId",
                table: "WarehouseMaterials");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_StoreProductId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "StoreProductId",
                table: "Recipes");

            migrationBuilder.RenameColumn(
                name: "WarehouseMaterialId",
                table: "Recipes",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "WarehouseId",
                table: "Recipes",
                newName: "MaterialId");

            migrationBuilder.RenameIndex(
                name: "IX_Recipes_WarehouseMaterialId",
                table: "Recipes",
                newName: "IX_Recipes_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Recipes_WarehouseId",
                table: "Recipes",
                newName: "IX_Recipes_MaterialId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "WarehouseMaterials",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarehouseMaterials",
                table: "WarehouseMaterials",
                columns: new[] { "WarehouseId", "MaterialId" });

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
        }
    }
}
