namespace EateryPOSSystem.Services.Interfaces
{
    public interface IBaseDataService
    {
        bool IsCityExist(string cityName);

        void AddCity(string cityName, int? postalCode);

        bool IsDocumentTypeExist(string documentTypeName);

        void AddDocumentType(string documentTypeName);

        bool IsMeasurementExist(string measurementName);

        void AddMeasurement(string measurementName);

        bool IsPaymentTypeExist(string paymentTypeName);

        void AddPaymentType(string paymenttTypeName);

        bool IsPositionExist(string positionName);

        void AddPosition(string positionName);

        bool IsProductTypeExist(string productTypeName);

        void AddProductType(string productTypeName);

        bool IsStoreExist(string storeName);

        void AddStore(string storeName);

        bool IsWarehouseExist(string warehouseName);

        void AddWarehouse(string warehouseName);
    }
}