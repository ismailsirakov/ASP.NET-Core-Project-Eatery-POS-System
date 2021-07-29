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
        public IActionResult ChooseTable(ChooseTableFormModel chosenStore, int tableNumber)
        {

            return View(chosenStore);
        }
    }
}
