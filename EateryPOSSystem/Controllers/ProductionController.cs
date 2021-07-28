namespace EateryPOSSystem.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using EateryPOSSystem.Models.Production;
    using EateryPOSSystem.Services.Interfaces;
    using static ControllerConstants;

    public class ProductionController : Controller
    {
        private readonly IDbService dbService;
        private readonly IProductionService production;

        public ProductionController(IDbService dbService,
                                    IProductionService production)
        {
            this.dbService = dbService;
            this.production = production;
        }
        public IActionResult AddProductToStore()
        {
            var storeProduct = new AddProductToStoreFormModel
            {
                Measurements = dbService.GetMeasurements(),
                Products = dbService.GetProducts(),
                Stores = dbService.GetStores(),
            };

            return View(storeProduct);
        }

        [HttpPost]
        public IActionResult AddProductToStore(AddProductToStoreFormModel storeProduct)
        {
            storeProduct.Measurements = dbService.GetMeasurements();

            if (!storeProduct
                .Measurements
                .Any(m => m.Id == storeProduct.MeasurementId))
            {
                ModelState.AddModelError(nameof(storeProduct.MeasurementId), notExistingModelInDB);

                return View(storeProduct);
            }

            storeProduct.Stores = dbService.GetStores();

            if (!storeProduct
                .Stores
                .Any(s => s.Id == storeProduct.StoreId))
            {
                ModelState.AddModelError(nameof(storeProduct.StoreId), notExistingModelInDB);

                return View(storeProduct);
            }

            if (production.IsProductExistInStore(storeProduct.ProductId, storeProduct.StoreId))
            {
                ModelState.AddModelError(nameof(storeProduct.StoreId), existingModelInDB);

                return View(storeProduct);
            }

            if (!ModelState.IsValid)
            {
                return View(storeProduct);
            }

            production.AddProductToStore(storeProduct.ProductId,
                                    storeProduct.StoreId,
                                    storeProduct.MeasurementId,
                                    storeProduct.Quantity,
                                    storeProduct.Price);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddRecipe()
        {
            var recipe = new AddRecipeFormModel
            {
                Products = dbService.GetProducts(),
                Materials = dbService.GetMaterials()
            };

            return View(recipe);
        }

        [HttpPost]
        public IActionResult AddRecipeFirstPage(AddRecipeFormModel recipe)
        {
            recipe.Products = dbService.GetProducts();

            if (production.IsProductWithIdExist(recipe.ProductId))
            {
                ModelState.AddModelError(nameof(recipe.ProductId), notExistingModelInDB);
                return View(recipe);
            }

            if (!ModelState.IsValid)
            {
                return View(recipe);
            }

            var productName = recipe.Products.FirstOrDefault(p => p.Id == recipe.ProductId).Name;

            recipe.RecipeInfo = $"Име на рецепта: {recipe.Name} - За продукт: {productName}";

            return RedirectToAction("AddRecipeSecondPage", "Production", recipe);
        }

        [HttpPost]
        public IActionResult AddRecipeSecondPage(string addButton, AddRecipeFormModel recipe)
        {

            recipe.Materials = dbService.GetMaterials();

            if (production.IsMaterialWithIdExist(recipe.MaterialId))
            {
                ModelState.AddModelError(nameof(recipe.MaterialId), notExistingModelInDB);
                return View(recipe);
            }

            if (production.IsMaterialInRecipeExist(recipe.Name, recipe.ProductId, recipe.MaterialId))
            {
                ModelState.AddModelError(nameof(recipe.MaterialId), existingMaterialInRecipe);

                return View(recipe);
            }

            if (!ModelState.IsValid)
            {
                return View(recipe);
            }

            recipe.AddedMaterialsToRecipe = dbService.GetAddedMaterialsToRecipe(recipe.Name, recipe.ProductId);

            if (addButton != null)
            {
                production.AddRecipe(recipe.Name, recipe.ProductId, recipe.MaterialId, recipe.MaterialQuantity);

                recipe.AddedMaterialsToRecipe = dbService.GetAddedMaterialsToRecipe(recipe.Name, recipe.ProductId);

                return View(recipe);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
