namespace EateryPOSSystem.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using EateryPOSSystem.Data.Models;
    using EateryPOSSystem.Services.Models;

    public interface IDbService
    {
        void AddAddress(Address address);

        void AddBill(Bill bill);

        void AddDoocumentType(DocumentType documentType);

        void AddCity(City city);

        void AddMaterial(Material material);

        void AddMeasurement(Measurement measurement);

        void AddOrderProduct(OrderProduct orderProduct);

        void AddPaymentType(PaymentType paymentType);

        void AddPosition(Position position);

        void AddProduct(Product product);

        void AddProductType(ProductType productType);

        void AddProvider(Provider provider);

        void AddRecipe(Recipe recipe);

        void AddSoldProduct(SoldProduct soldProduct);

        void AddStore(Store store);

        void AddStoreProduct(StoreProduct storeProduct);

        void AddStoreTable(StoreTable table);

        void AddTempWarehouseReceipt(TempWarehouseReceipt tempWarehouseReceipt);

        void AddTransfer(Transfer transfer);

        void AddWarehouse(Warehouse warehouse);

        void AddWarehouseMaterial(WarehouseMaterial warehouseMaterial);

        void AddWarehouseReceiptRange(IEnumerable<WarehouseReceipt> warehouseReceipts);

        IEnumerable<RecipeServiceModel> GetAddedMaterialsToRecipe(string recipeName, int productId);

        IEnumerable<AddressServiceModel> GetAddresses();

        IEnumerable<BillServiceModel> GetBillsByDate(DateTime dateTime);

        Bill GetBillById(int billId);

        IEnumerable<CityServiceModel> GetCities();

        IEnumerable<DocumentTypeServiceModel> GetDocumentTypes();

        IEnumerable<MaterialServiceModel> GetMaterials();

        IEnumerable<MeasurementServiceModel> GetMeasurements();

        IEnumerable<OrderProductServiceModel> GetOrderProducts();

        IEnumerable<PaymentTypeServiceModel> GetPaymentTypes();

        IEnumerable<PositionServiceModel> GetPositions();

        IEnumerable<ProductServiceModel> GetProducts();

        IEnumerable<ProductTypeServiceModel> GetProductTypes();

        IEnumerable<ProviderServiceModel> GetProviders();

        IEnumerable<RecipeServiceModel> GetRecipes();

        IEnumerable<SoldProductServiceModel> GetSoldProducts();

        IEnumerable<StoreServiceModel> GetStores();

        IEnumerable<StoreProductServiceModel> GetStoreProducts();

        IEnumerable<TableServiceModel> GetTablesWithOpenBills();

        IEnumerable<TempWarehouseReceipt> GetTempWarehouseReceipts();

        IEnumerable<TransferServiceModel> GetTransfers();

        IEnumerable<UserServiceModel> GetUsers();

        string GetUserUserName(string userId);

        IEnumerable<WarehouseServiceModel> GetWarehouses();

        IEnumerable<WarehouseMaterialServiceModel> GetWarehouseMaterials();

        WarehouseMaterial GetWarehouseMaterialByWarehouseIdAndMaterialId(int warehouseId, int materialId);

        IEnumerable<int> GetWarehouseMaterialIdsByWarehouseId(int warehouseId);

        IEnumerable<WarehouseReceipt> GetWarehouseReceipts();

        void RemoveBillFromTable(int billId);

        void RemoveOrderProductById(int billId);

        void RemoveOrderProductsFromBill(int billId);

        void RemoveTempWarehouseReceiptsRangeByReceiptNumber(int receiptNumber);

        void UpdateWarehouseMaterialQuantity(int warehouseId, int materialId, decimal quantity);

        void UpdateWarehouseMaterialPrice(int warehouseId, int materialId, decimal price);
    }
}
