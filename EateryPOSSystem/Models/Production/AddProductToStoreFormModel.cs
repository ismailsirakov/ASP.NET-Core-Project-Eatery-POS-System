namespace EateryPOSSystem.Models.Production
{
    using System.Collections.Generic;
    using EateryPOSSystem.Services.Models;

    public class AddProductToStoreFormModel
    {
        public int ProductId { get; set; }

        public int StoreId { get; set; }

        public int MeasurementId { get; set; }

        public decimal Price { get; set; }

        public decimal Quantity { get; set; }

        public IEnumerable<StoreServiceModel> Stores { get; set; }
        public IEnumerable<ProductServiceModel> Products { get; set; }
        public IEnumerable<MeasurementServiceModel> Measurements { get; set; }
    }
}
