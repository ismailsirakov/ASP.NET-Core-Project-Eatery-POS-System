namespace EateryPOSSystem.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using EateryPOSSystem.Data.Models;
    using EateryPOSSystem.Services.Models;

    public interface IStorekeeperService
    {
        bool IsAddressExist(string addressDetail);

        void AddAddress(string addressDetail);

        void AddTempWarehouseReceipt(int receiptNumber, int providerId, int documentTypeId, int documentNumber, DateTime documentDate, int warehouseId, decimal quantity, decimal unitPrice, int materialId);

        bool IsMaterialWhitNameExist(string materialName);

        void AddMaterial(string materialName, int measurementId);

        bool IsProviderExist(string poviderName);

        void AddProvider(string poviderName, int number, int cityId, int addressId);

        bool DbTempWarehouseReceiptsEmpty();

        WarehouseReceipt LastWarehouseReceiptInDb();

        IEnumerable<WarehouseReceipt> AddWarehouseReceiptListByReceiptNumber(int receiptNumber, int lastReceiptNumberInDb);

        void AddReceiptsMaterialsToWarehouse(IEnumerable<WarehouseReceipt> warehouseReceiptList);

        IEnumerable<WarehouseMaterialServiceModel> GetAddedMaterials(int providerId, int documentNumber);

        void ImportStorekeeperData();
    }
}
