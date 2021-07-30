namespace EateryPOSSystem.Services.Interfaces
{
    using System.Collections.Generic;
    using EateryPOSSystem.Services.Models;

    public interface IProductionService
    {
        bool IsProductExistInStore(int productId, int storeId);

        bool IsMaterialWithIdExist(int materialId);

        bool IsWarehouseMaterialExist(int warehouseId, int materialId);

        bool IsProductWithIdExist(int productId);

        bool IsStoreProductWithIdExist(int storeProductlId);

        bool IsMaterialInRecipeExist(string recipeName, int productId, int materialId);

        void AddRecipe(string recipeName, int storeProductId, int warehouseMaterialWarehouseId, int warehouseMaterialMaterialId, decimal quantity);

        void AddProduct(string productName, int productTypeId);

        void AddProductToStore(int productId, int storeId, int measurementlId, decimal quantity, decimal price);

        IEnumerable<WarehouseMaterialServiceModel> GetWarehouseMaterialsByWarehouseId(int warehouseId);
    }
}
