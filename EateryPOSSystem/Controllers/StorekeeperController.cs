namespace EateryPOSSystem.Controllers
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using EateryPOSSystem.Models.Storekeeper;
    using EateryPOSSystem.Services.Interfaces;
    using EateryPOSSystem.Services.Models;
    using EateryPOSSystem.Infrastructure;
    using static ControllerConstants;
    using static WebConstants;

    [Authorize(Roles = "Administrator, Storekeeper")]
    public class StorekeeperController : Controller
    {
        private readonly IDbService dbService;
        private readonly IStorekeeperService storekeeperService;

        public StorekeeperController(IDbService dbService,
                                     IStorekeeperService storekeeperService)
        {
            this.dbService = dbService;
            this.storekeeperService = storekeeperService;
        }

        public IActionResult AddAddress()
            => View();

        [HttpPost]
        public IActionResult AddAddress(AddAddressFormModel address)
        {
            if (!ModelState.IsValid)
            {
                return View(address);
            }

            var addressExists = storekeeperService.IsAddressExist(address.AddressDetail);

            if (addressExists)
            {
                ModelState.AddModelError(nameof(address.AddressDetail), existingAddressInDB);

                return View(address);
            }

            storekeeperService.AddAddress(address.AddressDetail);

            if ((string)TempData["redirectFrom"] == "AddProvider")
            {
                var newProvider = new AddProviderFormModel
                {
                    Cities = dbService.GetCities(),
                    Addresses = dbService.GetAddresses()
                };

                return RedirectToAction("AddProvider", "Storekeeper", newProvider);
            }

            TempData[GlobalMessageKey] = $"В база данни успешно се добави адрес '{address.AddressDetail}'.";

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddCity() => View();

        [HttpPost]
        public IActionResult AddCity(AddCityFormModel city)
        {
            if (!ModelState.IsValid)
            {
                return View(city);
            }

            var cityExists = storekeeperService.IsCityExist(city.Name);

            if (cityExists)
            {
                ModelState.AddModelError(nameof(city.Name), existingCityInDB);

                return View(city);
            }

            storekeeperService.AddCity(city.Name, city.PostalCode);

            if ((string)TempData["redirectFrom"] == "AddProvider")
            {
                var newProvider = new AddProviderFormModel
                {
                    Cities = dbService.GetCities(),
                    Addresses = dbService.GetAddresses()
                };

                return RedirectToAction("AddProvider", "Storekeeper", newProvider);
            }

            TempData[GlobalMessageKey] = $"В база данни успешно се добави град '{city.Name}'.";

            return RedirectToAction("Index", "Home");
        }
        public IActionResult AddMaterialToWarehouse_1()
        {
            var newAddMaterialToWarehouseModel = new AddMaterialToWarehouseFormModel_1
            {
                Warehouses = dbService.GetWarehouses().ToList(),

                DocumentTypes = dbService.GetDocumentTypes().ToList(),

                Providers = dbService.GetProviders().ToList()
            };

            return View(newAddMaterialToWarehouseModel);
        }

        [HttpPost]
        public IActionResult AddMaterialToWarehouse_1(AddMaterialToWarehouseFormModel_1 warehouseMaterial)
        {
            if (!ModelState.IsValid)
            {
                warehouseMaterial.Warehouses = dbService.GetWarehouses().ToList();

                warehouseMaterial.DocumentTypes = dbService.GetDocumentTypes().ToList();

                warehouseMaterial.Providers = dbService.GetProviders().ToList();

                return View(warehouseMaterial);
            }

            warehouseMaterial.Warehouses = dbService.GetWarehouses().ToList();

            if (!warehouseMaterial
                .Warehouses
                .Any(w => w.Id == warehouseMaterial.WarehouseId))
            {
                ModelState.AddModelError(nameof(warehouseMaterial.WarehouseId), notExistingModelInDB);

                return View(warehouseMaterial);
            }

            warehouseMaterial.DocumentTypes = dbService.GetDocumentTypes().ToList();

            if (!warehouseMaterial
                .DocumentTypes
                .Any(dt => dt.Id == warehouseMaterial.DocumentTypeId))
            {
                ModelState.AddModelError(nameof(warehouseMaterial.DocumentTypeId), notExistingModelInDB);

                return View(warehouseMaterial);
            }

            warehouseMaterial.Providers = dbService.GetProviders().ToList();

            if (!warehouseMaterial
                .Providers
                .Any(dt => dt.Id == warehouseMaterial.ProviderId))
            {
                ModelState.AddModelError(nameof(warehouseMaterial.ProviderId), notExistingModelInDB);

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

            warehouseMaterial
                .ReceiptInfo = $"Доставчик: {providerName} " +
                $"- {documentTypeName} №{warehouseMaterial.DocumentNumber}" +
                $"/{warehouseMaterial.DocumentDate:d} -" +
                $" Склад: {warehouseName}";

            return RedirectToAction("AddMaterialToWarehouse_2", "Storekeeper", warehouseMaterial);
        }

        public IActionResult AddMaterialToWarehouse_2(AddMaterialToWarehouseFormModel_1 warehouseMaterial)
        {
            var addedMaterials = storekeeperService.GetAddedMaterials(warehouseMaterial.ProviderId,
                                                                      warehouseMaterial.DocumentNumber).ToList();

            var materials = dbService.GetMaterials().ToList();

            var newWarehouseMaterial = new AddMaterialToWarehouseFormModel_2
            {
                ReceiptInfo = warehouseMaterial.ReceiptInfo,
                ProviderId = warehouseMaterial.ProviderId,
                DocumentTypeId = warehouseMaterial.DocumentTypeId,
                DocumentNumber = warehouseMaterial.DocumentNumber,
                DocumentDate = warehouseMaterial.DocumentDate,
                WarehouseId = warehouseMaterial.WarehouseId,
                Materials = materials,
                AddedMaterials = addedMaterials
            };

            return View(newWarehouseMaterial);
        }

        [HttpPost]
        public IActionResult AddMaterialToWarehouse_2(string addButton,
                                                      string saveButton,
                                                      AddMaterialToWarehouseFormModel_2 warehouseMaterial)
        {
            warehouseMaterial.Materials = dbService.GetMaterials().ToList();

            warehouseMaterial.AddedMaterials = storekeeperService.GetAddedMaterials(warehouseMaterial.ProviderId,
                                                                                    warehouseMaterial.DocumentNumber).ToList();

            var materialExist = warehouseMaterial
                .Materials
                .Any(m => m.Id == warehouseMaterial.MaterialId);

            if (!materialExist)
            {
                ModelState.AddModelError(nameof(warehouseMaterial.MaterialId), notExistingModelInDB);

                return View(warehouseMaterial);
            }

            if (!ModelState.IsValid)
            {
                return View(warehouseMaterial);
            }

            var receiptNumber = 0;

            if (!warehouseMaterial.AddedMaterials.Any())
            {
                if (storekeeperService.DbTempWarehouseReceiptsEmpty())
                {
                    receiptNumber = 1;
                }
                else
                {
                    receiptNumber = storekeeperService.GetLastTempWarehouseReceiptNumber() + 1;
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
                var userId = ClaimsPrincipalExtensions.GetId(User);

                storekeeperService.AddTempWarehouseReceipt(receiptNumber,
                                                           providerId, 
                                                           documentTypeId,
                                                           documentNumber,
                                                           documentDate,
                                                           warehouseId,
                                                           quantity,
                                                           unitPrice,
                                                           materialId,
                                                           userId);

                warehouseMaterial.AddedMaterials = storekeeperService.GetAddedMaterials(providerId,
                                                                                        documentNumber).ToList();

                return View(warehouseMaterial);
            }

            if (saveButton != null)
            {
                var lastWarehouseReceiptInDb = storekeeperService.LastWarehouseReceiptInDb();

                var lastReceiptNumberInDb = 0;

                if (lastWarehouseReceiptInDb != null)
                {
                    lastReceiptNumberInDb = lastWarehouseReceiptInDb.ReceiptNumber;
                }

                var warehouseReceiptList = storekeeperService
                                            .AddWarehouseReceiptListByReceiptNumber(receiptNumber,
                                                                                    lastReceiptNumberInDb).ToList();

                storekeeperService.AddReceiptsMaterialsToWarehouse(warehouseReceiptList);

                TempData[GlobalMessageKey] = $"В база данни успешно се добавиха материали със стокова разписка № {receiptNumber}.";

                return RedirectToAction("Index", "Home");
            }

            return View(warehouseMaterial);
        }

        public IActionResult AddMaterial()
        {
            var newAddMaterialModel = new AddMaterialFormModel
            {
                Measurements = dbService.GetMeasurements().ToList()
            };

            return View(newAddMaterialModel);
        }

        [HttpPost]
        public IActionResult AddMaterial(AddMaterialFormModel material)
        {

            material.Measurements = dbService.GetMeasurements();

            var materialExists = storekeeperService.IsMaterialWhitNameExist(material.Name);

            if (materialExists)
            {
                ModelState.AddModelError(nameof(material.Name), existingMaterialInDB);

                return View(material);
            }

            if (!ModelState.IsValid)
            {
                return View(material);
            }

            storekeeperService.AddMaterial(material.Name, material.MeasurementId);

            TempData[GlobalMessageKey] = $"В база данни успешно се добави материал '{material.Name}'.";

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddProvider()
        {
            var newProvider = new AddProviderFormModel
            {
                Cities = dbService.GetCities().ToList(),
                Addresses = dbService.GetAddresses().ToList()
            };

            TempData["RedirectFrom"] = "AddProvider";

            return View(newProvider);
        }

        [HttpPost]
        public IActionResult AddProvider(AddProviderFormModel provider)
        {
            provider.Cities = dbService.GetCities().ToList();

            provider.Addresses = dbService.GetAddresses().ToList();

            var providerExists = storekeeperService.IsProviderExist(provider.Name);

            if (providerExists)
            {
                ModelState.AddModelError(nameof(provider.Name), existingProviderInDB);

                return View(provider);
            }

            if (!ModelState.IsValid)
            {
                return View(provider);
            }

            storekeeperService.AddProvider(provider.Name,
                                           provider.Number,
                                           provider.CityId, 
                                           provider.AddressId);

            TempData["RedirectFrom"] = string.Empty;

            TempData[GlobalMessageKey] = $"В база данни успешно се добави доставчик '{provider.Name}' с № {provider.Number}.";

            return RedirectToAction("Index", "Home");
        }

        public IActionResult TransferMaterialsFirstPage()
            => View(new TransferMaterialsFormModel
            {
                Warehouses = dbService.GetWarehouses().ToList()
            });

        [HttpPost]

        public IActionResult TransferMaterialsFirstPage(TransferMaterialsFormModel transfer)
        {
            transfer.TransferFromWarehouseName = dbService.GetWarehouses()
                                                    .FirstOrDefault(w => w.Id == transfer.TransferFromWarehouseId)
                                                    .Name;

            transfer.TransferToWarehouseName = dbService.GetWarehouses()
                .FirstOrDefault(w => w.Id == transfer.TransferToWarehouseId).Name;

            transfer.TransferedMaterials = storekeeperService.GetTransferedMaterials(transfer.TransferNumber).ToList();

            transfer.Warehouses = dbService.GetWarehouses().ToList();

            if (!transfer.Warehouses.Any(w=>w.Id == transfer.TransferFromWarehouseId))
            {
                ModelState.AddModelError(nameof(transfer.TransferFromWarehouseId), notExistingModelInDB);

                return View(transfer);
            }

            if (!transfer.Warehouses.Any(w => w.Id == transfer.TransferToWarehouseId))
            {
                ModelState.AddModelError(nameof(transfer.TransferToWarehouseId), notExistingModelInDB);

                return View(transfer);
            }

            if (transfer.TransferFromWarehouseId == transfer.TransferToWarehouseId)
            {
                ModelState.AddModelError(nameof(transfer.TransferToWarehouseId), warehouseCannotTransferToItself);

                return View(transfer);
            }

            if (!ModelState.IsValid)
            {
                return View(transfer);
            }

            var lastTransfer = dbService.GetTransfers().OrderBy(t => t.Id).LastOrDefault();

            if (lastTransfer is null)
            {
                transfer.TransferNumber = 1;
            }
            else if (!transfer.TransferedMaterials.Any())
            {
                transfer.TransferNumber = lastTransfer.Number + 1;
            }

            return RedirectToAction("TransferMaterialsSecondPage", "Storekeeper", transfer);
        }

        public IActionResult TransferMaterialsSecondPage(TransferMaterialsFormModel transfer)
        {
            transfer.WarehouseMaterials = dbService.GetWarehouseMaterials()
                .Where(wm => wm.WarehouseId == transfer.TransferFromWarehouseId).ToList();

            transfer.TransferedMaterials = storekeeperService.GetTransferedMaterials(transfer.TransferNumber).ToList();

            return View(transfer);
        }

        [HttpPost]

        public IActionResult TransferMaterialsSecondPage(string addButton,
                                                         string endButton,
                                                         TransferMaterialsFormModel transfer)
        {
            transfer.WarehouseMaterials = dbService.GetWarehouseMaterials()
                .Where(wm => wm.WarehouseId == transfer.TransferFromWarehouseId).ToList();

            transfer.QuantityInWarehouse = transfer.WarehouseMaterials
                .FirstOrDefault(w => w.MaterialId == transfer.TransferedMaterialId).Quantity;

            transfer.TransferedMaterials = storekeeperService.GetTransferedMaterials(transfer.TransferNumber).ToList();

            if (endButton != null)
            {
                TempData[GlobalMessageKey] = $"Успешно трансфериране на материали между складове.";

                return RedirectToAction("Index","Home");
            }

            if (transfer.QuantityToTransfer > transfer.QuantityInWarehouse)
            {
                ModelState.AddModelError(nameof(transfer.QuantityToTransfer), greaterQuantityThenExistInWarehouse);

                return View(transfer);
            }

            transfer.WarehouseMaterials = dbService.GetWarehouseMaterials()
                .Where(wm => wm.WarehouseId == transfer.TransferFromWarehouseId).ToList();

            transfer.TransferedMaterials = storekeeperService.GetTransferedMaterials(transfer.TransferNumber).ToList();

            if (!transfer.WarehouseMaterials.Any(wm=>wm.MaterialId == transfer.TransferedMaterialId))
            {
                ModelState.AddModelError(nameof(transfer.TransferedMaterialId), notExistingModelInDB);

                return View(transfer);
            }

            if (!ModelState.IsValid)
            {
                return View(transfer);
            }

            if (addButton != null)
            {
                var newTransfer = new TransferServiceModel
                {
                    Number = transfer.TransferNumber,
                    DateTime = DateTime.UtcNow,
                    FromWarehouseId = transfer.TransferFromWarehouseId,
                    ToWarehouseId = transfer.TransferToWarehouseId,
                    MaterialId = transfer.TransferedMaterialId,
                    Quantity = transfer.QuantityToTransfer,
                    UserId = ClaimsPrincipalExtensions.GetId(User)                   
                };

                storekeeperService.AddTransfer(newTransfer);

                storekeeperService.TransferMaterial(newTransfer);

                transfer.WarehouseMaterials = dbService.GetWarehouseMaterials()
                .Where(wm => wm.WarehouseId == transfer.TransferFromWarehouseId).ToList();

                transfer.TransferedMaterials = storekeeperService.GetTransferedMaterials(transfer.TransferNumber).ToList();

                return View(transfer);
            }

            return View(transfer);
        }
    }
}
