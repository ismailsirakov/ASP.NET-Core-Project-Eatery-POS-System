namespace EateryPOSSystem.Models.Storekeeper
{
    using EateryPOSSystem.Data.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class AddProviderFormModel
    {
        [Required]
        [StringLength(ProviderNameMaxLength, MinimumLength = ProviderNameMinLength)]
        public string Name { get; init; }


        public int Number { get; init; }

        public int AddressId { get; init; }

        public ICollection<Address> Addresses { get; set; }
    }
}
