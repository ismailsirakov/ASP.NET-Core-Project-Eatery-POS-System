namespace EateryPOSSystem.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using EateryPOSSystem.Data.Models;
    using EateryPOSSystem.Services.Models;

    public interface IStorekeeperService
    {
        bool DbTempWarehouseReceiptsEmpty();

        bool IsAddressExist(string addressDetail);

        bool IsMaterialWhitNameExist(string materialName);

        bool IsProviderExist(string poviderName);

        IEnumerable<WarehouseReceipt> AddWarehouseReceiptListByReceiptNumber(int receiptNumber, int lastReceiptNumberInDb);

        IEnumerable<WarehouseMaterialServiceModel> GetAddedMaterials(int providerId, int documentNumber);

        IEnumerable<TransferServiceModel> GetTransferedMaterials(int transferNumber);
        void AddAddress(string addressDetail);

        void AddTempWarehouseReceipt(int receiptNumber,
                                    int providerId,
                                    int documentTypeId,
                                    int documentNumber,
                                    DateTime documentDate,
                                    int warehouseId,
                                    decimal quantity,
                                    decimal unitPrice,
                                    int materialId);

        void AddMaterial(string materialName, int measurementId);

        void AddProvider(string poviderName, int number, int cityId, int addressId);

        void AddReceiptsMaterialsToWarehouse(IEnumerable<WarehouseReceipt> warehouseReceiptList);

        void AddTransfer(TransferServiceModel transfer);

        void ImportStorekeeperData();

        void TransferMaterial(TransferServiceModel transfer);

        WarehouseReceipt LastWarehouseReceiptInDb();
    }
}
