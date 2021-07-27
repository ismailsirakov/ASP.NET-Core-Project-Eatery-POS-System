namespace EateryPOSSystem.Services
{
    using EateryPOSSystem.Data;
    using EateryPOSSystem.Models.Seller;
    using EateryPOSSystem.Services.Interfaces;
    using System.Collections.Generic;
    using System.Linq;

    public class SellerService : ISellerService
    {
        private EateryPOSDbContext data;

        public SellerService(EateryPOSDbContext data)
        {
            this.data = data;
        }

        public IEnumerable<StoreViewModel> GetStores()
            => data.Stores
            .Select(s => new StoreViewModel
            {
                Id = s.Id,
                Name = s.Name,
                TablesInStore = s.TablesInStore
            })
            .ToList();
    }
}
