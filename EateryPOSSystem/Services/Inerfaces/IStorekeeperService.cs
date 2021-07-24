using EateryPOSSystem.Data.Models;
using EateryPOSSystem.Services.Models;
using System;
using System.Collections.Generic;

namespace EateryPOSSystem.Services.Interfaces
{
    public interface IStorekeeperService
    {
        bool IsAddressExist(string addressDetail);

        void AddAddress(string addressDetail);

        void AddTempWarehouseReceipt( int receiptNumber, int providerId, int documentTypeId, int documentNumber, DateTime documentDate, int warehouseId, decimal quantity, decimal unitPrice, int materialId);

        bool IsMaterialExist(string materialName);

        void AddMaterial(string materialName, int measurementId);

        bool IsProviderExist(string poviderName);

        void AddProvider(string poviderName, int number, int cityId, int addressId);

        bool DbTempWarehouseReceiptsEmpty();

        WarehouseReceipt LastWarehouseReceiptInDb();

        IEnumerable<WarehouseReceipt> AddWarehouseReceiptListByReceiptNumber(int receiptNumber, int lastReceiptNumberInDb);

        void AddReceiptsMaterialsToWarehouse(IEnumerable<WarehouseReceipt> warehouseReceiptList);

        IEnumerable<WarehouseServiceModel> GetWarehouses();

        IEnumerable<ProviderServiceModel> GetProviders();

        IEnumerable<DocumentTypeServiceModel> GetDocumentTypes();

        IEnumerable<MeasurementServiceModel> GetMeasurements();

        IEnumerable<MaterialServiceModel> GetMaterials();

        IEnumerable<WarehouseMaterialServiceModel> GetAddedMaterials(int providerId, int documentNumber);


        IEnumerable<CityServiceModel> GetCities();

        IEnumerable<AddressServiceModel> GetAddresses();

        void ImportStorekeeperData();
    }
}
