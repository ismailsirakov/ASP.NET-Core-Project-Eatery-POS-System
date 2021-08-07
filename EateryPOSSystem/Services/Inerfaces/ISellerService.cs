namespace EateryPOSSystem.Services.Interfaces
{
    using System.Collections.Generic;
    using EateryPOSSystem.Services.Models;


    public interface ISellerService
    {
        IEnumerable<BillServiceModel> GetOpenBillsByStoreAndTableNumber(int storeId, int tableNumber);

        IEnumerable<StoreProductServiceModel> GetStoreProductsByStoreId(int storeId);

        void AddOrderProductToBill(OrderProductServiceModel soldProduct);

        void AddSoldProductToBill(SoldProductServiceModel soldProduct);

        void RemoveOrderProductsFromBill(int billId);

        void RemoveOrderProductById(int orderProductId);
    }
}
