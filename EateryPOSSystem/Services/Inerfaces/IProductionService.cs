namespace EateryPOSSystem.Services.Interfaces
{
    using System.Collections.Generic;
    using EateryPOSSystem.Services.Models;

    public interface IProductionService
    {
        

        void AddProduct(string productName, int storeId, int measurementId, int productTypeId, decimal quantity, decimal price);
    }
}
