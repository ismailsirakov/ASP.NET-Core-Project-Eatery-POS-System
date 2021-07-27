namespace EateryPOSSystem.Services.Interfaces
{
    using EateryPOSSystem.Models.Seller;
    using System.Collections.Generic;

    public interface ISellerService
    {
        IEnumerable<StoreViewModel> GetStores();
    }
}
