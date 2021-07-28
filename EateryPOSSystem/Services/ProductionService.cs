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

        public bool IsMaterialInRecipeExist(string recipeName, int productId, int materialId)
            => dbService.GetRecipes()
            .Any(r => r.Name == recipeName & r.ProductId == productId & r.MaterialId == materialId);

        
        public void AddRecipe(string recipeName, int productId, int materialId, decimal quantity)
        {
            var recipe = new Recipe
            {
                Name = recipeName,
                ProductId = productId,
                MaterialId = materialId,
                MaterialQuantity = quantity
            };

            dbService.AddRecipe(recipe);
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
