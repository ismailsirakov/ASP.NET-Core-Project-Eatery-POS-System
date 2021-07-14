using Microsoft.EntityFrameworkCore.Migrations;

namespace EateryPOSSystem.Data.Migrations
{
    public partial class ChangeMaterialReciptToWarehouseRecipt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaterialReceipts_DocumentTypes_DocumentTypeId",
                table: "MaterialReceipts");

            migrationBuilder.DropForeignKey(
                name: "FK_MaterialReceipts_Materials_MaterialId",
                table: "MaterialReceipts");

            migrationBuilder.DropForeignKey(
                name: "FK_MaterialReceipts_POSSystemUsers_UserId",
                table: "MaterialReceipts");

            migrationBuilder.DropForeignKey(
                name: "FK_MaterialReceipts_Providers_ProviderId",
                table: "MaterialReceipts");

            migrationBuilder.DropForeignKey(
                name: "FK_MaterialReceipts_WarehouseMaterials_WarehouseMaterialWarehouseId_WarehouseMaterialMaterialId",
                table: "MaterialReceipts");

            migrationBuilder.DropForeignKey(
                name: "FK_MaterialReceipts_Warehouses_WarehouseId",
                table: "MaterialReceipts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MaterialReceipts",
                table: "MaterialReceipts");

            migrationBuilder.RenameTable(
                name: "MaterialReceipts",
                newName: "WarehouseReceipts");

            migrationBuilder.RenameIndex(
                name: "IX_MaterialReceipts_WarehouseMaterialWarehouseId_WarehouseMaterialMaterialId",
                table: "WarehouseReceipts",
                newName: "IX_WarehouseReceipts_WarehouseMaterialWarehouseId_WarehouseMaterialMaterialId");

            migrationBuilder.RenameIndex(
                name: "IX_MaterialReceipts_WarehouseId",
                table: "WarehouseReceipts",
                newName: "IX_WarehouseReceipts_WarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_MaterialReceipts_UserId",
                table: "WarehouseReceipts",
                newName: "IX_WarehouseReceipts_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_MaterialReceipts_ProviderId",
                table: "WarehouseReceipts",
                newName: "IX_WarehouseReceipts_ProviderId");

            migrationBuilder.RenameIndex(
                name: "IX_MaterialReceipts_MaterialId",
                table: "WarehouseReceipts",
                newName: "IX_WarehouseReceipts_MaterialId");

            migrationBuilder.RenameIndex(
                name: "IX_MaterialReceipts_DocumentTypeId",
                table: "WarehouseReceipts",
                newName: "IX_WarehouseReceipts_DocumentTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarehouseReceipts",
                table: "WarehouseReceipts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseReceipts_DocumentTypes_DocumentTypeId",
                table: "WarehouseReceipts",
                column: "DocumentTypeId",
                principalTable: "DocumentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseReceipts_Materials_MaterialId",
                table: "WarehouseReceipts",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseReceipts_POSSystemUsers_UserId",
                table: "WarehouseReceipts",
                column: "UserId",
                principalTable: "POSSystemUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseReceipts_Providers_ProviderId",
                table: "WarehouseReceipts",
                column: "ProviderId",
                principalTable: "Providers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseReceipts_WarehouseMaterials_WarehouseMaterialWarehouseId_WarehouseMaterialMaterialId",
                table: "WarehouseReceipts",
                columns: new[] { "WarehouseMaterialWarehouseId", "WarehouseMaterialMaterialId" },
                principalTable: "WarehouseMaterials",
                principalColumns: new[] { "WarehouseId", "MaterialId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseReceipts_Warehouses_WarehouseId",
                table: "WarehouseReceipts",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseReceipts_DocumentTypes_DocumentTypeId",
                table: "WarehouseReceipts");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseReceipts_Materials_MaterialId",
                table: "WarehouseReceipts");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseReceipts_POSSystemUsers_UserId",
                table: "WarehouseReceipts");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseReceipts_Providers_ProviderId",
                table: "WarehouseReceipts");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseReceipts_WarehouseMaterials_WarehouseMaterialWarehouseId_WarehouseMaterialMaterialId",
                table: "WarehouseReceipts");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseReceipts_Warehouses_WarehouseId",
                table: "WarehouseReceipts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WarehouseReceipts",
                table: "WarehouseReceipts");

            migrationBuilder.RenameTable(
                name: "WarehouseReceipts",
                newName: "MaterialReceipts");

            migrationBuilder.RenameIndex(
                name: "IX_WarehouseReceipts_WarehouseMaterialWarehouseId_WarehouseMaterialMaterialId",
                table: "MaterialReceipts",
                newName: "IX_MaterialReceipts_WarehouseMaterialWarehouseId_WarehouseMaterialMaterialId");

            migrationBuilder.RenameIndex(
                name: "IX_WarehouseReceipts_WarehouseId",
                table: "MaterialReceipts",
                newName: "IX_MaterialReceipts_WarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_WarehouseReceipts_UserId",
                table: "MaterialReceipts",
                newName: "IX_MaterialReceipts_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_WarehouseReceipts_ProviderId",
                table: "MaterialReceipts",
                newName: "IX_MaterialReceipts_ProviderId");

            migrationBuilder.RenameIndex(
                name: "IX_WarehouseReceipts_MaterialId",
                table: "MaterialReceipts",
                newName: "IX_MaterialReceipts_MaterialId");

            migrationBuilder.RenameIndex(
                name: "IX_WarehouseReceipts_DocumentTypeId",
                table: "MaterialReceipts",
                newName: "IX_MaterialReceipts_DocumentTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaterialReceipts",
                table: "MaterialReceipts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialReceipts_DocumentTypes_DocumentTypeId",
                table: "MaterialReceipts",
                column: "DocumentTypeId",
                principalTable: "DocumentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialReceipts_Materials_MaterialId",
                table: "MaterialReceipts",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialReceipts_POSSystemUsers_UserId",
                table: "MaterialReceipts",
                column: "UserId",
                principalTable: "POSSystemUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialReceipts_Providers_ProviderId",
                table: "MaterialReceipts",
                column: "ProviderId",
                principalTable: "Providers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
    }
}
