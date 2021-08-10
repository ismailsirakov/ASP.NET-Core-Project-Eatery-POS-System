namespace EateryPOSSystem.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using EateryPOSSystem.Services.Interfaces;
    using EateryPOSSystem.Infrastructure;

    [Authorize]
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
            storekeeperService.ImportStorekeeperData(User.GetId());

            return RedirectToAction("Index", "Home");
        }
    }

}
