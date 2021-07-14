namespace EateryPOSSystem.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class StorekeeperController : Controller
    {
        public IActionResult AddMaterialToWarehouse()
        {
            return View();
        }

        public IActionResult AddMaterial()
        {
            return View();
        }

        public IActionResult AddProvider()
        {
            return View();
        }
    }
}
