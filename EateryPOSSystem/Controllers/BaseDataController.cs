namespace EateryPOSSystem.Controllers
{
    using EateryPOSSystem.Models.Storekeeper;
    using Microsoft.AspNetCore.Mvc;

    public class BaseDataController : Controller
    {

        public IActionResult AddCity() => View();

        [HttpPost]
        public IActionResult AddCity(AddCityFormModel city) => View();

        public IActionResult AddDocumentType() => View();

        [HttpPost]
        public IActionResult AddDocumentType(AddCityFormModel city) => View();

        public IActionResult AddMeasurement() => View();

        [HttpPost]
        public IActionResult AddMeasurement(AddCityFormModel city) => View();

        public IActionResult AddPaymentType() => View();

        [HttpPost]
        public IActionResult AddPaymentType(AddCityFormModel city) => View();

        public IActionResult AddPosition() => View();

        [HttpPost]
        public IActionResult AddPosition(AddCityFormModel city) => View();

        public IActionResult AddProductType() => View();

        [HttpPost]
        public IActionResult AddProductType(AddCityFormModel city) => View();

        public IActionResult AddStore() => View();

        [HttpPost]
        public IActionResult AddStore(AddCityFormModel city) => View();

        public IActionResult AddWarehouse() => View();

        [HttpPost]
        public IActionResult AddWarehouse(AddCityFormModel city) => View();
    }
}
