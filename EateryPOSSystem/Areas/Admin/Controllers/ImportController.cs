namespace EateryPOSSystem.Areas.Admin.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using EateryPOSSystem.Services.Interfaces;
    using EateryPOSSystem.Infrastructure;
    using static WebConstants;

    [Area("Admin")]
    [Authorize(Roles = "Administrator")]

    public class ImportController : Controller
    {
        private readonly IBaseDataService baseDataService;
        private readonly IDbService dbService;
        private readonly IProductionService productionService;
        private readonly IStorekeeperService storekeeperService;
        public ImportController(IBaseDataService baseDataService,
                                IDbService dbService,
                                IStorekeeperService storekeeperService,
                                IProductionService productionService)
        {
            this.baseDataService = baseDataService;
            this.dbService = dbService;
            this.productionService = productionService;
            this.storekeeperService = storekeeperService;
        }

        public IActionResult BaseData()
        {
            baseDataService.ImportBaseData();

            var documentTypesCount = dbService.GetDocumentTypes().Count();

            var measurementsCount = dbService.GetMeasurements().Count();

            var paymentTypesCount = dbService.GetPaymentTypes().Count();

            var positionsCount = dbService.GetPositions().Count();

            var productTypesCount = dbService.GetProductTypes().Count();

            var storesCount = dbService.GetStores().Count();

            var warehousesCount = dbService.GetWarehouses().Count();

            TempData[GlobalMessageKey] = $"В база данни успешно се импортираха " +
                $"{documentTypesCount} типа за документ, {measurementsCount} мерни единици, " +
                $"{paymentTypesCount} типа за плащане, {positionsCount} длъжности, " +
                $"{productTypesCount} типа за продукт, {storesCount} обекта и {warehousesCount} склада.";

            return Redirect("/Home/Index");
        }

        public IActionResult StorekeeperData()
        {
            storekeeperService.ImportStorekeeperData(User.GetId());

            var addressesCount = dbService.GetAddresses().Count();

            var citiesCount = dbService.GetCities().Count();

            var materialsCount = dbService.GetMaterials().Count();

            var providersCount = dbService.GetProviders().Count();

            TempData[GlobalMessageKey] = $"В база данни успешно се импортираха {addressesCount} адреса, " +
                $"{citiesCount} града, {materialsCount} материала и {providersCount} доставчика.";

            return Redirect("/Home/Index");
        }

        public IActionResult ProductionData()
        {
            productionService.ImportProductionData();

            var productsCount = dbService.GetProducts().Count();

            var storeProductsCount = dbService.GetStoreProducts().Count();

            var recipesCount = dbService.GetRecipes().Select(r=>r.Name).Distinct().Count();

            TempData[GlobalMessageKey] = $"В база данни успешно се импортираха {productsCount} продукта, " +
                $"{storeProductsCount} продукта към обект и {recipesCount} рецепти.";

            return Redirect("/Home/Index");
        }        
    }
}