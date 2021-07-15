namespace EateryPOSSystem.Controllers
{
    using EateryPOSSystem.Data;
    using EateryPOSSystem.Models.Storekeeper;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    public class StorekeeperController : Controller
    {
        private EateryPOSDbContext data;

        public StorekeeperController(EateryPOSDbContext data)
        {
            this.data = data;
        }

        public IActionResult AddMaterialToWarehouse()
        {
            var newAddMaterialToWarehouseModel = new AddMaterialToWarehouseFormModel();

            newAddMaterialToWarehouseModel.Warehouses = GetWarehouses();

            newAddMaterialToWarehouseModel.Materials = GetMaterials();

            newAddMaterialToWarehouseModel.DocumentTypes = GetDocumentTypes();

            newAddMaterialToWarehouseModel.Providers = GetProviders();

            newAddMaterialToWarehouseModel.Measurements = GetMeasurements();

            return View(newAddMaterialToWarehouseModel);
        }

        [HttpPost]
        public IActionResult AddMaterialToWarehouse(AddMaterialToWarehouseFormModel material)
        {
            var newAddMaterialToWarehouseModel = new AddMaterialToWarehouseFormModel();

            newAddMaterialToWarehouseModel.Warehouses = GetWarehouses();

            newAddMaterialToWarehouseModel.Materials = GetMaterials();

            newAddMaterialToWarehouseModel.DocumentTypes = GetDocumentTypes();

            newAddMaterialToWarehouseModel.Providers = GetProviders();

            newAddMaterialToWarehouseModel.Measurements = GetMeasurements();

            return View();
        }

        public IActionResult AddMaterial()
        {
            var newAddMaterialModel = new AddMaterialFormModel();

            newAddMaterialModel.Measurements = GetMeasurements();

            return View(newAddMaterialModel);
        }

        [HttpPost]
        public IActionResult AddMaterial(AddMaterialFormModel material)
        {
            var newAddMaterialModel = new AddMaterialFormModel();

            newAddMaterialModel.Measurements = GetMeasurements();

            return View(material);
        }

        public IActionResult AddProvider()
        {
            return View();
        }

        private ICollection<WarehouseViewModel> GetWarehouses()
            => data.Warehouses.Select(w => new WarehouseViewModel
            {
                Id = w.Id,
                Name = w.Name
            })
            .ToList();

        private ICollection<ProviderViewModel> GetProviders()
            => data.Providers.Select(p => new ProviderViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Number = p.Number
            })
            .ToList();

        private ICollection<DocumentTypeViewModel> GetDocumentTypes()
            => data.DocumentTypes.Select(dt => new DocumentTypeViewModel
            {
                Id = dt.Id,
                Name = dt.Name
            })
            .ToList();

        private ICollection<MeasurementViewModel> GetMeasurements()
            => data.Measurements.Select(m => new MeasurementViewModel
            {
                Id = m.Id,
                Name = m.Name
            })
            .ToList();

        private ICollection<MaterialViewModel> GetMaterials()
            => data.Materials.Select(m => new MaterialViewModel
            {
                Id = m.Id,
                Name = m.Name
            })
            .ToList();
    }
}
