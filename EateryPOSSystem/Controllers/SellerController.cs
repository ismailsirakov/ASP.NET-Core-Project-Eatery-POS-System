namespace EateryPOSSystem.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using EateryPOSSystem.Models.Seller;
    using EateryPOSSystem.Services.Interfaces;
    using EateryPOSSystem.Models.Bill;
    using EateryPOSSystem.Services.Models;
    using static ControllerConstants;

    [Authorize]
    public class SellerController : Controller
    {
        private readonly IBillService billService;
        private readonly IDbService dbService;
        private readonly IProductionService productionService;
        private readonly ISellerService sellerService;

        public SellerController(IBillService billService,
                                IDbService dbService,
                                IProductionService productionService,
                                ISellerService sellerService)
        {
            this.billService = billService;
            this.dbService = dbService;
            this.productionService = productionService;
            this.sellerService = sellerService;
        }

        public IActionResult ChooseStore()
        {
            var table = new ChooseStoreFormModel
            {
                Stores = dbService.GetStores()
            };

            return View(table);
        }

        [HttpPost]
        public IActionResult ChooseStore(ChooseStoreFormModel store)
        {
            var stores = dbService.GetStores();

            var chosenStore = new ChooseTableFormModel
            {
                Id = store.StoreId,
                TablesInStore = stores.FirstOrDefault(s => s.Id == store.StoreId).TablesInStore,
                StoreName = stores.FirstOrDefault(s => s.Id == store.StoreId).Name
            };

            return RedirectToAction("ChooseTable", "Seller", chosenStore);
        }

        public IActionResult ChooseTable(ChooseTableFormModel chosenStore)
        {
            chosenStore.TablesWithOpenBills = dbService.GetTablesWithOpenBills()
                                                       .Where(t=>t.StoreName == chosenStore.StoreName);

            return View(chosenStore);
        }

        [HttpPost]
        public IActionResult ChooseTable(ChooseTableFormModel chosenStoreTables, int tableNumber)
        {
            chosenStoreTables.TablesWithOpenBills = dbService.GetTablesWithOpenBills()
                                                             .Where(t => t.StoreName == chosenStoreTables.StoreName);

            if (chosenStoreTables.Id == 0)
            {
                chosenStoreTables.Id = dbService.GetStores()
                                                .FirstOrDefault(s => s.Name == chosenStoreTables.StoreName)
                                                .Id;
            }

            var table = new TableFormModel
            {
                TableNumber = tableNumber,
                StoreId = chosenStoreTables.Id,
                StoreName = chosenStoreTables.StoreName
            };

            return RedirectToAction("Table", "Seller", table);
        }

        public IActionResult Table(TableFormModel table)
        {
            if (table.StoreId == 0 && table.TableNumber == 0)
            {
                table.StoreId = (int)TempData["StoreId"];

                table.TableNumber = (int)TempData["TableNumber"];

                table.StoreName = (string)TempData["StoreName"];
            }

            table.OpenBills = sellerService.GetOpenBillsByStoreAndTableNumber(table.StoreId,
                                                                              table.TableNumber);

            TempData["StoreId"] = table.StoreId;

            TempData["StoreName"] = table.StoreName;

            TempData["TableNumber"] = table.TableNumber;

            return View(table);
        }

        [HttpPost]
        public IActionResult Table(TableFormModel table,
                                   int billIdToView,
                                   string goBack,
                                   string newBill)
        {
            if (table.StoreId == 0 && table.StoreName == null && table.TableNumber == 0)
            {
                table.StoreId = (int)TempData["StoreId"];

                table.TableNumber = (int)TempData["TableNumber"];

                table.StoreName = (string)TempData["StoreName"];
            }


            if (goBack != null)
            {
                var storeName = table.StoreName;

                if (table.StoreId == 0)
                {
                    table.StoreId = (int)TempData["StoreId"];
                }

                var tablesInStore = dbService.GetStores()
                                             .FirstOrDefault(s => s.Name == table.StoreName)
                                             .TablesInStore;

                var chosenStore = new ChooseTableFormModel
                {
                    StoreName = storeName,
                    TablesInStore = tablesInStore
                };

                TempData["StoreId"] = table.StoreId;

                return RedirectToAction("ChooseTable", "Seller", chosenStore);
            }

            table.OpenBills = sellerService.GetOpenBillsByStoreAndTableNumber(table.StoreId,
                                                                              table.TableNumber);

            if (table.StoreId == 0)
            {
                table.StoreId = (int)TempData["StoreId"];
            }

            if (newBill != null)
            {
                TempData["StoreId"] = table.StoreId;

                TempData["TableNumber"] = table.TableNumber;

                return RedirectToAction("New", "Bill");
            }

            if (!table.OpenBills.Any(ob => ob.Id == billIdToView))
            {
                ModelState.AddModelError(nameof(table.BillId), notExistingModelInDB);

                return View(table);
            }

            if (table.BillId == 0)
            {
                table.BillId = billIdToView;
            }

            TempData["BillId"] = table.BillId;

            TempData["StoreId"] = table.StoreId;

            TempData["StoreName"] = table.StoreName;

            TempData["TableNumber"] = table.TableNumber;

            return RedirectToAction("Details", "Bill");
        }

        public IActionResult AddSoldProductToBill()
        {
            var billId = (int)TempData["BillId"];

            var storeId = (int)TempData["StoreId"];

            var bill = new AddSoldProductToBillFormModel
            {
                BillId = billId,
                StoreId = storeId,
                OrderProducts = billService.OrderProductsByBillId(billId),
                StoreProducts = sellerService.GetStoreProductsByStoreId(storeId)
            };

            TempData["BillId"] = billId;

            TempData["StoreId"] = storeId;

            return View(bill);
        }

        [HttpPost]
        public IActionResult AddSoldProductToBill(AddSoldProductToBillFormModel bill,
                                                  string addButton,
                                                  string deleteButton,
                                                  string endOrderButton)
        {
            bill.BillId = (int)TempData["BillId"];

            bill.OrderProducts = billService.OrderProductsByBillId(bill.BillId);

            bill.StoreId = (int)TempData["StoreId"];

            bill.StoreProducts = sellerService.GetStoreProductsByStoreId(bill.StoreId);

            if (addButton != null)
            {
                if (bill.StoreProductId == 0)
                {
                    TempData["BillId"] = bill.BillId;

                    TempData["StoreId"] = bill.StoreId;
                    
                    return View(bill);
                }

                var soldProductPrice = bill.StoreProducts
                                           .FirstOrDefault(sp => sp.Id == bill.StoreProductId)
                                           .Price;

                var measurementId = bill.StoreProducts
                                        .FirstOrDefault(sp => sp.Id == bill.StoreProductId)
                                        .MeasurementId;

                var measurementName = dbService.GetMeasurements()
                                               .FirstOrDefault(m => m.Id == measurementId)
                                               .Name;

                var storeProductName = bill.StoreProducts
                                           .FirstOrDefault(sp => sp.Id == bill.StoreProductId)
                                           .ProductName;

                var orderProduct = new OrderProductServiceModel
                {
                    BillId = bill.BillId,
                    StoreProductId = bill.StoreProductId,
                    StoreProductName = storeProductName,
                    MeasurementId = measurementId,
                    MeasurementName = measurementName,
                    Quantity = bill.Quantity,
                    Price = soldProductPrice
                };

                sellerService.AddOrderProductToBill(orderProduct);

                bill.StoreProducts = sellerService.GetStoreProductsByStoreId(bill.StoreId);

                bill.OrderProducts = billService.OrderProductsByBillId(bill.BillId);

                TempData["BillId"] = bill.BillId;

                TempData["StoreId"] = bill.StoreId;

                return View(bill);
            }

            if (deleteButton != null)
            {
                if (bill.OrderProductId == 0)
                {
                    TempData["BillId"] = bill.BillId;

                    TempData["StoreId"] = bill.StoreId;

                    return View(bill);
                }

                sellerService.RemoveOrderProductById(bill.OrderProductId);

                bill.StoreProducts = sellerService.GetStoreProductsByStoreId(bill.StoreId);

                bill.OrderProducts = billService.OrderProductsByBillId(bill.BillId);

                TempData["BillId"] = bill.BillId;

                TempData["StoreId"] = bill.StoreId;

                return View(bill);
            }

            if (endOrderButton != null)
            {
                bill.OrderProducts = billService.OrderProductsByBillId(bill.BillId);

                foreach (var orderProduct in bill.OrderProducts)
                {
                    var soldProduct = new SoldProductServiceModel
                    {
                        BillId = orderProduct.BillId,
                        StoreProductId = orderProduct.StoreProductId,
                        StoreProductName = orderProduct.StoreProductName,
                        MeasurementId = orderProduct.MeasurementId,
                        MeasurementName = orderProduct.MeasurementName,
                        Quantity = orderProduct.Quantity,
                        Price = orderProduct.Price
                    };                    

                    soldProduct.Cost = productionService.CalculateCostOfProduct(orderProduct.StoreProductId) * orderProduct.Quantity;

                    sellerService.AddSoldProductToBill(soldProduct);

                    productionService.DecreaseMaterialsUsedInStoreProduct(orderProduct.StoreProductId,
                                                                          orderProduct.Quantity);
                }

                sellerService.RemoveOrderProductsFromBill(bill.BillId);

                return RedirectToAction("ChooseStore", "Seller");
            }

            var currentStore = dbService.GetStores()
                                        .FirstOrDefault(s => s.Id == bill.StoreId);

            var store = new ChooseTableFormModel
            {
                StoreName = currentStore.Name,
                TablesInStore = currentStore.TablesInStore
            };

            return RedirectToAction("ChooseTable", "Seller", store);
        }

    }
}
