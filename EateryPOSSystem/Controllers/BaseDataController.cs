namespace EateryPOSSystem.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using EateryPOSSystem.Models.BaseData;
    using EateryPOSSystem.Services.Interfaces;

    public class BaseDataController : Controller
    {
        private readonly IBaseDataService baseData;
        public BaseDataController(IBaseDataService baseData)
        {
            this.baseData = baseData;
        }

        public IActionResult AddCity() => View();

        [HttpPost]
        public IActionResult AddCity(AddCityFormModel city)
        {
            var cityExists = baseData.IsCityExist(city.Name);

            if (cityExists)
            {
                ModelState.AddModelError(nameof(city.Name), ControllerConstants.existingModelInDB);

                return View(city);
            }

            if (!ModelState.IsValid)
            {
                return View(city);
            }

            baseData.AddCity(city.Name, city.PostalCode);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddDocumentType() => View();

        [HttpPost]
        public IActionResult AddDocumentType(AddDocumentTypeFormModel documentType)
        {
            var documentExists = baseData.IsDocumentTypeExist(documentType.Name);

            if (documentExists)
            {
                ModelState.AddModelError(nameof(documentType.Name), ControllerConstants.existingModelInDB);

                return View(documentType);
            }

            if (!ModelState.IsValid)
            {
                return View(documentType);
            }

            baseData.AddDocumentType(documentType.Name);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddMeasurement() => View();

        [HttpPost]
        public IActionResult AddMeasurement(AddMeasurementFormModel measurement)
        {
            var measurementExists = baseData.IsMeasurementExist(measurement.Name);

            if (measurementExists)
            {
                ModelState.AddModelError(nameof(measurement.Name), ControllerConstants.existingModelInDB);

                return View(measurement);
            }

            if (!ModelState.IsValid)
            {
                return View(measurement);
            }

            baseData.AddMeasurement(measurement.Name);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddPaymentType() => View();

        [HttpPost]
        public IActionResult AddPaymentType(AddPaymentTypeFormModel paymentType)
        {
            var paymentTypetExists = baseData.IsPaymentTypeExist(paymentType.Name);

            if (paymentTypetExists)
            {
                ModelState.AddModelError(nameof(paymentType.Name), ControllerConstants.existingModelInDB);

                return View(paymentType);
            }

            if (!ModelState.IsValid)
            {
                return View(paymentType);
            }

            baseData.AddPaymentType(paymentType.Name);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddPosition() => View();

        [HttpPost]
        public IActionResult AddPosition(AddPositionFormModel position)
        {
            var positionExists = baseData.IsPositionExist(position.Name);

            if (positionExists)
            {
                ModelState.AddModelError(nameof(position.Name), ControllerConstants.existingModelInDB);

                return View(position);
            }

            if (!ModelState.IsValid)
            {
                return View(position);
            }

            baseData.AddPosition(position.Name);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddProductType() => View();

        [HttpPost]
        public IActionResult AddProductType(AddProductTypeFormModel productType)
        {
            var productTypeExists = baseData.IsProductTypeExist(productType.Name);

            if (productTypeExists)
            {
                ModelState.AddModelError(nameof(productType.Name), ControllerConstants.existingModelInDB);

                return View(productType);
            }

            if (!ModelState.IsValid)
            {
                return View(productType);
            }

            baseData.AddProductType(productType.Name);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddStore() => View();

        [HttpPost]
        public IActionResult AddStore(AddStoreFormModel store)
        {
            var storeExists = baseData.IsStoreExist(store.Name);

            if (storeExists)
            {
                ModelState.AddModelError(nameof(store.Name), ControllerConstants.existingModelInDB);

                return View(store);
            }

            if (!ModelState.IsValid)
            {
                return View(store);
            }

            baseData.AddStore(store.Name, store.TablesInStore);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddWarehouse() => View();

        [HttpPost]
        public IActionResult AddWarehouse(AddWarehouseFormModel warehouse)
        {
            var warehouseExists = baseData.IsWarehouseExist(warehouse.Name);

            if (warehouseExists)
            {
                ModelState.AddModelError(nameof(warehouse.Name), ControllerConstants.existingModelInDB);

                return View(warehouse);
            }

            if (!ModelState.IsValid)
            {
                return View(warehouse);
            }

            baseData.AddWarehouse(warehouse.Name);

            return RedirectToAction("Index", "Home");
        }
    }
}
