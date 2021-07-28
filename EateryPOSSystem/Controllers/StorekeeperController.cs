namespace EateryPOSSystem.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using EateryPOSSystem.Data;
    using EateryPOSSystem.Models.Storekeeper;
    using EateryPOSSystem.Services.Interfaces;

    public class StorekeeperController : Controller
    {
        private readonly EateryPOSDbContext data;
        private readonly IStorekeeperService storekeeper;
        private readonly IDbService dbService;

        public StorekeeperController(EateryPOSDbContext data, IStorekeeperService storekeeper, IDbService dbService)
        {
            this.data = data;
            this.storekeeper = storekeeper;
            this.dbService = dbService;
        }

        public IActionResult AddAddress()
            => View();

        [HttpPost]
        public IActionResult AddAddress(AddAddressFormModel address)
        {
            var addressExists = storekeeper.IsAddressExist(address.AddressDetail);

            if (addressExists)
            {
                ModelState.AddModelError(nameof(address.AddressDetail), ControllerConstants.existingModelInDB);

                return View(address);
            }

            if (!ModelState.IsValid)
            {
                return View(address);
            }

            storekeeper.AddAddress(address.AddressDetail);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddMaterialToWarehouse_1()
        {
            var newAddMaterialToWarehouseModel = new AddMaterialToWarehouseFormModel_1
            {
                Warehouses = dbService.GetWarehouses(),

                DocumentTypes = dbService.GetDocumentTypes(),

                Providers = dbService.GetProviders()
            };

            return View(newAddMaterialToWarehouseModel);
        }

        [HttpPost]
        public IActionResult AddMaterialToWarehouse_1(AddMaterialToWarehouseFormModel_1 warehouseMaterial)
        {

            warehouseMaterial.Warehouses = dbService.GetWarehouses();

            if (!warehouseMaterial
                .Warehouses
                .Any(w => w.Id == warehouseMaterial.WarehouseId))
            {
                ModelState.AddModelError(nameof(warehouseMaterial.WarehouseId), ControllerConstants.notExistingModelInDB);

                return View(warehouseMaterial);
            }

            warehouseMaterial.DocumentTypes = dbService.GetDocumentTypes();

            if (!warehouseMaterial
                .DocumentTypes
                .Any(dt => dt.Id == warehouseMaterial.DocumentTypeId))
            {
                ModelState.AddModelError(nameof(warehouseMaterial.DocumentTypeId), ControllerConstants.notExistingModelInDB);

                return View(warehouseMaterial);
            }

            warehouseMaterial.Providers = dbService.GetProviders();

            if (!warehouseMaterial
                .Providers
                .Any(dt => dt.Id == warehouseMaterial.ProviderId))
            {
                ModelState.AddModelError(nameof(warehouseMaterial.ProviderId), ControllerConstants.notExistingModelInDB);

                return View(warehouseMaterial);
            }

            var providerName = warehouseMaterial
                .Providers
                .FirstOrDefault(p => p.Id == warehouseMaterial.ProviderId)
                .Name;

            var warehouseName = warehouseMaterial
                .Warehouses
                .FirstOrDefault(w => w.Id == warehouseMaterial.WarehouseId)
                .Name;

            var documentTypeName = warehouseMaterial
                .DocumentTypes
                .FirstOrDefault(dt => dt.Id == warehouseMaterial.DocumentTypeId)
                .Name;

            warehouseMaterial.ReceiptInfo = $"Доставчик: {providerName} - {documentTypeName} №{warehouseMaterial.DocumentNumber}/{warehouseMaterial.DocumentDate:d} - Склад: {warehouseName}";

            return RedirectToAction("AddMaterialToWarehouse_2", "Storekeeper", warehouseMaterial);
        }

        public IActionResult AddMaterialToWarehouse_2(AddMaterialToWarehouseFormModel_1 warehouseMaterial)
        {
            var addedMaterials = storekeeper.GetAddedMaterials(warehouseMaterial.ProviderId, warehouseMaterial.DocumentNumber);

            var newWarehouseMaterial = new AddMaterialToWarehouseFormModel_2
            {
                ReceiptInfo = warehouseMaterial.ReceiptInfo,
                ProviderId = warehouseMaterial.ProviderId,
                DocumentTypeId = warehouseMaterial.DocumentTypeId,
                DocumentNumber = warehouseMaterial.DocumentNumber,
                DocumentDate = warehouseMaterial.DocumentDate,
                WarehouseId = warehouseMaterial.WarehouseId,
                Materials = dbService.GetMaterials(),
                AddedMaterials = addedMaterials
            };

            return View(newWarehouseMaterial);
        }

        [HttpPost]
        public IActionResult AddMaterialToWarehouse_2(string addButton, string saveButton, AddMaterialToWarehouseFormModel_2 warehouseMaterial)
        {
            warehouseMaterial.Materials = dbService.GetMaterials();

            warehouseMaterial.AddedMaterials = storekeeper.GetAddedMaterials(warehouseMaterial.ProviderId, warehouseMaterial.DocumentNumber);

            var materialExist = warehouseMaterial
                .Materials
                .Any(m => m.Id == warehouseMaterial.MaterialId);

            if (!materialExist)
            {
                ModelState.AddModelError(nameof(warehouseMaterial.MaterialId), ControllerConstants.notExistingModelInDB);

                return View(warehouseMaterial);
            }

            if (!ModelState.IsValid)
            {
                return View(warehouseMaterial);
            }

            var receiptNumber = 0;

            if (!warehouseMaterial.AddedMaterials.Any())
            {
                if (storekeeper.DbTempWarehouseReceiptsEmpty())
                {
                    receiptNumber = 1;
                }
                else
                {
                    receiptNumber = data.TempWarehouseReceipts.OrderBy(t => t.Id).Last().ReceiptNumber + 1;
                }
            }
            else
            {
                receiptNumber = warehouseMaterial.AddedMaterials.First().ReceiptNumber;
            }

            if (addButton != null)
            {
                var providerId = warehouseMaterial.ProviderId;
                var documentTypeId = warehouseMaterial.DocumentTypeId;
                var documentNumber = warehouseMaterial.DocumentNumber;
                var documentDate = warehouseMaterial.DocumentDate;
                var warehouseId = warehouseMaterial.WarehouseId;
                var quantity = warehouseMaterial.Quantity;
                var unitPrice = warehouseMaterial.UnitPrice;
                var materialId = warehouseMaterial.MaterialId;

                storekeeper.AddTempWarehouseReceipt(receiptNumber, providerId, documentTypeId, documentNumber, documentDate, warehouseId, quantity, unitPrice, materialId);

                warehouseMaterial.AddedMaterials = storekeeper.GetAddedMaterials(providerId, documentNumber);

                return View(warehouseMaterial);
            }

            if (saveButton != null)
            {
                var lastWarehouseReceiptInDb = storekeeper.LastWarehouseReceiptInDb();

                var lastReceiptNumberInDb = 0;

                if (lastWarehouseReceiptInDb != null)
                {
                    lastReceiptNumberInDb = lastWarehouseReceiptInDb.ReceiptNumber;
                }

                var warehouseReceiptList = storekeeper.AddWarehouseReceiptListByReceiptNumber(receiptNumber, lastReceiptNumberInDb);

                storekeeper.AddReceiptsMaterialsToWarehouse(warehouseReceiptList);

                return RedirectToAction("Index", "Home");
            }

            return View(warehouseMaterial);
        }

        public IActionResult AddMaterial()
        {
            var newAddMaterialModel = new AddMaterialFormModel
            {
                Measurements = dbService.GetMeasurements()
            };

            return View(newAddMaterialModel);
        }

        [HttpPost]
        public IActionResult AddMaterial(AddMaterialFormModel material)
        {

            material.Measurements = dbService.GetMeasurements();

            var materialExists = storekeeper.IsMaterialExist(material.Name);

            if (materialExists)
            {
                ModelState.AddModelError(nameof(material.Name), ControllerConstants.existingModelInDB);

                return View(material);
            }

            if (!ModelState.IsValid)
            {
                return View(material);
            }

            storekeeper.AddMaterial(material.Name, material.MeasurementId);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddProvider()
        {
            var newProvider = new AddProviderFormModel
            {
                Cities = dbService.GetCities(),
                Addresses = dbService.GetAddresses()
            };

            return View(newProvider);
        }

        [HttpPost]
        public IActionResult AddProvider(AddProviderFormModel provider)
        {
            provider.Cities = dbService.GetCities();
            provider.Addresses = dbService.GetAddresses();

            var providerlExists = storekeeper.IsProviderExist(provider.Name);

            if (providerlExists)
            {
                ModelState.AddModelError(nameof(provider.Name), ControllerConstants.existingModelInDB);

                return View(provider);
            }

            if (!ModelState.IsValid)
            {
                return View(provider);
            }

            storekeeper.AddProvider(provider.Name, provider.Number, provider.CityId, provider.AddressId);

            return RedirectToAction("Index", "Home");
        }

    }
}
