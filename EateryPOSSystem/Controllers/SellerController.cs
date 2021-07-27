namespace EateryPOSSystem.Controllers
{
    using EateryPOSSystem.Services;
    using EateryPOSSystem.Models.Seller;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.Diagnostics;
    using EateryPOSSystem.Services.Interfaces;
    using System.Linq;

    public class SellerController : Controller
    {
        private ISellerService seller;

        public SellerController(ISellerService seller)
        {
            this.seller = seller;
        }

        public IActionResult ChooseStore()
        {
            var table = new ChooseStoreFormModel
            {
                Stores = seller.GetStores()
            };

            return View(table);
        }

        [HttpPost]
        public IActionResult ChooseStore(ChooseStoreFormModel store)
        {
            var stores = seller.GetStores();

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

    }
}
