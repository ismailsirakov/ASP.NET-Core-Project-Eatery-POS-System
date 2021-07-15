namespace EateryPOSSystem.Controllers
{
    using EateryPOSSystem.Models.BaseData;
    using Microsoft.AspNetCore.Mvc;

    public class BaseDataController : Controller
    {

        public IActionResult AddCity() => View();

        [HttpPost]
        public IActionResult AddCity(AddCityFormModel city) => View();

        public IActionResult AddDocumentType() => View();

        [HttpPost]
        public IActionResult AddDocumentType(AddDocumentTypeFormModel document) => View();

        public IActionResult AddMeasurement() => View();

        [HttpPost]
        public IActionResult AddMeasurement(AddMeasurementFormModel measurement) => View();

        public IActionResult AddPaymentType() => View();

        [HttpPost]
        public IActionResult AddPaymentType(AddPaymentTypeFormModel paymentType) => View();

        public IActionResult AddPosition() => View();

        [HttpPost]
        public IActionResult AddPosition(AddPositionFormModel position) => View();

        public IActionResult AddProductType() => View();

        [HttpPost]
        public IActionResult AddProductType(AddProductTypeFormModel productType) => View();

        public IActionResult AddStore() => View();

        [HttpPost]
        public IActionResult AddStore(AddStoreFormModel store) => View();

        public IActionResult AddWarehouse() => View();

        [HttpPost]
        public IActionResult AddWarehouse(AddWarehouseFormModel warehouse) => View();
    }
}
