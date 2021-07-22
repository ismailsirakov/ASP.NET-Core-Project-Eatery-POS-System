namespace EateryPOSSystem.Services
{
    using EateryPOSSystem.Data;
    using EateryPOSSystem.Data.Models;
    using EateryPOSSystem.Services.Interfaces;
    using System.Linq;

    public class BaseDataService : IBaseDataService
    {
        private readonly EateryPOSDbContext data;

        public BaseDataService(EateryPOSDbContext data)
        {
            this.data = data;
        }

        public bool IsCityExist(string cityName)
            => data.Cities.Any(c => c.Name == cityName);

        public void AddCity(string cityName, int? postalCode)
        {
            var city = new City
            {
                Name = cityName,
                PostalCode = postalCode
            };

            data.Cities.Add(city);
            data.SaveChanges();
        }

        public bool IsDocumentTypeExist(string documentTypeName)
            => data.DocumentTypes.Any(dt => dt.Name == documentTypeName);

        public void AddDocumentType(string documentTypeName)
        {
            var documentType = new DocumentType
            {
                Name = documentTypeName
            };

            data.DocumentTypes.Add(documentType);
            data.SaveChanges();
        }

        public bool IsMeasurementExist(string measurementName)
            => data.Measurements.Any(m => m.Name == measurementName);

        public void AddMeasurement(string measurementName)
        {
            var measurement = new Measurement
            {
                Name = measurementName
            };

            data.Measurements.Add(measurement);
            data.SaveChanges();
        }

        public bool IsPaymentTypeExist(string paymenttTypeName)
            => data.PaymentTypes.Any(pt => pt.Name == paymenttTypeName);

        public void AddPaymentType(string paymentTypeName)
        {
            var paymentType = new PaymentType
            {
                Name = paymentTypeName
            };

            data.PaymentTypes.Add(paymentType);
            data.SaveChanges();
        }

        public bool IsPositionExist(string positionName)
            => data.Positions.Any(p => p.Name == positionName);

        public void AddPosition(string positionName)
        {
            var position = new Position
            {
                Name = positionName
            };

            data.Positions.Add(position);
            data.SaveChanges();
        }

        public bool IsProductTypeExist(string productTypeName)
            => data.ProductTypes.Any(pt => pt.Name == productTypeName);


        public void AddProductType(string productTypeName)
        {
            var productType = new ProductType
            {
                Name = productTypeName
            };

            data.ProductTypes.Add(productType);
            data.SaveChanges();
        }

        public bool IsStoreExist(string storeName)
            => data.Stores.Any(s => s.Name == storeName);

        public void AddStore(string storeName)
        {
            var store = new Store
            {
                Name = storeName
            };

            data.Stores.Add(store);
            data.SaveChanges();
        }

        public bool IsWarehouseExist(string warehouseName)
            => data.Warehouses.Any(w => w.Name == warehouseName);

        public void AddWarehouse(string warehouseName)
        {
            var warehouse = new Warehouse
            {
                Name = warehouseName
            };

            data.Warehouses.Add(warehouse);
            data.SaveChanges();
        }
    }
}
