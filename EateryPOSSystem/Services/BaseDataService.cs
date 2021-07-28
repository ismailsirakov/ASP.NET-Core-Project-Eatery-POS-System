namespace EateryPOSSystem.Services
{
    using System.IO;
    using System.Linq;
    using AutoMapper;
    using Newtonsoft.Json;
    using EateryPOSSystem.Data.DataTransferObjects;
    using EateryPOSSystem.Data.Models;
    using EateryPOSSystem.Services.Interfaces;

    public class BaseDataService : IBaseDataService
    {
        private readonly IDbService dbService;
        private IMapper mapper;

        public BaseDataService(IDbService dbService)
        {
            this.dbService = dbService;
        }

        public bool IsCityExist(string cityName)
            => dbService.GetCities().Any(c => c.Name == cityName);

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

            dbService.AddCity(city);
        }

        public bool IsDocumentTypeExist(string documentTypeName)
            => dbService.GetDocumentTypes().Any(dt => dt.Name == documentTypeName);

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

            dbService.AddDoocumentType(documentType);
        }

        public bool IsMeasurementExist(string measurementName)
            => dbService.GetMeasurements().Any(m => m.Name == measurementName);

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

            dbService.AddMeasurement(measurement);
        }

        public bool IsPaymentTypeExist(string paymentTypeName)
            => dbService.GetPaymentTypes().Any(pt => pt.Name == paymentTypeName);

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

            dbService.AddPaymentType(paymentType);
        }

        public bool IsPositionExist(string positionName)
            => dbService.GetPositions().Any(p => p.Name == positionName);

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

            dbService.AddPosition(position);
            
            if (positionName == "Администратор")
            {
                var user = new User
                {
                    FirstName = "Admin",
                    LastName = "Administrator",
                    UserName = "Admin",
                    Password = "AdminPass",
                    PositionId = position.Id
                };

                dbService.AddUser(user);
            }

        }

        public bool IsProductTypeExist(string productTypeName)
            => dbService.GetProductTypes().Any(pt => pt.Name == productTypeName);

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

            dbService.AddProductType(productType);
        }

        public bool IsStoreExist(string storeName)
            => dbService.GetStores().Any(s => s.Name == storeName);

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

            dbService.AddStore(store);
        }

        public bool IsWarehouseExist(string warehouseName)
            => dbService.GetWarehouses().Any(w => w.Name == warehouseName);

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

            dbService.AddWarehouse(warehouse);
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
