namespace EateryPOSSystem.Models.Storekeeper
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class AddAddressFormModel
    {
        [Required]
        [StringLength(AddressDetailsMaxLength, MinimumLength = AddressDetailsMinLength)]
        public string AddressDetail { get; set; }
    }
}
