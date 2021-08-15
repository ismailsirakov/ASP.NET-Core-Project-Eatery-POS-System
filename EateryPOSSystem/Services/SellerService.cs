namespace EateryPOSSystem.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EateryPOSSystem.Data.Models;
    using EateryPOSSystem.Services.Interfaces;
    using EateryPOSSystem.Services.Models;

    public class SellerService : ISellerService
    {
        private readonly IDbService dbService;
        private readonly IBillService billService;

        public SellerService(IBillService billService, IDbService dbService)
        {
            this.billService = billService;
            this.dbService = dbService;
        }

        public IEnumerable<BillServiceModel> GetOpenBillsByStoreAndTableNumber(int storeId, int tableNumber)
        {
            var billIds = dbService.GetTablesWithOpenBills()
                .Where(op => op.StoreId == storeId & op.TableNumber == tableNumber)
                .Select(ob => ob.BillId)
                .ToList();

            var openBills = new List<BillServiceModel>();

            foreach (var billId in billIds)
            {
                var soldProducts = billService.SoldProductsByBillId(billId);

                var bill = dbService.GetBillById(billId);

                var user = dbService.GetUsers().FirstOrDefault(u => u.UserId == bill.UserId);

                var userBadge = user.FirstName + " " + user.LastName;

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
                    UserName = userBadge
                };

                openBills.Add(openBill);
            }

            return openBills;
        }

        public IEnumerable<StoreProductServiceModel> GetStoreProductsByStoreId(int storeId)
            => dbService.GetStoreProducts().Where(sp => sp.StoreId == storeId).ToList();

        public void AddOrderProductToBill(OrderProductServiceModel orderProduct)
        {
            dbService.AddOrderProduct(new OrderProduct
            {
                Id = orderProduct.Id,
                BillId = orderProduct.BillId,
                StoreProductId = orderProduct.StoreProductId,
                StoreProductName = orderProduct.StoreProductName,
                MeasurementId = orderProduct.MeasurementId,
                MeasurementName = orderProduct.MeasurementName,
                Quantity = orderProduct.Quantity,
                Price = orderProduct.Price
            });
        }

        public void AddSoldProductToBill(SoldProductServiceModel soldProduct)
        {
            dbService.AddSoldProduct(new SoldProduct
            {
                BillId = soldProduct.BillId,
                StoreProductId = soldProduct.StoreProductId,
                MeasurementId = soldProduct.MeasurementId,
                Quantity = soldProduct.Quantity,
                Price = soldProduct.Price,
                Cost = soldProduct.Cost,
                DateTime = DateTime.UtcNow
            });
        }

        public void RemoveOrderProductById(int orderProductId)
            => dbService.RemoveOrderProductById(orderProductId);
        public void RemoveOrderProductsFromBill(int billId)
            => dbService.RemoveOrderProductsFromBill(billId);

    }
}
