namespace EateryPOSSystem.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using EateryPOSSystem.Services.Interfaces;

    public class DbController : Controller
    {
        private readonly IBaseDataService baseData;

        private readonly IStorekeeperService storekeeperService;

        public DbController(IBaseDataService baseData,
            IStorekeeperService storekeeperService)
        {
            this.baseData = baseData;
            this.storekeeperService = storekeeperService;
        }


        public IActionResult ImportBaseData()
        {
            baseData.ImportBaseData();

            return RedirectToAction("Index", "Home");
        }


        public IActionResult ImportStorekeeperData()
        {
            storekeeperService.ImportStorekeeperData();

            return RedirectToAction("Index", "Home");
        }
    }

}
