namespace EateryPOSSystem.Services
{
    using EateryPOSSystem.Data;
    using EateryPOSSystem.Services.Interfaces;

    public class SellerService : ISellerService
    {
        private EateryPOSDbContext data;

        public SellerService(EateryPOSDbContext data)
        {
            this.data = data;
        }

        
    }
}
