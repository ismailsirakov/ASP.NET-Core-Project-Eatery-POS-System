namespace EateryPOSSystem.Controllers
{
    using EateryPOSSystem.Data;
    using EateryPOSSystem.Data.Models;
    using EateryPOSSystem.Models.Storekeeper;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StorekeeperController : Controller
    {
        private EateryPOSDbContext data;

        private const string existingModelInDB = "Already exists in database.";

        private const string notExistingModelInDB = "Selected item not exists in database.";

        public StorekeeperController(EateryPOSDbContext data)
        {
            this.data = data;
        }

        public IActionResult AddAddress()
            => View();

        [HttpPost]
        public IActionResult AddAddress(AddAddressFormModel address)
        {
            var addressExists = data.Addresses.Any(a => a.AddressDetails == address.AddressDetail);

            if (addressExists)
            {
                ModelState.AddModelError(nameof(address.AddressDetail), existingModelInDB);

                return View(address);
            }

            if (!ModelState.IsValid)
            {
                return View(address);
            }

            var newAddress = new Address
            {
                AddressDetails = address.AddressDetail
            };

            data.Addresses.Add(newAddress);
            data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddMaterialToWarehouse_1()
        {
            var newAddMaterialToWarehouseModel = new AddMaterialToWarehouseFormModel_1
            {
                Warehouses = GetWarehouses(),

                DocumentTypes = GetDocumentTypes(),

                Providers = GetProviders()
            };

            return View(newAddMaterialToWarehouseModel);
        }

        [HttpPost]
        public IActionResult AddMaterialToWarehouse_1(AddMaterialToWarehouseFormModel_1 warehouseMaterial)
        {

            warehouseMaterial.Warehouses = GetWarehouses();

            if (!warehouseMaterial
                .Warehouses
                .Any(w => w.Id == warehouseMaterial.WarehouseId))
            {
                ModelState.AddModelError(nameof(warehouseMaterial.WarehouseId), notExistingModelInDB);

                return View(warehouseMaterial);
            }

            warehouseMaterial.DocumentTypes = GetDocumentTypes();

            if (!warehouseMaterial
                .DocumentTypes
                .Any(dt => dt.Id == warehouseMaterial.DocumentTypeId))
            {
                ModelState.AddModelError(nameof(warehouseMaterial.DocumentTypeId), notExistingModelInDB);

                return View(warehouseMaterial);
            }

            warehouseMaterial.Providers = GetProviders();

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

            warehouseMaterial.ReceiptInfo = $"Доставчик: {providerName} - {documentTypeName} №{warehouseMaterial.DocumentNumber}/{warehouseMaterial.DocumentDate:d} - Склад: {warehouseName}";

            return RedirectToAction("AddMaterialToWarehouse_2", "Storekeeper", warehouseMaterial);
        }

        public IActionResult AddMaterialToWarehouse_2(AddMaterialToWarehouseFormModel_1 warehouseMaterial)
        {
            var addedMaterials = GetAddedMaterials(warehouseMaterial.ProviderId, warehouseMaterial.DocumentNumber);

            var newWarehouseMaterial = new AddMaterialToWarehouseFormModel_2
            {
                ReceiptInfo = warehouseMaterial.ReceiptInfo,
                ProviderId = warehouseMaterial.ProviderId,
                DocumentTypeId = warehouseMaterial.DocumentTypeId,
                DocumentNumber = warehouseMaterial.DocumentNumber,
                DocumentDate = warehouseMaterial.DocumentDate,
                WarehouseId = warehouseMaterial.WarehouseId,
                Materials = GetMaterials(),
                AddedMaterials = addedMaterials
            };

            return View(newWarehouseMaterial);
        }

        [HttpPost]
        public IActionResult AddMaterialToWarehouse_2(string addButton, string saveButton, AddMaterialToWarehouseFormModel_2 warehouseMaterial)
        {
            warehouseMaterial.Materials = GetMaterials();

            warehouseMaterial.AddedMaterials = GetAddedMaterials(warehouseMaterial.ProviderId, warehouseMaterial.DocumentNumber);

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
                if (!data.TempWarehouseReceipts.Any())
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
                var newWarehouseReceipt = new TempWarehouseReceipt
                {
                    ProviderId = warehouseMaterial.ProviderId,
                    DocumentTypeId = warehouseMaterial.DocumentTypeId,
                    DocumentNumber = warehouseMaterial.DocumentNumber,
                    DocumentDate = warehouseMaterial.DocumentDate,
                    WarehouseId = warehouseMaterial.DocumentTypeId,
                    Quantity = warehouseMaterial.Quantity,
                    UnitPrice = warehouseMaterial.UnitPrice,
                    MaterialId = warehouseMaterial.MaterialId,
                    ReceiptNumber = receiptNumber
                };

                data.TempWarehouseReceipts.Add(newWarehouseReceipt);
                data.SaveChanges();

                var providerId = warehouseMaterial.ProviderId;

                var documentNumber = warehouseMaterial.DocumentNumber;

                warehouseMaterial.AddedMaterials = GetAddedMaterials(providerId, documentNumber);

                return View(warehouseMaterial);
            }

            if (saveButton != null)
            {
                var lastWarehouseReceiptInDb = data
                    .WarehouseReceipts
                    .OrderBy(wr => wr.ReceiptNumber)
                    .LastOrDefault();

                var lastReceiptNumberInDb = 1;

                if (lastWarehouseReceiptInDb != null)
                {
                    lastReceiptNumberInDb = lastWarehouseReceiptInDb.ReceiptNumber;
                }

                var warehouseReceiptList = data
                    .TempWarehouseReceipts
                    .Where(twr => twr.ReceiptNumber == receiptNumber)
                    .Select(twr => new WarehouseReceipt
                    {
                        ReceiptNumber = lastReceiptNumberInDb + 1,
                        ProviderId = twr.ProviderId,
                        DocumentTypeId = twr.DocumentTypeId,
                        DocumentNumber = twr.DocumentNumber,
                        DocumentDate = twr.DocumentDate,
                        WarehouseId = twr.WarehouseId,
                        MaterialId = twr.MaterialId,
                        Quantity = twr.Quantity,
                        UnitPrice = twr.UnitPrice,
                        UserId = 1,
                        DateTime = DateTime.UtcNow
                    })
                    .ToList();

                data.WarehouseReceipts.AddRange(warehouseReceiptList);
                data.TempWarehouseReceipts.RemoveRange(data.TempWarehouseReceipts.Where(twr => twr.ReceiptNumber == receiptNumber));
                data.SaveChanges();

                var currentWarehouse = warehouseReceiptList
                    .FirstOrDefault()
                    .WarehouseId;

                var materialIdsInWarehouse = data
                    .WarehouseMaterials
                    .Where(wm => wm.WarehouseId == currentWarehouse)
                    .Select(wm => wm.MaterialId)
                    .ToList();

                foreach (var receipt in warehouseReceiptList)
                {
                    var materialId = receipt.MaterialId;

                    if (!materialIdsInWarehouse.Contains(materialId))
                    {
                        var newWarehouseMaterial = new WarehouseMaterial
                        {
                            WarehouseId = currentWarehouse,
                            MaterialId = materialId
                        };

                        data.WarehouseMaterials.Add(newWarehouseMaterial);
                        data.SaveChanges();
                    }

                    var currentMaterialQuantity = data.WarehouseMaterials.FirstOrDefault(wm => wm.MaterialId == materialId).Quantity;

                    var currentMaterialPrice = data.WarehouseMaterials.FirstOrDefault(wm => wm.MaterialId == materialId).Price;

                    var currentTotalAmount = currentMaterialQuantity * currentMaterialPrice;

                    currentMaterialQuantity += receipt.Quantity;

                    currentMaterialPrice = (currentTotalAmount + receipt.Quantity * receipt.UnitPrice) / currentMaterialQuantity;

                    data.WarehouseMaterials.FirstOrDefault(wm => wm.MaterialId == receipt.MaterialId).Quantity = currentMaterialQuantity;
                    data.WarehouseMaterials.FirstOrDefault(wm => wm.MaterialId == receipt.MaterialId).Price = currentMaterialPrice;
                }

                data.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return View(warehouseMaterial);
        }

        public IActionResult AddMaterial()
        {
            var newAddMaterialModel = new AddMaterialFormModel
            {
                Measurements = GetMeasurements()
            };

            return View(newAddMaterialModel);
        }

        [HttpPost]
        public IActionResult AddMaterial(AddMaterialFormModel material)
        {

            material.Measurements = GetMeasurements();

            var materialExists = data
                .Materials
                .Any(m => m.Name == material.Name);

            if (materialExists)
            {
                ModelState.AddModelError(nameof(material.Name), existingModelInDB);

                return View(material);
            }

            if (!ModelState.IsValid)
            {
                return View(material);
            }

            var newMaterial = new Material
            {
                Name = material.Name,
                MeasurementId = material.MeasurementId
            };

            data.Materials.Add(newMaterial);
            data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddProvider()
        {
            var newProvider = new AddProviderFormModel
            {
                Cities = GetCities(),
                Addresses = GetAddresses()
            };

            return View(newProvider);
        }

        [HttpPost]
        public IActionResult AddProvider(AddProviderFormModel provider)
        {
            provider.Cities = GetCities();
            provider.Addresses = GetAddresses();

            var providerlExists = data
               .Providers
               .Any(m => m.Name == provider.Name);

            if (providerlExists)
            {
                ModelState.AddModelError(nameof(provider.Name), existingModelInDB);

                return View(provider);
            }

            if (!ModelState.IsValid)
            {
                return View(provider);
            }

            var newProvider = new Provider
            {
                Name = provider.Name,
                Number = provider.Number,
                CityId = provider.CityId,
                AddressId = provider.AddressId
            };

            data.Providers.Add(newProvider);
            data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        private ICollection<WarehouseViewModel> GetWarehouses()
            => data
            .Warehouses
            .Select(w => new WarehouseViewModel
            {
                Id = w.Id,
                Name = w.Name
            })
            .ToList();

        private ICollection<ProviderViewModel> GetProviders()
            => data
            .Providers
            .Select(p => new ProviderViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Number = p.Number
            })
            .ToList();

        private ICollection<DocumentTypeViewModel> GetDocumentTypes()
            => data
            .DocumentTypes
            .Select(dt => new DocumentTypeViewModel
            {
                Id = dt.Id,
                Name = dt.Name
            })
            .ToList();

        private ICollection<MeasurementViewModel> GetMeasurements()
            => data
            .Measurements
            .Select(m => new MeasurementViewModel
            {
                Id = m.Id,
                Name = m.Name
            })
            .ToList();

        private ICollection<MaterialViewModel> GetMaterials()
            => data
            .Materials
            .Select(m => new MaterialViewModel
            {
                Id = m.Id,
                Name = m.Name,
                MeasurementId = m.Measurement.Id,
                MeasurementName = m.Measurement.Name
            })
            .ToList();

        private ICollection<WarehouseMaterialViewModel> GetAddedMaterials(int providerId, int documentNumber)
        {
            var materials = GetMaterials();

            var receiptNumber = 0;

            var currentReceipt = data
                .TempWarehouseReceipts
                .FirstOrDefault(twr => twr.ProviderId == providerId & twr.DocumentNumber == documentNumber);

            if (currentReceipt is null)
            {
                if (!data.TempWarehouseReceipts.Any())
                {
                    receiptNumber = 1;
                }
                else
                {
                    receiptNumber = data.TempWarehouseReceipts.OrderBy(x => x.Id).Last().ReceiptNumber + 1;
                }
            }
            else
            {
                receiptNumber = data.TempWarehouseReceipts.First(twr => twr.ProviderId == providerId & twr.DocumentNumber == documentNumber).ReceiptNumber;
            }

            var currentTemp = data
                .TempWarehouseReceipts
                .Where(t => t.ReceiptNumber == receiptNumber).ToList();

            var warehouseMaterialViewModels = new List<WarehouseMaterialViewModel>();

            foreach (var tempWarehouseMaterial in currentTemp)
            {
                var materialId = tempWarehouseMaterial.MaterialId;
                var quantity = tempWarehouseMaterial.Quantity;
                var price = tempWarehouseMaterial.UnitPrice;

                var newWarehouseMaterialViewModel = new WarehouseMaterialViewModel
                {
                    ReceiptNumber = receiptNumber,
                    MaterialId = materialId,
                    MaterialName = materials.FirstOrDefault(m => m.Id == materialId).Name,
                    MeasurementId = materials.FirstOrDefault(m => m.Id == materialId).MeasurementId,
                    MeasurementName = materials.FirstOrDefault(m => m.Id == materialId).MeasurementName,
                    Quantity = quantity,
                    Price = price
                };

                warehouseMaterialViewModels.Add(newWarehouseMaterialViewModel);
            }

            return warehouseMaterialViewModels;
        }

        private ICollection<CityViewModel> GetCities()
            => data
            .Cities
            .Select(c => new CityViewModel
            {
                Id = c.Id,
                Name = c.Name,
                PostalCode = c.PostalCode
            })
            .ToList();

        private ICollection<AddressViewModel> GetAddresses()
            => data
            .Addresses
            .Select(a => new AddressViewModel
            {
                Id = a.Id,
                AddressDetails = a.AddressDetails
            })
            .ToList();
    }
}
