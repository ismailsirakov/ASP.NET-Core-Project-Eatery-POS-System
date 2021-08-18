namespace EateryPOSSystem.Services
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using EateryPOSSystem.Data;
    using EateryPOSSystem.Services.Interfaces;
    using EateryPOSSystem.Services.Models;
    using EateryPOSSystem.Data.Models;

    public class DbService : IDbService
    {
        private readonly EateryPOSDbContext data;

        public DbService(EateryPOSDbContext data)
        {
            this.data = data;
        }

        public void AddAddress(Address address)
        {
            data.Addresses.Add(address);
            data.SaveChanges();
        }

        public void AddBill(Bill bill)
        {
            data.Bills.Add(bill);
            data.SaveChanges();
        }

        public void AddCity(City city)
        {
            data.Cities.Add(city);
            data.SaveChanges();
        }

        public void AddDoocumentType(DocumentType documentType)
        {
            data.DocumentTypes.Add(documentType);
            data.SaveChanges();
        }

        public void AddMaterial(Material material)
        {
            data.Materials.Add(material);
            data.SaveChanges();
        }

        public void AddMeasurement(Measurement measurement)
        {
            data.Measurements.Add(measurement);
            data.SaveChanges();
        }

        public void AddOrderProduct(OrderProduct orderProduct)
        {
            data.OrderProducts.Add(orderProduct);
            data.SaveChanges();
        }

        public void AddPaymentType(PaymentType paymentType)
        {
            data.PaymentTypes.Add(paymentType);
            data.SaveChanges();
        }

        public void AddPosition(Position position)
        {
            data.Positions.Add(position);
            data.SaveChanges();
        }

        public void AddProduct(Product product)
        {
            data.Products.Add(product);
            data.SaveChanges();
        }

        public void AddProductType(ProductType productType)
        {
            data.ProductTypes.Add(productType);
            data.SaveChanges();
        }

        public void AddProvider(Provider provider)
        {
            data.Providers.Add(provider);
            data.SaveChanges();
        }

        public void AddRecipe(Recipe recipe)
        {
            data.Recipes.Add(recipe);
            data.SaveChanges();
        }

        public void AddSoldProduct(SoldProduct soldProduct)
        {
            data.SoldProducts.Add(soldProduct);
            data.SaveChanges();
        }

        public void AddStore(Store store)
        {
            data.Stores.Add(store);
            data.SaveChanges();
        }

        public void AddStoreProduct(StoreProduct storeProduct)
        {
            data.StoreProducts.Add(storeProduct);
            data.SaveChanges();
        }

        public void AddStoreTable(StoreTable table)
        {
            data.StoreTables.Add(table);
            data.SaveChanges();
        }
        public void AddTempWarehouseReceipt(TempWarehouseReceipt tempWarehouseReceipt)
        {
            data.TempWarehouseReceipts.Add(tempWarehouseReceipt);
            data.SaveChanges();
        }

        public void AddTransfer(Transfer transfer)
        {
            data.Transfers.Add(transfer);
            data.SaveChanges();
        }

        public void AddWarehouse(Warehouse warehouse)
        {
            data.Warehouses.Add(warehouse);
            data.SaveChanges();
        }

        public void AddWarehouseMaterial(WarehouseMaterial warehouseMaterial)
        {
            data.WarehouseMaterials.Add(warehouseMaterial);
            data.SaveChanges();
        }

        public void AddWarehouseReceiptRange(IEnumerable<WarehouseReceipt> warehouseReceipts)
        {
            data.WarehouseReceipts.AddRange(warehouseReceipts);
            data.SaveChanges();
        }

        public IEnumerable<AddressServiceModel> GetAddresses()
            => data.Addresses
            .Select(a => new AddressServiceModel
            {
                Id = a.Id,
                AddressDetails = a.AddressDetails
            });

        public IEnumerable<RecipeServiceModel> GetAddedMaterialsToRecipe(string recipeName, int storeProductId)
            => data.Recipes
            .Where(r => r.Name == recipeName & r.StoreProductId == storeProductId)
            .Select(r => new RecipeServiceModel
            {
                Id = r.Id,
                Name = r.Name,
                StoreProductId = r.StoreProductId,
                WarehouseMaterialId = r.WarehouseMaterialMaterialId,
                MaterialName = r.WarehouseMaterial.Material.Name,
                MeasurementName = r.WarehouseMaterial.Material.Measurement.Name,
                MaterialQuantity = r.MaterialQuantity
            });

        public IEnumerable<BillServiceModel> GetBillsByDate(DateTime dateTime)
            => data.Bills
                   .Where(b => b.OpenDateTime.Date == dateTime.Date)
                   .Select(b=> new BillServiceModel
                   {
                       Id = b.Id,
                       OpenDateTime = b.OpenDateTime,
                       UserName = b.User.UserName,
                       UserBadge = b.User.FirstName + " " + b.User.LastName,
                       PaymentTypeId = b.PaymentTypeId,
                       CloseDateTime = b.CloseDateTime,
                       Closed = b.Closed,
                       UserId = b.UserId
                   });

        public Bill GetBillById(int billId)
            => data.Bills.FirstOrDefault(b => b.Id == billId);

        public IEnumerable<CityServiceModel> GetCities()
            => data.Cities
            .Select(c => new CityServiceModel
            {
                Id = c.Id,
                Name = c.Name,
                PostalCode = c.PostalCode
            });

        public IEnumerable<DocumentTypeServiceModel> GetDocumentTypes()
            => data.DocumentTypes
            .Select(dt => new DocumentTypeServiceModel
            {
                Id = dt.Id,
                Name = dt.Name
            });

        public IEnumerable<MaterialServiceModel> GetMaterials()
            => data.Materials
            .Select(m => new MaterialServiceModel
            {
                Id = m.Id,
                Name = m.Name,
                MeasurementId = m.Measurement.Id,
                MeasurementName = m.Measurement.Name
            });

        public IEnumerable<MeasurementServiceModel> GetMeasurements()
            => data.Measurements
            .Select(m => new MeasurementServiceModel
            {
                Id = m.Id,
                Name = m.Name
            });

        public IEnumerable<OrderProductServiceModel> GetOrderProducts()
            => data.OrderProducts
            .Select(op => new OrderProductServiceModel
            {
                Id = op.Id,
                BillId = op.BillId,
                StoreProductId = op.StoreProductId,
                StoreProductName = op.StoreProductName,
                MeasurementId = op.MeasurementId,
                MeasurementName = op.MeasurementName,
                Quantity = op.Quantity,
                Price = op.Price
            });
        

        public IEnumerable<ProductTypeServiceModel> GetProductTypes()
            => data.ProductTypes
            .Select(pt => new ProductTypeServiceModel
            {
                Id = pt.Id,
                Name = pt.Name
            });

        public IEnumerable<ProductServiceModel> GetProducts()
            => data.Products
            .Select(p => new ProductServiceModel
            {
                Id = p.Id,
                Name = p.Name
            });

        public IEnumerable<ProviderServiceModel> GetProviders()
            => data.Providers
            .Select(p => new ProviderServiceModel
            {
                Id = p.Id,
                Name = p.Name,
                Number = p.Number
            });

        public IEnumerable<PositionServiceModel> GetPositions()
            => data.Positions
            .Select(m => new PositionServiceModel
            {
                Id = m.Id,
                Name = m.Name
            })
            .ToList();

        public IEnumerable<PaymentTypeServiceModel> GetPaymentTypes()
            => data.PaymentTypes
            .Select(m => new PaymentTypeServiceModel
            {
                Id = m.Id,
                Name = m.Name
            });

        public IEnumerable<RecipeServiceModel> GetRecipes()
            => data.Recipes
            .Select(r=> new RecipeServiceModel
            {
                Id = r.Id,
                Name = r.Name,
                StoreProductId = r.StoreProductId,
                StoreName = r.StoreProduct.Store.Name,
                StoreProductName = r.StoreProduct.Product.Name,
                WarehouseMaterialId = r.WarehouseMaterialMaterialId,
                MaterialName = r.WarehouseMaterial.Material.Name,
                WarehouseId = r.WarehouseMaterialWarehouseId,
                MeasurementName = r.WarehouseMaterial.Material.Measurement.Name,
                MaterialQuantity = r.MaterialQuantity
            });

        public IEnumerable<SoldProductServiceModel> GetSoldProducts()
            =>data.SoldProducts
            .Select(sp=> new SoldProductServiceModel
            {
                Id = sp.Id,
                BillId = sp.BillId,
                StoreProductId = sp.StoreProductId,
                StoreProductName = sp.StoreProduct.Product.Name,
                MeasurementId = sp.MeasurementId,
                MeasurementName = sp.Measurement.Name,
                Quantity = sp.Quantity,
                Price = sp.Price,
                DateTime = sp.DateTime,
                Cost = sp.Cost
            });
        
        public IEnumerable<StoreServiceModel> GetStores()
            => data.Stores
            .Select(s => new StoreServiceModel
            {
                Id = s.Id,
                Name = s.Name,
                TablesInStore = s.TablesInStore
            });

        public IEnumerable<StoreProductServiceModel> GetStoreProducts()
            => data.StoreProducts
            .Select(sp => new StoreProductServiceModel
            {
                Id = sp.Id,
                StoreId = sp.StoreId,
                StoreName = sp.Store.Name,
                ProductId = sp.ProductId,
                ProductName = sp.Product.Name,
                MeasurementId = sp.MeasurementId,
                Quantity = sp.Quantity,
                Price = sp.Price
            });

        public IEnumerable<TableServiceModel> GetTablesWithOpenBills()
            => data.StoreTables
            .Select(st => new TableServiceModel
            {
                BillId = st.BillId,
                StoreId = st.StoreId,
                StoreName = st.StoreName,
                TableNumber = st.TableNumber,
                UserId = st.UserId
            });

        public IEnumerable<TempWarehouseReceipt> GetTempWarehouseReceipts()
            => data.TempWarehouseReceipts.ToList();

        public IEnumerable<TransferServiceModel> GetTransfers()
            => data.Transfers
            .Select(t => new TransferServiceModel
            {
                Id = t.Id,
                Number = t.Number,
                FromWarehouseId = t.FromWarehouseId,
                FromWarehouseName = t.FromWarehouse.Name,
                ToWarehouseId = t.ToWarehouseId,
                ToWarehouseName = t.ToWarehouse.Name,
                MaterialId = t.MaterialId,
                MaterialName = t.Material.Name,
                MeasurementName = t.Material.Measurement.Name,
                Quantity = t.Quantity,
                UserId = t.UserId
            });

        public IEnumerable<UserServiceModel> GetUsers()
            => data.Users.Select(u => new UserServiceModel
            {
                UserId = u.Id,
                UserName = u.UserName,
                FirstName = u.FirstName,
                LastName = u.LastName
            });

        public string GetUserUserName(string userId)
            => data.Users.FirstOrDefault(u => u.Id == userId).UserName;

        public IEnumerable<int> GetWarehouseMaterialIdsByWarehouseId(int warehouseId)
            => data.WarehouseMaterials
            .Where(wm => wm.WarehouseId == warehouseId)
            .Select(wm => wm.MaterialId);

        public IEnumerable<WarehouseMaterialServiceModel> GetWarehouseMaterials()
            => data.WarehouseMaterials
            .Select(wm => new WarehouseMaterialServiceModel
            {
                Id = wm.Id,
                WarehouseId = wm.WarehouseId,
                WarehouseName = wm.Warehouse.Name,
                MaterialId = wm.MaterialId,
                MaterialName = wm.Material.Name,
                MeasurementId = wm.Material.MeasurementId,
                MeasurementName = wm.Material.Measurement.Name,
                Quantity = wm.Quantity,
                Price = wm.Price
            });

        public WarehouseMaterial GetWarehouseMaterialByWarehouseIdAndMaterialId(int warehouseId, int materialId)
            => data.WarehouseMaterials
            .FirstOrDefault(wm => wm.WarehouseId == warehouseId & wm.MaterialId == materialId);

        public IEnumerable<WarehouseServiceModel> GetWarehouses()
            => data.Warehouses
            .Select(w => new WarehouseServiceModel
            {
                Id = w.Id,
                Name = w.Name
            });

        public IEnumerable<WarehouseReceipt> GetWarehouseReceipts()
            => data.WarehouseReceipts;

        public void RemoveOrderProductById(int orderProductId)
        {
            data.OrderProducts.Remove(data.OrderProducts.Find(orderProductId));

            data.SaveChanges();
        }

        public void RemoveBillFromTable(int billId)
        {
            var billOnTable = data.StoreTables.FirstOrDefault(st => st.BillId == billId);

            data.StoreTables.Remove(billOnTable);

            data.SaveChanges();
        }

        public void RemoveOrderProductsFromBill(int billId)
        {
            data.OrderProducts
            .RemoveRange(data.OrderProducts.Where(op => op.BillId == billId));

            data.SaveChanges();
        }

        public void RemoveTempWarehouseReceiptsRangeByReceiptNumber(int receiptNumber)
        {
            data.TempWarehouseReceipts
            .RemoveRange(data.TempWarehouseReceipts.Where(twr => twr.ReceiptNumber == receiptNumber));

            data.SaveChanges();
        }

        public void UpdateWarehouseMaterialQuantity(int warehouseId, int materialId, decimal quantity)
        {
            data.WarehouseMaterials
            .FirstOrDefault(wm => wm.WarehouseId == warehouseId & wm.MaterialId == materialId).Quantity = quantity;

            data.SaveChanges();
        }

        public void UpdateWarehouseMaterialPrice(int warehouseId, int materialId, decimal price)
        {
            data.WarehouseMaterials
            .FirstOrDefault(wm => wm.WarehouseId == warehouseId & wm.MaterialId == materialId).Price = price;

            data.SaveChanges();
        }
    }
}
