namespace EateryPOSSystem.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using EateryPOSSystem.Models.Production;
    using EateryPOSSystem.Services.Interfaces;

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
            var product = new AddProductToStoreFormModel
            {
                Measurements = dbService.GetMeasurements(),
                ProductTypes = dbService.GetProductTypes(),
                Stores = dbService.GetStores(),
            };

            return View(product);
        }

        [HttpPost]
        public IActionResult AddProductToStore(AddProductToStoreFormModel product)
        {
            product.Measurements = dbService.GetMeasurements();

            if (!product
                .Measurements
                .Any(m => m.Id == product.MeasurementId))
            {
                ModelState.AddModelError(nameof(product.MeasurementId), ControllerConstants.notExistingModelInDB);

                return View(product);
            }

            product.ProductTypes = dbService.GetProductTypes();

            if (!product
                .ProductTypes
                .Any(p => p.Id == product.ProductTypeId))
            {
                ModelState.AddModelError(nameof(product.ProductTypeId), ControllerConstants.notExistingModelInDB);

                return View(product);
            }

            product.Stores = dbService.GetStores();

            if (!product
                .Stores
                .Any(s => s.Id == product.StoreId))
            {
                ModelState.AddModelError(nameof(product.StoreId), ControllerConstants.notExistingModelInDB);

                return View(product);
            }

            if (dbService.IsProductExistInStore(product.Name, product.StoreId))
            {
                ModelState.AddModelError(nameof(product.StoreId), ControllerConstants.existingModelInDB);

                return View(product);
            }

            if (!ModelState.IsValid)
            {
                return View(product);
            }

            production.AddProduct(product.Name,
                                    product.StoreId,
                                    product.MeasurementId,
                                    product.ProductTypeId,
                                    product.Quantity, 
                                    product.Price);

            return RedirectToAction("Index","Home");
        }
    }
}
