namespace EateryPOSSystem.Models.Bill
{
    using EateryPOSSystem.Services.Models;
    using System.Collections.Generic;

    public class AddSoldProductToBillFormModel
    {
        public int BillId { get; set; }

        public int StoreProductId { get; set; }

        public int OrderProductId { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int StoreId { get; set; }

        public string UserName { get; init; }

        public IEnumerable<StoreProductServiceModel> StoreProducts { get; set; }

        public IEnumerable<OrderProductServiceModel> OrderProducts { get; set; }
    }
}
