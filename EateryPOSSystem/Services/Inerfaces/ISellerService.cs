namespace EateryPOSSystem.Services.Interfaces
{
    using System.Collections.Generic;
    using EateryPOSSystem.Services.Models;


    public interface ISellerService
    {
        IEnumerable<BillServiceModel> GetOpenBillsByStoreAndTableNumber(int storeId, int tableNumber);
    }
}
