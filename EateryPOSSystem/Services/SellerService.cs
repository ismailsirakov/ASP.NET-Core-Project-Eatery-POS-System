namespace EateryPOSSystem.Services
{
    using EateryPOSSystem.Services.Interfaces;
    using EateryPOSSystem.Services.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class SellerService : ISellerService
    {
        private readonly IDbService dbService;

        public SellerService(IDbService dbService)
        {
            this.dbService = dbService;
        }

        public IEnumerable<BillServiceModel> GetOpenBillsByStoreAndTableNumber(int storeId, int tableNumber)
        {
            var billIds = dbService.GetTablesWithOpenBills()
                .Where(op => op.StoreId == storeId & op.TableNumber == tableNumber)
                .Select(ob => ob.BillNumber)
                .ToList();

            var openBills = new List<BillServiceModel>();

            foreach (var billId in billIds)
            {
                var soldProducts = dbService.GetSoldProductsByBillId(billId);

                var bill = dbService.GetBillById(billId);

                var billTotalSum = 0m;

                foreach (var soldProduct in soldProducts)
                {
                    billTotalSum += (soldProduct.Quantity * soldProduct.Price);
                }

                var openBill = new BillServiceModel
                {
                    Id = billId,
                    TotalSum = billTotalSum,
                    UserId = bill.UserId,
                    UserName = bill.User.UserName
                };

                openBills.Add(openBill);
            }

            return openBills;
        }
    }
}
