namespace EateryPOSSystem.Services.Interfaces
{
    using System.Collections.Generic;
    using EateryPOSSystem.Services.Models;

    public interface IProductionService
    {
        bool IsProductExistInStore(int productId, int storeId);

        bool IsMaterialWithIdExist(int materialId);

        bool IsProductWithIdExist(int productId);

        bool IsMaterialInRecipeExist(string recipeName, int productId, int materialId);

        void AddRecipe(string recipeName, int productId, int materialId, decimal quantity);

        void AddProductToStore(int productId, int storeId, int measurementlId, decimal quantity, decimal price);
    }
}
