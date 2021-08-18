namespace EateryPOSSystem.Services
{
    using System.Linq;
    using EateryPOSSystem.Data.Models;
    using EateryPOSSystem.Services.Interfaces;
    using System;
    using System.Collections.Generic;
    using EateryPOSSystem.Services.Models;

    public class ReportService : IReportService
    {
        private readonly IDbService dbService;

        public ReportService(IDbService dbService)
        {
            this.dbService = dbService;
        }

        public IEnumerable<BillServiceModel> BillsForDate(DateTime dateTime, bool closed)
        {
            var bills = dbService.GetBillsByDate(dateTime).Where(b => b.Closed == closed).ToList();

            var paymentTypes = dbService.GetPaymentTypes().ToList();

            var soldProductsForDate = dbService.GetSoldProducts().Where(sp => sp.DateTime.Date == dateTime.Date).ToList();

            foreach (var bill in bills)
            {
                bill.SoldProducts = soldProductsForDate.Where(sp => sp.BillId == bill.Id).ToList();

                bill.PaymentTypeName = paymentTypes.FirstOrDefault(pt => pt.Id == bill.PaymentTypeId).Name;

                var total = 0m;

                foreach (var soldProduct in bill.SoldProducts)
                {
                    total += soldProduct.Quantity * soldProduct.Price;
                }

                bill.TotalSum = total;
            }

            return bills;
        }


        public IEnumerable<SoldProductServiceModel> SoldProductsCumulative(int storeId, DateTime fromDate, DateTime toDate)
        {
            var storeProductsIds = StoreProductsIds(storeId).ToList();

            var soldProducts = SoldProductsBetweenDates(storeProductsIds, fromDate, toDate).ToList();

            var products = new Dictionary<string, SoldProductServiceModel>();

            foreach (var soldProduct in soldProducts)
            {
                var productName = soldProduct.StoreProductName;

                if (!products.ContainsKey(productName))
                {
                    products.Add(productName, soldProduct);

                    products[productName].Total = soldProduct.Quantity * soldProduct.Price;
                }
                else
                {
                    products[productName].Total += soldProduct.Quantity * soldProduct.Price;

                    products[productName].Quantity += soldProduct.Quantity;

                    products[productName].Cost += soldProduct.Cost;
                }
            }

            soldProducts.Clear();

            foreach (var product in products)
            {
                product.Value.Price = product.Value.Total / product.Value.Quantity;

                soldProducts.Add(product.Value);
            }

            return soldProducts;
        }

        public IEnumerable<SoldProductServiceModel> SoldProductsDetailed(int storeId,
                                                                 DateTime fromDate,
                                                                 DateTime toDate)
        {
            var storeProductsIds = StoreProductsIds(storeId).ToList();

            var soldProducts = SoldProductsBetweenDates(storeProductsIds, fromDate, toDate).ToList();

            return soldProducts;
        }

        private IEnumerable<SoldProductServiceModel> SoldProductsBetweenDates(IEnumerable<int> storeProductsIds,
                                                                              DateTime fromDate,
                                                                              DateTime toDate)
            => dbService.GetSoldProducts()
                        .Where(sp => sp.DateTime >= fromDate
                                  && sp.DateTime <= toDate
                                  && storeProductsIds
                                     .Contains(sp.StoreProductId));

        private IEnumerable<int> StoreProductsIds(int storeId)
           => dbService.GetStoreProducts()
                       .Where(sp => sp.StoreId == storeId)
                       .Select(sp => sp.Id);

        public IEnumerable<WarehouseReceiptServiceModel> WarehouseReceipts(int providerId,
                                                                           DateTime fromDate,
                                                                           DateTime toDate)
        {
            var materials = new List<WarehouseReceiptServiceModel>();

            if (providerId == 0)
            {
                var receipts = WarehouseReceipts2(dbService.GetWarehouseReceipts()
                                        .Where(wr => wr.DocumentDate >= fromDate
                                                && wr.DocumentDate <= toDate))
                                        .ToList();

                materials.AddRange(receipts);
            }
            else
            {
                var receipts = WarehouseReceipts2(dbService.GetWarehouseReceipts()
                                        .Where(wr => wr.ProviderId == providerId
                                                && wr.DocumentDate >= fromDate
                                                && wr.DocumentDate <= toDate))
                                        .ToList();

                materials.AddRange(receipts);
            }

            var providers = dbService.GetProviders().ToList();
            var documentTypes = dbService.GetDocumentTypes().ToList();
            var warehouses = dbService.GetWarehouses().ToList();
            var allMaterials = dbService.GetMaterials().ToList();
            var measurements = dbService.GetMeasurements().ToList();

            foreach (var material in materials)
            {
                material.ProviderName = providers.FirstOrDefault(p => p.Id == material.ProviderId).Name;
                material.DocumentTypeName = documentTypes.FirstOrDefault(dt => dt.Id == material.DocumentTypeId).Name;
                material.WarehouseName = warehouses.FirstOrDefault(w => w.Id == material.WarehouseId).Name;
                material.MaterialName = allMaterials.FirstOrDefault(m => m.Id == material.MaterialId).Name;
                material.MeasurementId = allMaterials.FirstOrDefault(m => m.Id == material.MaterialId).MeasurementId;
                material.MeasurementName = measurements.FirstOrDefault(m => m.Id == material.MeasurementId).Name;
            }

            return materials;
        }

        private IList<WarehouseReceiptServiceModel> WarehouseReceipts2(IEnumerable<WarehouseReceipt> warehouseReceipts)
        => warehouseReceipts.Select(wr => new WarehouseReceiptServiceModel
        {
            Id = wr.Id,
            ReceiptNumber = wr.ReceiptNumber,
            ProviderId = wr.ProviderId,
            DocumentTypeId = wr.DocumentTypeId,
            DocumentNumber = wr.DocumentNumber,
            DocumentDate = wr.DocumentDate,
            WarehouseId = wr.WarehouseId,
            MaterialId = wr.MaterialId,
            Quantity = wr.Quantity,
            Price = wr.UnitPrice,
        })
                            .ToList();
    }
}
