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
        public string Name { get; set; }


        public int Number { get; set; }

        public int AddressId { get; set; }

        public ICollection<Address> Addresses { get; set; }
    }
}
