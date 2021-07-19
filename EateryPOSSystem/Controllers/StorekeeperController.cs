namespace EateryPOSSystem.Controllers
{
    using EateryPOSSystem.Data;
    using EateryPOSSystem.Data.Models;
    using EateryPOSSystem.Models.Storekeeper;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    public class StorekeeperController : Controller
    {
        private EateryPOSDbContext data;

        private const string existingModelInDB = "Already exists in database.";

        private const string notExistingModelInDB = "Selected item not exists in database.";

        public StorekeeperController(EateryPOSDbContext data)
        {
            this.data = data;
        }

        public IActionResult AddAddress()
            => View();

        [HttpPost]
        public IActionResult AddAddress(AddAddressFormModel address)
        {
            var addressExists = data.Addresses.Any(a => a.AddressDetails == address.AddressDetail);

            if (addressExists)
            {
                ModelState.AddModelError(nameof(address.AddressDetail), existingModelInDB);

                return View(address);
            }

            if (!ModelState.IsValid)
            {
                return View(address);
            }

            var newAddress= new Address
            {
                AddressDetails = address.AddressDetail
            };

            data.Addresses.Add(newAddress);
            data.SaveChanges();

            return View(address);
        }

        public IActionResult AddMaterialToWarehouse_1()
        {
            var newAddMaterialToWarehouseModel = new AddMaterialToWarehouseFormModel_1
            {
                Warehouses = GetWarehouses(),

                DocumentTypes = GetDocumentTypes(),

                Providers = GetProviders()
            };

            return View(newAddMaterialToWarehouseModel);
        }

        [HttpPost]
        public IActionResult AddMaterialToWarehouse_1(AddMaterialToWarehouseFormModel_1 warehouseMaterial)
        {

            warehouseMaterial.Warehouses = GetWarehouses();

            if (!warehouseMaterial.Warehouses.Any(w=>w.Id == warehouseMaterial.WarehouseId))
            {
                ModelState.AddModelError(nameof(warehouseMaterial.WarehouseId), notExistingModelInDB);

                return View(warehouseMaterial);
            }

            warehouseMaterial.DocumentTypes = GetDocumentTypes();

            if (!warehouseMaterial.DocumentTypes.Any(dt => dt.Id == warehouseMaterial.DocumentTypeId))
            {
                ModelState.AddModelError(nameof(warehouseMaterial.DocumentTypeId), notExistingModelInDB);

                return View(warehouseMaterial);
            }

            warehouseMaterial.Providers = GetProviders();

            if (!warehouseMaterial.Providers.Any(dt => dt.Id == warehouseMaterial.ProviderId))
            {
                ModelState.AddModelError(nameof(warehouseMaterial.ProviderId), notExistingModelInDB);

                return View(warehouseMaterial);
            }

            var providerName = warehouseMaterial.Providers.FirstOrDefault(p => p.Id == warehouseMaterial.ProviderId).Name;

            var warehouseName = warehouseMaterial.Warehouses
                .FirstOrDefault(w => w.Id == warehouseMaterial.WarehouseId)
                .Name;

            var documentTypeName = warehouseMaterial.DocumentTypes
                .FirstOrDefault(dt => dt.Id == warehouseMaterial.DocumentTypeId)
                .Name;
            //12;15;156;FGD;dsfsdf;dfs
            warehouseMaterial.ReceiptInfo = $"Доставчик: {providerName} - {documentTypeName} №{warehouseMaterial.DocumentNumber}/{warehouseMaterial.DocumentDate:d} - Склад: {warehouseName}";

            return RedirectToAction("AddMaterialToWarehouse_2", "Storekeeper", warehouseMaterial);
        }

        public IActionResult AddMaterialToWarehouse_2(AddMaterialToWarehouseFormModel_1 warehouseMaterial)
        {
            var newWarehouseMaterial = new AddMaterialToWarehouseFormModel_2
            {
                ReceiptInfo = warehouseMaterial.ReceiptInfo,
                Materials = GetMaterials(),
                AddedMaterials = new List<WarehouseMaterialViewModel>()
            };

            return View(newWarehouseMaterial);
        }

        [HttpPost]
        public IActionResult AddMaterialToWarehouse_2(AddMaterialToWarehouseFormModel_2 warehouseMaterial)
        {
            warehouseMaterial.Materials = GetMaterials();

            if (warehouseMaterial.AddedMaterials == null)
            {
                warehouseMaterial.AddedMaterials = new List<WarehouseMaterialViewModel>();
            }

            var currentMaterial = warehouseMaterial
                .Materials
                .FirstOrDefault(m => m.Id == warehouseMaterial.MaterialId);

            var newWarehouseMaterial = new WarehouseMaterialViewModel
            {
                MaterialId = currentMaterial.Id,
                MaterialName = currentMaterial.Name,
                MeasurementId = currentMaterial.MeasurementId,
                MeasurementName = currentMaterial.MeasurementName,
                Quantity = warehouseMaterial.Quantity,
                Price = warehouseMaterial.UnitPrice
            };

            if (warehouseMaterial.Quantity > 0)
            {
                warehouseMaterial.AddedMaterials.Add(newWarehouseMaterial);
            }


            return View(warehouseMaterial);
        }

        public IActionResult AddMaterial()
        {
            var newAddMaterialModel = new AddMaterialFormModel
            {
                Measurements = GetMeasurements()
            };

            return View(newAddMaterialModel);
        }

        [HttpPost]
        public IActionResult AddMaterial(AddMaterialFormModel material)
        {

            material.Measurements = GetMeasurements();

            var materialExists = data
                .Materials
                .Any(m => m.Name == material.Name);

            if (materialExists)
            {
                ModelState.AddModelError(nameof(material.Name), existingModelInDB);

                return View(material);
            }

            if (!ModelState.IsValid)
            {
                return View(material);
            }

            var newMaterial = new Material
            {
                Name = material.Name,
                MeasurementId = material.MeasurementId
            };

            data.Materials.Add(newMaterial);
            data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddProvider()
        {
            var newProvider = new AddProviderFormModel
            {
                Cities = GetCities(),
                Addresses = GetAddresses()
            };

            return View(newProvider);
        }

        [HttpPost]
        public IActionResult AddProvider(AddProviderFormModel provider)
        {
            provider.Cities = GetCities();
            provider.Addresses = GetAddresses();

            return View(provider);
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
                Name = m.Name,
                MeasurementId = m.Measurement.Id,
                MeasurementName = m.Measurement.Name
            })
            .ToList();

        private ICollection<CityViewModel> GetCities()
            => data.Cities.Select(c => new CityViewModel
            {
                Id = c.Id,
                Name = c.Name,
                PostalCode = c.PostalCode
            })
            .ToList();

        private ICollection<AddressViewModel> GetAddresses()
            => data.Addresses.Select(a => new AddressViewModel
            {
                AddressDetails = a.AddressDetails
            })
            .ToList();
    }
}
