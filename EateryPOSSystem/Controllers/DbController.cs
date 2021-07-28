namespace EateryPOSSystem.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using EateryPOSSystem.Services.Interfaces;

    public class DbController : Controller
    {
        private readonly IBaseDataService baseData;

        private readonly IStorekeeperService storekeeper;

        public DbController(IBaseDataService baseData,
            IStorekeeperService storekeeper)
        {
            this.baseData = baseData;
            this.storekeeper = storekeeper;
        }


        public IActionResult ImportBaseData()
        {
            baseData.ImportBaseData();

            return RedirectToAction("Index", "Home");
        }


        public IActionResult ImportStorekeeperData()
        {
            storekeeper.ImportStorekeeperData();

            return RedirectToAction("Index", "Home");
        }
    }

}
