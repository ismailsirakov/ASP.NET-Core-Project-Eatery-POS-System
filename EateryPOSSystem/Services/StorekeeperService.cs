namespace EateryPOSSystem.Services
{
    using EateryPOSSystem.Data;
    using EateryPOSSystem.Data.Models;
    using EateryPOSSystem.Services.Interfaces;
    using EateryPOSSystem.Services.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StorekeeperService : IStorekeeperService
    {
        private readonly EateryPOSDbContext data;

        public StorekeeperService(EateryPOSDbContext data)
        {
            this.data = data;
        }
        public bool IsAddressExist(string addressDetail)
            => data.Addresses.Any(a => a.AddressDetails == addressDetail);

        public void AddAddress(string addressDetail)
        {
            var address = new Address
            {
                AddressDetails = addressDetail
            };

            data.Addresses.Add(address);
            data.SaveChanges();
        }

        public bool IsProviderExist(string poviderName)
            => data.Providers.Any(p => p.Name == poviderName);

        public void AddProvider(string poviderName, int number, int cityId, int addressId)
        {
            var provider = new Provider
            {
                Name = poviderName,
                Number = number,
                CityId = cityId,
                AddressId = addressId
            };

            data.Providers.Add(provider);
            data.SaveChanges();
        }

        public bool IsMaterialExist(string materialName)
            => data.Materials.Any(m => m.Name == materialName);

        public void AddMaterial(string materialName, int measurementId)
        {
            var material = new Material
            {
                Name = materialName,
                MeasurementId = measurementId
            };

            data.Materials.Add(material);
            data.SaveChanges();
        }

        public void AddTempWarehouseReceipt(int providerId, int documentTypeId, int documentNumber, DateTime documentDate, int warehouseId, decimal quantity, decimal unitPrice, int materialId)
        {
            var tempWarehouseReceipt = new TempWarehouseReceipt
            {
                ProviderId = providerId,
                DocumentTypeId = documentTypeId,
                DocumentNumber = documentNumber,
                DocumentDate = documentDate,
                WarehouseId = warehouseId,
                MaterialId = materialId,
                Quantity = quantity,
                UnitPrice = unitPrice
            };

            data.TempWarehouseReceipts.Add(tempWarehouseReceipt);
            data.SaveChanges();
        }

        public WarehouseReceipt LastWarehouseReceiptInDb()
            => data.WarehouseReceipts.OrderBy(wr => wr.ReceiptNumber).LastOrDefault();

        public bool DbTempWarehouseReceiptsEmpty()
            => !data.TempWarehouseReceipts.Any();

        public IEnumerable<WarehouseReceipt> AddWarehouseReceiptListByReceiptNumber(int receiptNumber, int lastReceiptNumberInDb)
        {
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

            return warehouseReceiptList;
        }

        public void AddReceiptsMaterialsToWarehouse(IEnumerable<WarehouseReceipt> warehouseReceiptList)
        {
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
        }

        public IEnumerable<WarehouseServiceModel> GetWarehouses()
            => data
            .Warehouses
            .Select(w => new WarehouseServiceModel
            {
                Id = w.Id,
                Name = w.Name
            })
            .ToList();

        public IEnumerable<ProviderServiceModel> GetProviders()
            => data
            .Providers
            .Select(p => new ProviderServiceModel
            {
                Id = p.Id,
                Name = p.Name,
                Number = p.Number
            })
            .ToList();

        public IEnumerable<DocumentTypeServiceModel> GetDocumentTypes()
            => data
            .DocumentTypes
            .Select(dt => new DocumentTypeServiceModel
            {
                Id = dt.Id,
                Name = dt.Name
            })
            .ToList();

        public IEnumerable<MeasurementServiceModel> GetMeasurements()
            => data
            .Measurements
            .Select(m => new MeasurementServiceModel
            {
                Id = m.Id,
                Name = m.Name
            })
            .ToList();

        public IEnumerable<MaterialServiceModel> GetMaterials()
            => data
            .Materials
            .Select(m => new MaterialServiceModel
            {
                Id = m.Id,
                Name = m.Name,
                MeasurementId = m.Measurement.Id,
                MeasurementName = m.Measurement.Name
            })
            .ToList();

        public IEnumerable<WarehouseMaterialServiceModel> GetAddedMaterials(int providerId, int documentNumber)
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

            var warehouseMaterialServiceModels = new List<WarehouseMaterialServiceModel>();

            foreach (var tempWarehouseMaterial in currentTemp)
            {
                var materialId = tempWarehouseMaterial.MaterialId;
                var quantity = tempWarehouseMaterial.Quantity;
                var price = tempWarehouseMaterial.UnitPrice;

                var newWarehouseMaterialServiceModel = new WarehouseMaterialServiceModel
                {
                    ReceiptNumber = receiptNumber,
                    MaterialId = materialId,
                    MaterialName = materials.FirstOrDefault(m => m.Id == materialId).Name,
                    MeasurementId = materials.FirstOrDefault(m => m.Id == materialId).MeasurementId,
                    MeasurementName = materials.FirstOrDefault(m => m.Id == materialId).MeasurementName,
                    Quantity = quantity,
                    Price = price
                };

                warehouseMaterialServiceModels.Add(newWarehouseMaterialServiceModel);
            }

            return warehouseMaterialServiceModels;
        }

        public IEnumerable<CityServiceModel> GetCities()
            => data
            .Cities
            .Select(c => new CityServiceModel
            {
                Id = c.Id,
                Name = c.Name,
                PostalCode = c.PostalCode
            })
            .ToList();

        public IEnumerable<AddressServiceModel> GetAddresses()
            => data
            .Addresses
            .Select(a => new AddressServiceModel
            {
                Id = a.Id,
                AddressDetails = a.AddressDetails
            })
            .ToList();

    }
}
