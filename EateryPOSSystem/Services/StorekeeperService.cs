namespace EateryPOSSystem.Services
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.IO;
    using AutoMapper;
    using Newtonsoft.Json;
    using EateryPOSSystem.Data.Models;
    using EateryPOSSystem.Services.Interfaces;
    using EateryPOSSystem.Services.Models;
    using EateryPOSSystem.Data.DataTransferObjects;

    public class StorekeeperService : IStorekeeperService
    {
        private readonly IDbService dbService;
        private IMapper mapper;

        public StorekeeperService(IDbService dbService)
        {
            this.dbService = dbService;
        }

        public bool IsAddressExist(string addressDetail)
            => dbService.GetAddresses()
            .Any(a => a.AddressDetails == addressDetail);

        public void AddAddress(string addressDetail)
        {
            if (IsAddressExist(addressDetail))
            {
                return;
            }

            var address = new Address
            {
                AddressDetails = addressDetail
            };

            dbService.AddAddress(address);
        }

        public bool IsProviderExist(string poviderName)
            => dbService.GetProviders()
            .Any(p => p.Name == poviderName);

        public void AddProvider(string providerName, int number, int cityId, int addressId)
        {
            if (IsProviderExist(providerName))
            {
                return;
            }

            var provider = new Provider
            {
                Name = providerName,
                Number = number,
                CityId = cityId,
                AddressId = addressId
            };

            dbService.AddProvider(provider);
        }

        public bool IsMaterialWhitNameExist(string materialName)
            => dbService.GetMaterials()
            .Any(m => m.Name == materialName);

        public void AddMaterial(string materialName, int measurementId)
        {
            if (IsMaterialWhitNameExist(materialName))
            {
                return;
            }

            var material = new Material
            {
                Name = materialName,
                MeasurementId = measurementId
            };

            dbService.AddMaterial(material);
        }

        public void AddTempWarehouseReceipt(int receiptNumber, int providerId, int documentTypeId, int documentNumber, DateTime documentDate, int warehouseId, decimal quantity, decimal unitPrice, int materialId)
        {
            var tempWarehouseReceipt = new TempWarehouseReceipt
            {
                ReceiptNumber = receiptNumber,
                ProviderId = providerId,
                DocumentTypeId = documentTypeId,
                DocumentNumber = documentNumber,
                DocumentDate = documentDate,
                WarehouseId = warehouseId,
                MaterialId = materialId,
                Quantity = quantity,
                UnitPrice = unitPrice
            };

            dbService.AddTempWarehouseReceipt(tempWarehouseReceipt);
        }

        public void AddTransfer(TransferServiceModel transfer)
        {
            var newTransfer = new Transfer
            {
                Number = transfer.Number,
                FromWarehouseId = transfer.FromWarehouseId,
                ToWarehouseId = transfer.ToWarehouseId,
                MaterialId = transfer.MaterialId,
                Quantity = transfer.Quantity,
                UserId = transfer.UserId
            };

            dbService.AddTransfer(newTransfer);
        }

        public WarehouseReceipt LastWarehouseReceiptInDb()
            => dbService.GetWarehouseReceipts().OrderBy(wr => wr.ReceiptNumber).LastOrDefault();

        public bool DbTempWarehouseReceiptsEmpty()
            => !dbService.GetTempWarehouseReceipts().Any();

        public IEnumerable<WarehouseReceipt> AddWarehouseReceiptListByReceiptNumber(int receiptNumber, int lastReceiptNumberInDb)
        {
            var warehouseReceiptList = dbService
                    .GetTempWarehouseReceipts()
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

            dbService.AddWarehouseReceiptRange(warehouseReceiptList);

            dbService.RemoveTempWarehouseReceiptsRangeByReceiptNumber(receiptNumber);

            return warehouseReceiptList;
        }

        public void AddReceiptsMaterialsToWarehouse(IEnumerable<WarehouseReceipt> warehouseReceiptList)
        {
            var currentWarehouseId = warehouseReceiptList
                    .FirstOrDefault()
                    .WarehouseId;

            var materialIdsInWarehouse = dbService.GetWarehouseMaterialIdsByWarehouseId(currentWarehouseId).ToList();

            foreach (var receipt in warehouseReceiptList)
            {
                var materialId = receipt.MaterialId;

                if (!materialIdsInWarehouse.Contains(materialId))
                {
                    var newWarehouseMaterial = new WarehouseMaterial
                    {
                        WarehouseId = currentWarehouseId,
                        MaterialId = materialId
                    };

                    materialIdsInWarehouse.Add(materialId);

                    dbService.AddWarehouseMaterial(newWarehouseMaterial);
                }

                var currentWarehouseMaterialQuantity = dbService.
                    GetWarehouseMaterialByWarehouseIdAndMaterialId(currentWarehouseId, materialId).Quantity;

                var currentWarehouseMaterialPrice = dbService.
                    GetWarehouseMaterialByWarehouseIdAndMaterialId(currentWarehouseId, materialId).Price;

                var currentTotalAmount = currentWarehouseMaterialQuantity * currentWarehouseMaterialPrice;

                currentWarehouseMaterialQuantity += receipt.Quantity;

                currentWarehouseMaterialPrice = (currentTotalAmount + receipt.Quantity * receipt.UnitPrice) / currentWarehouseMaterialQuantity;

                dbService.UpdateWarehouseMaterialQuantity(currentWarehouseId, materialId, currentWarehouseMaterialQuantity);

                dbService.UpdateWarehouseMaterialPrice(currentWarehouseId, materialId, currentWarehouseMaterialPrice);
            }
        }

        public void TransferMaterial(TransferServiceModel transfer)
        {
            var fromWarehouseMaterial = dbService
                .GetWarehouseMaterialByWarehouseIdAndMaterialId(transfer.FromWarehouseId, transfer.MaterialId);

            var toWarehouseMaterial = dbService.GetWarehouseMaterialByWarehouseIdAndMaterialId(transfer.ToWarehouseId, transfer.MaterialId);

            if (toWarehouseMaterial is null)
            {
                toWarehouseMaterial = new WarehouseMaterial
                {
                    WarehouseId = transfer.ToWarehouseId,
                    MaterialId = transfer.MaterialId
                };

                dbService.AddWarehouseMaterial(toWarehouseMaterial);
            }

            var toWarehouseMaterialTotalAmount = toWarehouseMaterial.Quantity * toWarehouseMaterial.Price;

            var transferedMaterialTotalAmount = transfer.Quantity * fromWarehouseMaterial.Price;

            toWarehouseMaterialTotalAmount += transferedMaterialTotalAmount;

            fromWarehouseMaterial.Quantity -= transfer.Quantity;

            toWarehouseMaterial.Quantity += transfer.Quantity;

            toWarehouseMaterial.Price = toWarehouseMaterialTotalAmount / toWarehouseMaterial.Quantity;

            dbService.UpdateWarehouseMaterialQuantity(fromWarehouseMaterial.WarehouseId,
                                                        fromWarehouseMaterial.MaterialId,
                                                        fromWarehouseMaterial.Quantity);

            dbService.UpdateWarehouseMaterialQuantity(toWarehouseMaterial.WarehouseId,
                                                        toWarehouseMaterial.MaterialId,
                                                        toWarehouseMaterial.Quantity);

            dbService.UpdateWarehouseMaterialPrice(toWarehouseMaterial.WarehouseId,
                                                        toWarehouseMaterial.MaterialId,
                                                        toWarehouseMaterial.Price);
        }

        public IEnumerable<WarehouseMaterialServiceModel> GetAddedMaterials(int providerId, int documentNumber)
        {
            var materials = dbService.GetMaterials();

            var receiptNumber = 0;

            var currentReceipt = dbService.GetTempWarehouseReceipts()
                .FirstOrDefault(twr => twr.ProviderId == providerId & twr.DocumentNumber == documentNumber);

            if (currentReceipt is null)
            {
                if (!dbService.GetTempWarehouseReceipts().Any())
                {
                    receiptNumber = 1;
                }
                else
                {
                    receiptNumber = dbService.GetTempWarehouseReceipts()
                        .OrderBy(x => x.Id).Last()
                        .ReceiptNumber + 1;
                }
            }
            else
            {
                receiptNumber = dbService.GetTempWarehouseReceipts()
                    .First(twr => twr.ProviderId == providerId & twr.DocumentNumber == documentNumber)
                    .ReceiptNumber;
            }

            var currentTempWarehouseReceipt = dbService.GetTempWarehouseReceipts()
                .Where(twr => twr.ProviderId == providerId & twr.DocumentNumber == documentNumber)
                .ToList();

            var warehouseMaterialServiceModels = new List<WarehouseMaterialServiceModel>();

            foreach (var tempWarehouseMaterial in currentTempWarehouseReceipt)
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

        public int GetLastTempWarehouseReceiptNumber()
            => dbService.GetTempWarehouseReceipts().OrderBy(r => r.Id).Last().ReceiptNumber;

        public IEnumerable<TransferServiceModel> GetTransferedMaterials(int transferNumber)
            => dbService.GetTransfers()
                .Where(t => t.Number == transferNumber)
                .ToList();

        

        public void ImportStorekeeperData()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<EateryPOSSystemProfile>());
            mapper = config.CreateMapper();

            var inputJson = File.ReadAllText(".\\Data\\Datasets\\seedingdata.json");

            var dtoInput = JsonConvert.DeserializeObject<InputDTO>(inputJson);
            var inputData = mapper.Map<Input>(dtoInput);

            foreach (var address in inputData.Addresses)
            {
                AddAddress(address.AddressDetails);
            }

            foreach (var material in inputData.Materials)
            {
                AddMaterial(material.Name, material.MeasurementId);
            }

            foreach (var provider in inputData.Providers)
            {
                AddProvider(provider.Name, provider.Number, provider.CityId, provider.AddressId);
            }

            foreach (var wr in inputData.warehouseReceipts)
            {
                AddTempWarehouseReceipt(wr.ReceiptNumber,
                                        wr.ProviderId,
                                        wr.DocumentTypeId,
                                        wr.DocumentNumber,
                                        wr.DocumentDate,
                                        wr.WarehouseId,
                                        wr.Quantity,
                                        wr.UnitPrice,
                                        wr.MaterialId);
            }
            
            AddReceiptsMaterialsToWarehouse(AddWarehouseReceiptListByReceiptNumber(1, 0));
            AddReceiptsMaterialsToWarehouse(AddWarehouseReceiptListByReceiptNumber(2, 1));
        }
    }
}
