namespace EateryPOSSystem.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using EateryPOSSystem.Models.BaseData;
    using EateryPOSSystem.Services.Interfaces;
    using static ControllerConstants;
    using static WebConstants;

    [Authorize(Roles = "Administrator, Storekeeper, Accountant")]
    public class BaseDataController : Controller
    {
        private readonly IBaseDataService baseDataService;
        public BaseDataController(IBaseDataService baseDataService)
        {
            this.baseDataService = baseDataService;
        }
        
        public IActionResult AddDocumentType() => View();

        [HttpPost]
        public IActionResult AddDocumentType(AddDocumentTypeFormModel documentType)
        {
            var documentExists = baseDataService.IsDocumentTypeExist(documentType.Name);

            if (documentExists)
            {
                ModelState.AddModelError(nameof(documentType.Name), existingDocumentTypeInDB);

                return View(documentType);
            }

            if (!ModelState.IsValid)
            {
                return View(documentType);
            }

            baseDataService.AddDocumentType(documentType.Name);

            TempData[GlobalMessageKey] = $"В база данни успешно се добави тип документ '{documentType.Name}'.";

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddMeasurement() => View();

        [HttpPost]
        public IActionResult AddMeasurement(AddMeasurementFormModel measurement)
        {
            var measurementExists = baseDataService.IsMeasurementExist(measurement.Name);

            if (measurementExists)
            {
                ModelState.AddModelError(nameof(measurement.Name), existingMeasurementInDB);

                return View(measurement);
            }

            if (!ModelState.IsValid)
            {
                return View(measurement);
            }

            baseDataService.AddMeasurement(measurement.Name);

            TempData[GlobalMessageKey] = $"В база данни успешно се добави мерна единица '{measurement.Name}'.";

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddPaymentType() => View();

        [HttpPost]
        public IActionResult AddPaymentType(AddPaymentTypeFormModel paymentType)
        {
            var paymentTypetExists = baseDataService.IsPaymentTypeExist(paymentType.Name);

            if (paymentTypetExists)
            {
                ModelState.AddModelError(nameof(paymentType.Name), existingPaymentTypeInDB);

                return View(paymentType);
            }

            if (!ModelState.IsValid)
            {
                return View(paymentType);
            }

            baseDataService.AddPaymentType(paymentType.Name);

            TempData[GlobalMessageKey] = $"В база данни успешно се добави тип плащане '{paymentType.Name}'.";

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddPosition() => View();

        [HttpPost]
        public IActionResult AddPosition(AddPositionFormModel position)
        {
            var positionExists = baseDataService.IsPositionExist(position.Name);

            if (positionExists)
            {
                ModelState.AddModelError(nameof(position.Name), existingPositionInDB);

                return View(position);
            }

            if (!ModelState.IsValid)
            {
                return View(position);
            }

            baseDataService.AddPosition(position.Name);

            TempData[GlobalMessageKey] = $"В база данни успешно се добави длъжност '{position.Name}'.";

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddProductType() => View();

        [HttpPost]
        public IActionResult AddProductType(AddProductTypeFormModel productType)
        {
            var productTypeExists = baseDataService.IsProductTypeExist(productType.Name);

            if (productTypeExists)
            {
                ModelState.AddModelError(nameof(productType.Name), existingProductTypeInDB);

                return View(productType);
            }

            if (!ModelState.IsValid)
            {
                return View(productType);
            }

            baseDataService.AddProductType(productType.Name);

            TempData[GlobalMessageKey] = $"В база данни успешно се добави тип продукт '{productType.Name}'.";

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddStore() => View();

        [HttpPost]
        public IActionResult AddStore(AddStoreFormModel store)
        {
            var storeExists = baseDataService.IsStoreExist(store.Name);

            if (storeExists)
            {
                ModelState.AddModelError(nameof(store.Name), existingStoreInDB);

                return View(store);
            }

            if (!ModelState.IsValid)
            {
                return View(store);
            }

            baseDataService.AddStore(store.Name, store.TablesInStore);

            TempData[GlobalMessageKey] = $"В база данни успешно се добави обект '{store.Name}' с {store.TablesInStore} маси.";

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddWarehouse() => View();

        [HttpPost]
        public IActionResult AddWarehouse(AddWarehouseFormModel warehouse)
        {
            var warehouseExists = baseDataService.IsWarehouseExist(warehouse.Name);

            if (warehouseExists)
            {
                ModelState.AddModelError(nameof(warehouse.Name), existingWarehouseInDB);

                return View(warehouse);
            }

            if (!ModelState.IsValid)
            {
                return View(warehouse);
            }

            baseDataService.AddWarehouse(warehouse.Name);

            TempData[GlobalMessageKey] = $"В база данни успешно се добави склад '{warehouse.Name}'.";

            return RedirectToAction("Index", "Home");
        }
    }
}
