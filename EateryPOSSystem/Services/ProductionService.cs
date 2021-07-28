namespace EateryPOSSystem.Services
{
    using System.Linq;
    using System.Collections.Generic;
    using EateryPOSSystem.Data;
    using EateryPOSSystem.Data.Models;
    using EateryPOSSystem.Services.Interfaces;
    using EateryPOSSystem.Services.Models;

    public class ProductionService : IProductionService
    {
        private readonly EateryPOSDbContext data;

        public ProductionService(EateryPOSDbContext data)
        {
            this.data = data;
        }
        
        public void AddProduct(string productName, int storeId, int measurementId, int productTypeId, decimal quantity, decimal price)
        {
            if (IsProductExist(productName, storeId))
            {
                return;
            }

            var product = new Product
            {
                Name = productName,
                StoreId = storeId,
                MeasurementId = measurementId,
                ProductTypeId = productTypeId,
                Quantity = quantity,
                Price = price
            };

            data.Products.Add(product);

            data.SaveChanges();
        }

        public bool IsProductExist(string productName, int storeId)
            => data.Products
            .Any(p => p.Name == productName & p.StoreId == storeId);
    }
}
