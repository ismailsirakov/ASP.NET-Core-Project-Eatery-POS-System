namespace EateryPOSSystem.Services
{
    using EateryPOSSystem.Services.Interfaces;
    using EateryPOSSystem.Services.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class BillService : IBillService
    {
        private readonly IDbService dbService;

        public BillService(IDbService dbService)
        {
            this.dbService = dbService;
        }

        public IEnumerable<SoldProductServiceModel> SoldProductsByBillId(int billId)
            => dbService.GetSoldProductsByBillId(billId)
                .Select(sp => new SoldProductServiceModel
                {
                    Id = sp.Id,
                    BillId = sp.BillId,
                    StoreProductId = sp.StoreProductId,
                    StoreProductName = sp.StoreProduct.Product.Name,
                    MeasurementName = sp.Measurement.Name,
                    Quantity = sp.Quantity,
                    Price = sp.Price
                })
                .ToList();
        
    }
}
