namespace EateryPOSSystem.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using EateryPOSSystem.Models.Report;
    using EateryPOSSystem.Services.Interfaces;
    using static ControllerConstants;

    [Authorize(Roles = "Administrator, Seller, Storekeeper, Accountant")]
    public class ReportController : Controller
    {
        private readonly IDbService dbService;
        private readonly IReportService reportService;
        public ReportController(IDbService dbService, IReportService reportService)
        {
            this.dbService = dbService;
            this.reportService = reportService;
        }

        public IActionResult MaterialsInWarehouseFirstPage()
        {
            var materialInWarehouse = new MaterialInWarehouseFormModel
            {
                Warehouses = dbService.GetWarehouses()
            };

            return View(materialInWarehouse);
        }

        [HttpPost]

        public IActionResult MaterialsInWarehouseFirstPage(MaterialInWarehouseFormModel materialInWarehouse)
        {
            return RedirectToAction("MaterialsInWarehouseSecondPage", "Report", materialInWarehouse);
        }

        public IActionResult MaterialsInWarehouseSecondPage(MaterialInWarehouseFormModel materialInWarehouse)
        {
            materialInWarehouse.MaterialsInWarehouse = dbService.GetWarehouseMaterials()
                                                                .Where(wm => wm.WarehouseId == materialInWarehouse.WarehouseId)
                                                                .ToList();

            var warehouseName = dbService.GetWarehouses()
                                         .FirstOrDefault(w => w.Id == materialInWarehouse.WarehouseId)
                                         .Name;

            materialInWarehouse.WarehouseName = warehouseName;

            return View(materialInWarehouse);
        }
        public IActionResult BuyedMaterialsByProviderFirstPage()
        {
            var buyedMaterialsByProvider = new BuyedMaterialsFormModel
            {
                Providers = dbService.GetProviders().ToList()
            };

            return View(buyedMaterialsByProvider);
        }

        [HttpPost]

        public IActionResult BuyedMaterialsByProviderFirstPage(BuyedMaterialsFormModel buyedMaterials)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(nameof(buyedMaterials.ProviderId), notExistingModelInDB);

                buyedMaterials.Providers = dbService.GetProviders().ToList();

                return View(buyedMaterials);
            }

            if (buyedMaterials.FromDate > buyedMaterials.ToDate)
            {
                ModelState.AddModelError(nameof(buyedMaterials.ToDate), fromDateIsAfterToDate);

                buyedMaterials.Providers = dbService.GetProviders().ToList();

                return View(buyedMaterials);
            }

            return RedirectToAction("BuyedMaterialsByProviderSecondPage", "Report", buyedMaterials);
        }

        public IActionResult BuyedMaterialsByProviderSecondPage(BuyedMaterialsFormModel buyedMaterials)
        {
            buyedMaterials.Receipts = reportService.WarehouseReceipts(buyedMaterials.ProviderId,
                                                                      buyedMaterials.FromDate,
                                                                      buyedMaterials.ToDate);

            return View(buyedMaterials);
        }

        public IActionResult SoldProductsFromStore()
        {
            var soldProductsFormModel = new SoldProductsFormModel
            {
                Stores = dbService.GetStores().ToList()
            };

            return View(soldProductsFormModel);
        }

        [HttpPost]
        public IActionResult SoldProductsFromStore(SoldProductsFormModel soldProductsFromStore)
        {
            soldProductsFromStore.Stores = dbService.GetStores().ToList();

            if (!ModelState.IsValid)
            {
                return View(soldProductsFromStore);
            }

            if (soldProductsFromStore.FromDate > soldProductsFromStore.ToDate)
            {
                ModelState.AddModelError(nameof(soldProductsFromStore.ToDate), fromDateIsAfterToDate);

                return View(soldProductsFromStore);
            }

            return RedirectToAction("SoldProductsFromStoreReport", "Report", soldProductsFromStore);
        }

        public IActionResult SoldProductsFromStoreReport(SoldProductsFormModel soldProductsFromStore)
        {
            soldProductsFromStore.Stores = dbService.GetStores().ToList();

            var storeId = soldProductsFromStore.StoreId;

            var fromDate = soldProductsFromStore.FromDate;

            var toDate = soldProductsFromStore.ToDate;

            if (soldProductsFromStore.Cumilative)
            {
                soldProductsFromStore.SoldProducts = reportService.SoldProductsCumulative(storeId, fromDate, toDate);
            }
            else
            {
                soldProductsFromStore.SoldProducts = reportService.SoldProductsDetailed(storeId, fromDate, toDate);
            }

            soldProductsFromStore.StoreName = soldProductsFromStore.Stores.FirstOrDefault(s => s.Id == storeId).Name;

            return View(soldProductsFromStore);
        }

        public IActionResult Recipe()
        {
            var recipe = new RecipeFormModel
            {
                Recipes = dbService.GetRecipes().ToList()
            };

            return View(recipe);
        }

        [HttpPost]
        public IActionResult Recipe(RecipeFormModel recipe)
        {
            recipe.Recipes = dbService.GetRecipes().ToList();

            recipe.RecipeName = recipe.Recipes
                                      .FirstOrDefault(r => r.StoreProductId == recipe.StoreProductId)
                                      .Name;

            recipe.StoreName = recipe.Recipes
                                     .FirstOrDefault(r => r.StoreProductId == recipe.StoreProductId)
                                     .StoreName;

            recipe.ProductName = recipe.Recipes
                                       .FirstOrDefault(r => r.StoreProductId == recipe.StoreProductId)
                                       .StoreProductName;
            

            return RedirectToAction("RecipeReport","Report", recipe);
        }

        public IActionResult RecipeReport(RecipeFormModel recipeFormModel)
        {
            recipeFormModel.Recipes = dbService.GetRecipes().Where(r => r.StoreProductId == recipeFormModel.StoreProductId).ToList();

            var warehouses = dbService.GetWarehouses();

            foreach (var recipe in recipeFormModel.Recipes)
            {
                recipe.WarehouseName = warehouses.FirstOrDefault(w=>w.Id == recipe.WarehouseId).Name;
            }

            return View(recipeFormModel);
        }

        public IActionResult BillsForDate()
            => View();

        [HttpPost]
        public IActionResult BillsForDate(BillsForDateFormModel billsForDate)
        {

            return RedirectToAction("BillsForDateReport", "Report", billsForDate);
        }

        public IActionResult BillsForDateReport(BillsForDateFormModel billsForDate)
        {
            billsForDate.Bills = reportService.BillsForDate(billsForDate.DateTime, billsForDate.Closed).ToList();

            return View(billsForDate);
        }
    }
}
