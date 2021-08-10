namespace EateryPOSSystem.Services.Interfaces
{
    using System.Collections.Generic;
    using EateryPOSSystem.Services.Models;

    public interface IBillService
    {

        void AddNewBillToTable(string userId, int storeId, int tableNumber, int billId);

        void RemoveBillFromTable(int billId);

        IEnumerable<SoldProductServiceModel> SoldProductsByBillId(int billId);

        IEnumerable<OrderProductServiceModel> OrderProductsByBillId(int billId);

        int NewBill(string userId);
    }
}
