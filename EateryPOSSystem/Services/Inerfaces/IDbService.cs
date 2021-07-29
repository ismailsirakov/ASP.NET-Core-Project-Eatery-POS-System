namespace EateryPOSSystem.Services.Interfaces
{
    using System.Collections.Generic;
    using EateryPOSSystem.Data.Models;
    using EateryPOSSystem.Services.Models;

    public interface IDbService
    {
        void AddAddress(Address address);

        void AddBill(Bill bill);

        void AddCity(City city);

        void AddDoocumentType(DocumentType documentType);

        void AddMaterial(Material material);

        void AddMeasurement(Measurement measurement);

        void AddPaymentType(PaymentType paymentType);

        void AddPosition(Position position);

        void AddProduct(Product product);

        void AddProductType(ProductType productType);

        void AddProvider(Provider provider);

        void AddRecipe(Recipe recipe);

        void AddSoldProduct(SoldProduct soldProduct);

        void AddStore(Store store);

        void AddStoreProduct(StoreProduct storeProduct);

        void AddTempWarehouseReceipt(TempWarehouseReceipt tempWarehouseReceipt);

        void AddUser(User user);

        void AddWarehouse(Warehouse warehouse);

        void AddWarehouseMaterial(WarehouseMaterial warehouseMaterial);


        void AddWarehouseReceiptRange(IEnumerable<WarehouseReceipt> warehouseReceipts);

        IEnumerable<StoreServiceModel> GetStores();

        IEnumerable<PositionServiceModel> GetPositions();

        IEnumerable<PaymentTypeServiceModel> GetPaymentTypes();

        IEnumerable<CityServiceModel> GetCities();

        IEnumerable<ProductTypeServiceModel> GetProductTypes();

        IEnumerable<AddressServiceModel> GetAddresses();

        IEnumerable<WarehouseServiceModel> GetWarehouses();

        IEnumerable<ProviderServiceModel> GetProviders();

        IEnumerable<ProductServiceModel> GetProducts();

        IEnumerable<DocumentTypeServiceModel> GetDocumentTypes();

        IEnumerable<MeasurementServiceModel> GetMeasurements();

        IEnumerable<MaterialServiceModel> GetMaterials();

        IEnumerable<Recipe> GetRecipes();

        IEnumerable<StoreProductServiceModel> GetStoreProducts();

        IEnumerable<WarehouseReceipt> GetWarehouseReceipts();

        IEnumerable<TempWarehouseReceipt> GetTempWarehouseReceipts();

        IEnumerable<int> GetWarehouseMaterialIdsByWarehouseId(int warehouseId);

        IEnumerable<WarehouseMaterialServiceModel> GetWarehouseMaterialsByWarehouseId(int warehouseId);

        WarehouseMaterial GetWarehouseMaterialByWarehouseIdAndMaterialId(int warehouseId, int materialId);

        IEnumerable<RecipeServiceModel> GetAddedMaterialsToRecipe(string recipeName, int productId);

        void RemoveTempWarehouseReceiptsRangeByReceiptNumber(int receiptNumber);

        void UpdateWareHouseMaterialQuantity(int warehouseId, int materialId, decimal quantity);

        void UpdateWarehouseMaterialPrice(int warehouseId, int materialId, decimal price);
    }
}
