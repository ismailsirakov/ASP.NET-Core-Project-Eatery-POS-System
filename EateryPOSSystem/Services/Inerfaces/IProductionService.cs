namespace EateryPOSSystem.Services.Interfaces
{
    using System.Collections.Generic;
    using EateryPOSSystem.Services.Models;

    public interface IProductionService
    {
        bool IsMaterialInRecipeExist(string recipeName, int productId, int materialId);

        bool IsMaterialWithIdExist(int materialId);

        bool IsProductExist(string productName);

        bool IsProductExistInStore(int productId, int storeId);

        bool IsProductWithIdExist(int productId);

        bool IsStoreProductWithIdExist(int storeProductlId);

        bool IsWarehouseMaterialExist(int warehouseId, int materialId);

        void AddProduct(string productName, int productTypeId);

        void AddProductToStore(int productId, int storeId, int measurementlId, decimal quantity, decimal price);

        void AddRecipe(string recipeName, int storeProductId, int warehouseMaterialWarehouseId, int warehouseMaterialMaterialId, decimal quantity);

        void DecreaseMaterialsUsedInStoreProduct(int storeProductId, decimal soldProductQuantity);

        IEnumerable<WarehouseMaterialServiceModel> GetWarehouseMaterialsByWarehouseId(int warehouseId);

        decimal CalculateCostOfProduct(int storeProductId);

        IEnumerable<RecipeServiceModel> GetRecipesByStorProductId(int storeProductId);

        void ImportProductionData();
    }
}
