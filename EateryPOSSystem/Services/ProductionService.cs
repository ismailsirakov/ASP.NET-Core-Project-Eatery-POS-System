namespace EateryPOSSystem.Services
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using AutoMapper;
    using Newtonsoft.Json;
    using EateryPOSSystem.Data.DataTransferObjects;
    using EateryPOSSystem.Data.Models;
    using EateryPOSSystem.Services.Interfaces;
    using EateryPOSSystem.Services.Models;

    public class ProductionService : IProductionService
    {
        private readonly IDbService dbService;
        private IMapper mapper;

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

        public bool IsProductExist(string productName)
            => dbService.GetProducts().Any(p => p.Name == productName);

        public bool IsProductWithIdExist(int productlId)
            => dbService.GetProducts()
            .Any(m => m.Id == productlId);

        public bool IsStoreProductWithIdExist(int storeProductlId)
            => dbService.GetStoreProducts()
            .Any(m => m.Id == storeProductlId);

        public bool IsMaterialInRecipeExist(string recipeName, int storeProductId, int warehouseMaterialId)
            => dbService.GetRecipes()
            .Any(r => r.Name == recipeName && r.StoreProductId == storeProductId && r.WarehouseMaterialId == warehouseMaterialId);

        public bool IsWarehouseMaterialExist(int warehouseId, int materialId)
            => dbService.GetWarehouseMaterials().Any(wm=> wm.WarehouseId == warehouseId &wm.MaterialId == materialId);

        public decimal CalculateCostOfProduct(int storeProductId)
        {
            var recipes = GetRecipesByStorProductId(storeProductId);

            var cost = 0m;

            foreach (var recipe in recipes)
            {
                var materialPrice = GetWarehouseMaterialsByWarehouseId(recipe.WarehouseId)
                    .FirstOrDefault(m => m.MaterialId == recipe.WarehouseMaterialId)
                    .Price;
                cost += materialPrice * recipe.MaterialQuantity;
            }

            return cost;
        }

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
            if (IsProductExist(productName))
            {
                return;
            }

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

        public void DecreaseMaterialsUsedInStoreProduct(int storeProductId, decimal soldProductQuantity)
        {
            var recipes = GetRecipesByStorProductId(storeProductId).ToList();

            foreach (var recipe in recipes)
            {
                var quantityInWarehouse = GetWarehouseMaterialsByWarehouseId(recipe.WarehouseId)
                    .FirstOrDefault(wm => wm.MaterialId == recipe.WarehouseMaterialId).Quantity;

                var quantity = quantityInWarehouse - recipe.MaterialQuantity * soldProductQuantity;

                dbService.UpdateWarehouseMaterialQuantity(recipe.WarehouseId, recipe.WarehouseMaterialId, quantity);
            }
        }

        public IEnumerable<WarehouseMaterialServiceModel> GetWarehouseMaterialsByWarehouseId(int warehouseId)
            => dbService.GetWarehouseMaterials().Where(wm => wm.WarehouseId == warehouseId);

        public IEnumerable<RecipeServiceModel> GetRecipesByStorProductId(int storeProductId)
            => dbService.GetRecipes().Where(r => r.StoreProductId == storeProductId);

        public void ImportProductionData()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<EateryPOSSystemProfile>());
            mapper = config.CreateMapper();

            var inputJson = File.ReadAllText(".\\Data\\Datasets\\seedingdata.json");

            var dtoInput = JsonConvert.DeserializeObject<ImportDTO>(inputJson);
            var inputData = mapper.Map<Import>(dtoInput);

            foreach (var product in inputData.Products)
            {
                AddProduct(product.Name, product.ProductTypeId);
            }

            foreach (var storeProduct in inputData.StoreProducts)
            {
                AddProductToStore(storeProduct.ProductId,
                                  storeProduct.StoreId,
                                  storeProduct.MeasurementId,
                                  storeProduct.Quantity,
                                  storeProduct.Price);
            }

            foreach (var recipe in inputData.Recipes)
            {
                AddRecipe(recipe.Name, recipe.StoreProductId, recipe.WarehouseMaterialWarehouseId, recipe.WarehouseMaterialMaterialId, recipe.MaterialQuantity);
            }
        }
    }
}
