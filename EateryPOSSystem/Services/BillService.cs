namespace EateryPOSSystem.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EateryPOSSystem.Data.Models;
    using EateryPOSSystem.Services.Interfaces;
    using EateryPOSSystem.Services.Models;

    public class BillService : IBillService
    {
        private readonly IDbService dbService;

        public BillService(IDbService dbService)
        {
            this.dbService = dbService;
        }

        public IEnumerable<SoldProductServiceModel> SoldProductsByBillId(int billId)
            => dbService.GetSoldProducts().Where(sp=>sp.BillId == billId).ToList();

        public IEnumerable<OrderProductServiceModel> OrderProductsByBillId(int billId)
            => dbService.GetOrderProducts().Where(sp => sp.BillId == billId).ToList();

        public int NewBill(string userId)
        {
            var newBill = new Bill
            {
                OpenDateTime = DateTime.UtcNow,
                UserId = userId
            };

            dbService.AddBill(newBill);

            return newBill.Id;
        }

        public void AddNewBillToTable(string userId, int storeId, int tableNumber, int billId)
        {
            var storeName = dbService.GetStores().FirstOrDefault(s => s.Id == storeId).Name;

            var tableWithNewBill = new StoreTable
            {
                StoreId = storeId,
                StoreName = storeName,
                TableNumber = tableNumber,
                UserId = userId,
                BillId = billId
            };

            dbService.AddStoreTable(tableWithNewBill);
        }

        public void RemoveBillFromTable(int billId)
            => dbService.RemoveBillFromTable(billId);
    }
}
