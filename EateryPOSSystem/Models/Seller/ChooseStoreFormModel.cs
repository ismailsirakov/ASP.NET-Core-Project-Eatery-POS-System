namespace EateryPOSSystem.Models.Seller
{
    using System.Collections.Generic;


    public class ChooseStoreFormModel
    {
        public int Id { get; set; }

        public int StoreId { get; set; }

        public IEnumerable<StoreViewModel> Stores { get; set; }
    }
}
