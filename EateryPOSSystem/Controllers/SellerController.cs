namespace EateryPOSSystem.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using EateryPOSSystem.Models.Seller;
    using EateryPOSSystem.Services.Interfaces;

    public class SellerController : Controller
    {
        private readonly ISellerService seller;
        private readonly IDbService dbService;

        public SellerController(IDbService dbService, ISellerService seller)
        {
            this.seller = seller;
            this.dbService = dbService;
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
                TablesInStore = stores.FirstOrDefault(s => s.Id == store.StoreId).TablesInStore,
                StoreName = stores.FirstOrDefault(s => s.Id == store.StoreId).Name
            };

            return RedirectToAction("ChooseTable", "Seller", chosenStore);
        }

        public IActionResult ChooseTable(ChooseTableFormModel chosenStore)
        {            

            return View(chosenStore);
        }

        [HttpPost]
        public IActionResult ChooseTable(ChooseTableFormModel chosenStoreTables, int tableNumber)
        {
            var table = new TableFormModel
            {
                TableNumber = tableNumber,
                StoreName = chosenStoreTables.StoreName
            };

            return RedirectToAction("Table", "Seller", table);
        }

        public IActionResult Table(TableFormModel table)
        {
            table.OpenBills = seller.GetOpenBillsByStoreAndTableNumber(table.StoreId, table.TableNumber);

            return View(table);
        }

        [HttpPost]
        public IActionResult Table(TableFormModel table, int billNumberToView, string newBill)
        {
            table.OpenBills = seller.GetOpenBillsByStoreAndTableNumber(table.StoreId, table.TableNumber);

            if (newBill != null)
            {

                return RedirectToAction();
            }

            return View(table);
        }
    }
}
