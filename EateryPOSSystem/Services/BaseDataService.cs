namespace EateryPOSSystem.Services
{
    using System.IO;
    using System.Linq;
    using AutoMapper;
    using Newtonsoft.Json;
    using EateryPOSSystem.Data;
    using EateryPOSSystem.Data.DataTransferObjects;
    using EateryPOSSystem.Data.Models;
    using EateryPOSSystem.Services.Interfaces;

    public class BaseDataService : IBaseDataService
    {
        private readonly EateryPOSDbContext data;
        private IMapper mapper;

        public BaseDataService(EateryPOSDbContext data)
        {
            this.data = data;
        }

        public bool IsCityExist(string cityName)
            => data.Cities.Any(c => c.Name == cityName);

        public void AddCity(string cityName, int? postalCode)
        {
            if (IsCityExist(cityName))
            {
                return;
            }

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
            if (IsDocumentTypeExist(documentTypeName))
            {
                return;
            }

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
            if (IsMeasurementExist(measurementName))
            {
                return;
            }

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
            if (IsPaymentTypeExist(paymentTypeName))
            {
                return;
            }

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
            if (IsPositionExist(positionName))
            {
                return;
            }
            
            var position = new Position
            {
                Name = positionName
            };

            data.Positions.Add(position);
            data.SaveChanges();
            
            if (positionName == "Администратор")
            {
                data.POSSystemUsers.Add(new User
                {
                    FirstName = "Admin",
                    LastName = "Administrator",
                    UserName = "Admin",
                    Password = "AdminPass",
                    PositionId = position.Id
                });

                data.SaveChanges();
            }

        }

        public bool IsProductTypeExist(string productTypeName)
            => data.ProductTypes.Any(pt => pt.Name == productTypeName);


        public void AddProductType(string productTypeName)
        {
            if (IsProductTypeExist(productTypeName))
            {
                return;
            }

            var productType = new ProductType
            {
                Name = productTypeName
            };

            data.ProductTypes.Add(productType);
            data.SaveChanges();
        }

        public bool IsStoreExist(string storeName)
            => data.Stores.Any(s => s.Name == storeName);

        public void AddStore(string storeName, int tablesInStore)
        {
            if (IsStoreExist(storeName))
            {
                return;
            }

            var store = new Store
            {
                Name = storeName,
                TablesInStore = tablesInStore
            };

            data.Stores.Add(store);
            data.SaveChanges();
        }

        public bool IsWarehouseExist(string warehouseName)
            => data.Warehouses.Any(w => w.Name == warehouseName);

        public void AddWarehouse(string warehouseName)
        {
            if (IsWarehouseExist(warehouseName))
            {
                return;
            }

            var warehouse = new Warehouse
            {
                Name = warehouseName
            };

            data.Warehouses.Add(warehouse);
            data.SaveChanges();
        }

        public void ImportBaseData()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<EateryPOSSystemProfile>());
            mapper = config.CreateMapper();

            var inputJson = File.ReadAllText(".\\Data\\Datasets\\seedingdata.json");

            var dtoInput = JsonConvert.DeserializeObject<InputDTO>(inputJson);

            var inputData = mapper.Map<Input>(dtoInput);

            foreach (var city in inputData.Cities)
            {
                AddCity(city.Name, city.PostalCode);
            }
            
            foreach (var documentType in inputData.DocumentTypes)
            {
                AddDocumentType(documentType.Name);
            }

            foreach (var measurement in inputData.Measurements)
            {
                AddMeasurement(measurement.Name);
            }

            foreach (var paymentType in inputData.PaymentTypes)
            {
                AddPaymentType(paymentType.Name);
            }

            foreach (var position in inputData.Positions)
            {
                AddPosition(position.Name);
            }

            foreach (var productType in inputData.ProductTypes)
            {
                AddProductType(productType.Name);
            }

            foreach (var store in inputData.Stores)
            {
                AddStore(store.Name, store.TablesInStore);
            }

            foreach (var warehouse in inputData.Warehouses)
            {
                AddWarehouse(warehouse.Name);
            }
        }
    }
}
