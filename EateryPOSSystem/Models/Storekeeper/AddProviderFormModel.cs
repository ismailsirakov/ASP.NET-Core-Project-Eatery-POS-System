namespace EateryPOSSystem.Models.Storekeeper
{
    using EateryPOSSystem.Services.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class AddProviderFormModel
    {
        [Required]
        [StringLength(ProviderNameMaxLength, MinimumLength = ProviderNameMinLength)]
        public string Name { get; init; }

        [Range(0, int.MaxValue)]
        public int Number { get; init; }

        public int CityId { get; init; }

        public int AddressId { get; init; }

        public IEnumerable<AddressServiceModel> Addresses { get; set; }

        public IEnumerable<CityServiceModel> Cities { get; set; }
    }
}
