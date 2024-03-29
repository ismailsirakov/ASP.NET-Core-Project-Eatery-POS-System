﻿namespace EateryPOSSystem.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using EateryPOSSystem.Models.Production;
    using EateryPOSSystem.Services.Interfaces;
    using static ControllerConstants;
    using static WebConstants;

    [Authorize(Roles = "Administrator, Accountant")]
    public class ProductionController : Controller
    {
        private readonly IDbService dbService;
        private readonly IProductionService productionService;

        public ProductionController(IDbService dbService,
                                    IProductionService productionService)
        {
            this.dbService = dbService;
            this.productionService = productionService;
        }

        public IActionResult AddProduct()
            => View(new AddProductFormModel
                        {
                            ProductTypes = dbService.GetProductTypes()
                        });

        [HttpPost]
        public IActionResult AddProduct(AddProductFormModel product)
        {
            product.ProductTypes = dbService.GetProductTypes().ToList(); ;

            var productTypeExist = product.ProductTypes
                                          .Any(p => p.Id == product.ProductTypeId);

            if (!productTypeExist)
            {
                ModelState.AddModelError(nameof(product.ProductTypeId), notExistingModelInDB);

                return View(product);
            }

            if (dbService.GetProducts().Any(p => p.Name == product.Name))
            {
                ModelState.AddModelError(nameof(product.Name), existingProductInDB);

                return View(product);
            }

            if (!ModelState.IsValid)
            {
                return View(product);
            }

            productionService.AddProduct(product.Name, product.ProductTypeId);

            TempData[GlobalMessageKey] = $"В база данни успешно се добави продукт '{product.Name}'.";

            return RedirectToAction("Index", "Home");
        }
        public IActionResult AddProductToStore()
        {
            var storeProduct = new AddProductToStoreFormModel
            {
                Measurements = dbService.GetMeasurements().ToList(),
                Products = dbService.GetProducts().ToList(),
                Stores = dbService.GetStores().ToList(),
            };

            return View(storeProduct);
        }

        [HttpPost]
        public IActionResult AddProductToStore(AddProductToStoreFormModel storeProduct)
        {
            storeProduct.Measurements = dbService.GetMeasurements().ToList();

            storeProduct.Products = dbService.GetProducts().ToList();

            storeProduct.Stores = dbService.GetStores().ToList();

            if (!storeProduct
                .Measurements
                .Any(m => m.Id == storeProduct.MeasurementId))
            {
                ModelState.AddModelError(nameof(storeProduct.MeasurementId), notExistingModelInDB);

                return View(storeProduct);
            }

            storeProduct.Stores = dbService.GetStores().ToList();

            if (!storeProduct
                .Stores
                .Any(s => s.Id == storeProduct.StoreId))
            {
                ModelState.AddModelError(nameof(storeProduct.StoreId), notExistingModelInDB);

                return View(storeProduct);
            }

            if (productionService.IsProductExistInStore(storeProduct.ProductId, storeProduct.StoreId))
            {
                ModelState.AddModelError(nameof(storeProduct.StoreId), existingProductInStoreInDB);

                return View(storeProduct);
            }

            if (!ModelState.IsValid)
            {
                return View(storeProduct);
            }

            productionService.AddProductToStore(storeProduct.ProductId,
                                                storeProduct.StoreId,
                                                storeProduct.MeasurementId,
                                                storeProduct.Quantity,
                                                storeProduct.Price);

            var storeName = storeProduct.Stores.FirstOrDefault(s=>s.Id == storeProduct.StoreId).Name;

            var productName = dbService.GetProducts().FirstOrDefault(p => p.Id == storeProduct.ProductId).Name;

            TempData[GlobalMessageKey] = $"В {storeName} успешно се добави продукт '{productName}'.";

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddRecipeFirstPage()
        {
            var recipe = new AddRecipeFormModel
            {
                StoreProducts = dbService.GetStoreProducts().ToList(),
                Warehouses = dbService.GetWarehouses().ToList()
        };

            return View(recipe);
        }

        [HttpPost]
        public IActionResult AddRecipeFirstPage(AddRecipeFormModel recipe)
        {
            recipe.StoreProducts = dbService.GetStoreProducts().ToList();

            if (!productionService.IsStoreProductWithIdExist(recipe.StoreProductId))
            {
                ModelState.AddModelError(nameof(recipe.StoreProductId), notExistingModelInDB);
                return View(recipe);
            }

            if (!ModelState.IsValid)
            {
                return View(recipe);
            }

            var storeName = recipe.StoreProducts
                                  .FirstOrDefault(p => p.Id == recipe.StoreProductId)
                                  .StoreName;

            var productName = recipe.StoreProducts
                                    .FirstOrDefault(p => p.Id == recipe.StoreProductId)
                                    .ProductName;

            recipe.RecipeInfo = $"Име на рецепта: {recipe.Name} - За продукт: {productName} - За обект: {storeName}";

            return RedirectToAction("AddRecipeSecondPage", "Production", recipe);
        }

        public IActionResult AddRecipeSecondPage(AddRecipeFormModel recipe)
        {
            recipe.WarehouseMaterials = productionService.GetWarehouseMaterialsByWarehouseId(recipe.WarehouseId).ToList();

            recipe.AddedMaterialsToRecipe = dbService.GetAddedMaterialsToRecipe(recipe.Name, recipe.StoreProductId).ToList();

            return View(recipe);
        }

        [HttpPost]
        public IActionResult AddRecipeSecondPage(string addButton, string saveButton, AddRecipeFormModel recipe)
        {
            recipe.AddedMaterialsToRecipe = dbService.GetAddedMaterialsToRecipe(recipe.Name, recipe.StoreProductId).ToList();

            if (saveButton != null && recipe.AddedMaterialsToRecipe.Any())
            {
                TempData[GlobalMessageKey] = $"В база данни успешно се добави рецепта с име '{recipe.Name}'.";

                return RedirectToAction("Index", "Home");
            }

            recipe.WarehouseMaterialWarehouseId = recipe.WarehouseId;

            recipe.WarehouseMaterials = productionService.GetWarehouseMaterialsByWarehouseId(recipe.WarehouseId).ToList();


            if (!productionService.IsWarehouseMaterialExist(recipe.WarehouseMaterialWarehouseId,
                                                            recipe.WarehouseMaterialMaterialId))
            {
                ModelState.AddModelError(nameof(recipe.WarehouseMaterialMaterialId), notExistingModelInDB);
                return View(recipe);
            }

            if (!ModelState.IsValid)
            {
                return View(recipe);
            }

            if (addButton != null)
            {
                if (productionService.IsMaterialInRecipeExist(recipe.Name,
                                                              recipe.StoreProductId,
                                                              recipe.WarehouseMaterialMaterialId))
                {
                    ModelState.AddModelError(nameof(recipe.MaterialQuantity), existingMaterialInRecipe);

                    return View(recipe);
                }

                productionService.AddRecipe(recipe.Name,
                                            recipe.StoreProductId,
                                            recipe.WarehouseMaterialWarehouseId,
                                            recipe.WarehouseMaterialMaterialId,
                                            recipe.MaterialQuantity);

                recipe.AddedMaterialsToRecipe = dbService.GetAddedMaterialsToRecipe(recipe.Name,
                                                                                    recipe.StoreProductId).ToList();

                return View(recipe);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
