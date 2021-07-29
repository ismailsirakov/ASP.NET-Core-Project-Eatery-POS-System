namespace EateryPOSSystem.Services
{
    using System.Linq;
    using EateryPOSSystem.Data.Models;
    using EateryPOSSystem.Services.Interfaces;

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
            .Any(r => r.Name == recipeName & r.StoreProductId == storeProductId & r.WarehouseMaterialId == WarehouseMaterialId);

        
        public void AddRecipe(string recipeName, int storeProductId, int warehouseMaterialId, decimal quantity)
        {
            var recipe = new Recipe
            {
                Name = recipeName,
                StoreProductId = storeProductId,
                WarehouseMaterialId = warehouseMaterialId,
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
    }
}
