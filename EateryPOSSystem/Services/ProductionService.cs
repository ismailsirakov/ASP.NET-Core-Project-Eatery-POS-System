namespace EateryPOSSystem.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using EateryPOSSystem.Data.Models;
    using EateryPOSSystem.Services.Interfaces;
    using EateryPOSSystem.Services.Models;

    public class ProductionService : IProductionService
    {
        private readonly IDbService dbService;

        public ProductionService(IDbService dbService)
        {
            this.dbService = dbService;
        }
        
        public bool IsProductExistInStore(int productId, int storeId)
            => dbService.GetStoreProducts()
            .Any(p => p.ProductId == productId & p.StoreId == storeId);

        public bool IsMaterialWithIdExist(int materialId)
            => dbService.GetMaterials()
            .Any(m => m.Id == materialId);

        public bool IsProductWithIdExist(int productlId)
            => dbService.GetProducts()
            .Any(m => m.Id == productlId);

        public bool IsStoreProductWithIdExist(int storeProductlId)
            => dbService.GetStoreProducts()
            .Any(m => m.Id == storeProductlId);

        public bool IsMaterialInRecipeExist(string recipeName, int storeProductId, int WarehouseMaterialId)
            => dbService.GetRecipes()
            .Any(r => r.Name == recipeName & r.StoreProductId == storeProductId);

        public bool IsWarehouseMaterialExist(int warehouseId, int materialId)
            => dbService.GetWarehouseMaterials().Any(wm=> wm.WarehouseId == warehouseId &wm.MaterialId == materialId);

        public void AddRecipe(string recipeName,
                                int storeProductId,
                                int warehouseMaterialWarehouseId,
                                int warehouseMaterialMaterialId,
                                decimal quantity)
        {
            var recipe = new Recipe
            {
                Name = recipeName,
                StoreProductId = storeProductId,
                WarehouseMaterialWarehouseId = warehouseMaterialWarehouseId,
                WarehouseMaterialMaterialId = warehouseMaterialMaterialId,
                MaterialQuantity = quantity
            };

            dbService.AddRecipe(recipe);
        }

        public void AddProduct(string productName, int productTypeId)
        {
            var product = new Product
            {
                Name = productName,
                ProductTypeId = productTypeId
            };

            dbService.AddProduct(product);
        }

        public void AddProductToStore(int productId, int storeId, int measurementlId, decimal quantity, decimal price)
        {
            var storeProduct = new StoreProduct
            {
                ProductId = productId,
                StoreId = storeId,
                MeasurementId = measurementlId,
                Quantity = quantity,
                Price = price
            };

            dbService.AddStoreProduct(storeProduct);
        }

        public IEnumerable<WarehouseMaterialServiceModel> GetWarehouseMaterialsByWarehouseId(int warehouseId)
            => dbService.GetWarehouseMaterials().Where(wm => wm.WarehouseId == warehouseId);
    }
}
