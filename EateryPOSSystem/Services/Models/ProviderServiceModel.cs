namespace EateryPOSSystem.Services.Models
{
    public class ProviderServiceModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public int Number { get; init; }

        public int CityId { get; set; }

        public int AddressId { get; set; }
    }
}
