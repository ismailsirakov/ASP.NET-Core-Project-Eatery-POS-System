namespace EateryPOSSystem.Models.Seller
{
    using EateryPOSSystem.Services.Models;
    using System.Collections.Generic;

    public class ChooseStoreFormModel
    {
        public int Id { get; set; }

        public int StoreId { get; set; }

        public IEnumerable<StoreServiceModel> Stores { get; set; }
    }
}
