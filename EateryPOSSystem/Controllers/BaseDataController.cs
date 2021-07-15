namespace EateryPOSSystem.Controllers
{
    using EateryPOSSystem.Data;
    using EateryPOSSystem.Data.Models;
    using EateryPOSSystem.Models.BaseData;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    public class BaseDataController : Controller
    {
        private EateryPOSDbContext data;

        private const string existingModelInDB= "Already exists in database.";
        public BaseDataController(EateryPOSDbContext data)
        {
            this.data = data;
        }

        public IActionResult AddCity() => View();

        [HttpPost]
        public IActionResult AddCity(AddCityFormModel city)
        {
            var cityExists = data.Cities.Any(c=> c.Name == city.Name);

            if (cityExists)
            {
                ModelState.AddModelError(nameof(city.Name), existingModelInDB);

                return View(city);
            }

            if (!ModelState.IsValid)
            {
                return View(city);
            }

            var newCity = new City
            {
                Name = city.Name,
                PostalCode = city.PostalCode,
            };

            data.Cities.Add(newCity);
            data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddDocumentType() => View();

        [HttpPost]
        public IActionResult AddDocumentType(AddDocumentTypeFormModel documentType)
        {
            var documentExists = data.DocumentTypes.Any(dt => dt.Name == documentType.Name);

            if (documentExists)
            {
                ModelState.AddModelError(nameof(documentType.Name), existingModelInDB);

                return View(documentType);
            }

            if (!ModelState.IsValid)
            {
                return View(documentType);
            }

            var newDocumentType = new DocumentType
            {
                Name = documentType.Name
            };

            data.DocumentTypes.Add(newDocumentType);
            data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddMeasurement() => View();

        [HttpPost]
        public IActionResult AddMeasurement(AddMeasurementFormModel measurement)
        {
            var measurementExists = data.Measurements.Any(m => m.Name == measurement.Name);

            if (measurementExists)
            {
                ModelState.AddModelError(nameof(measurement.Name), existingModelInDB);

                return View(measurement);
            }

            if (!ModelState.IsValid)
            {
                return View(measurement);
            }

            var newMeasurement= new Measurement
            {
                Name = measurement.Name
            };

            data.Measurements.Add(newMeasurement);
            data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddPaymentType() => View();

        [HttpPost]
        public IActionResult AddPaymentType(AddPaymentTypeFormModel paymentType)
        {
            var paymentTypetExists = data.PaymentTypes.Any(pt => pt.Name == paymentType.Name);

            if (paymentTypetExists)
            {
                ModelState.AddModelError(nameof(paymentType.Name), existingModelInDB);

                return View(paymentType);
            }

            if (!ModelState.IsValid)
            {
                return View(paymentType);
            }

            var newPaymenttype = new PaymentType
            {
                Name = paymentType.Name
            };

            data.PaymentTypes.Add(newPaymenttype);
            data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddPosition() => View();

        [HttpPost]
        public IActionResult AddPosition(AddPositionFormModel position)
        {
            var positionExists = data.Positions.Any(p => p.Name == position.Name);

            if (positionExists)
            {
                ModelState.AddModelError(nameof(position.Name), existingModelInDB);

                return View(position);
            }

            if (!ModelState.IsValid)
            {
                return View(position);
            }

            var newPosition = new Position
            {
                Name = position.Name
            };

            data.Positions.Add(newPosition);
            data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddProductType() => View();

        [HttpPost]
        public IActionResult AddProductType(AddProductTypeFormModel productType)
        {
            var productTypeExists = data.ProductTypes.Any(pt => pt.Name == productType.Name);

            if (productTypeExists)
            {
                ModelState.AddModelError(nameof(productType.Name), existingModelInDB);

                return View(productType);
            }

            if (!ModelState.IsValid)
            {
                return View(productType);
            }

            var newProductType = new ProductType
            {
                Name = productType.Name
            };

            data.ProductTypes.Add(newProductType);
            data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddStore() => View();

        [HttpPost]
        public IActionResult AddStore(AddStoreFormModel store)
        {
            var storeExists = data.Stores.Any(pt => pt.Name == store.Name);

            if (storeExists)
            {
                ModelState.AddModelError(nameof(store.Name), existingModelInDB);

                return View(store);
            }

            if (!ModelState.IsValid)
            {
                return View(store);
            }

            var newStore = new Store
            {
                Name = store.Name
            };

            data.Stores.Add(newStore);
            data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddWarehouse() => View();

        [HttpPost]
        public IActionResult AddWarehouse(AddWarehouseFormModel warehouse)
        {
            var warehouseExists = data.Warehouses.Any(pt => pt.Name == warehouse.Name);

            if (warehouseExists)
            {
                ModelState.AddModelError(nameof(warehouse.Name), existingModelInDB);

                return View(warehouse);
            }

            if (!ModelState.IsValid)
            {
                return View(warehouse);
            }

            var newWarehouse = new Warehouse
            {
                Name = warehouse.Name
            };

            data.Warehouses.Add(newWarehouse);
            data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}
