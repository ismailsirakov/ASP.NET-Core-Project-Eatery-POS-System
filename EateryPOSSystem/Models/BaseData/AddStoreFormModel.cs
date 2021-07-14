namespace EateryPOSSystem.Models.Storekeeper
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;
    public class AddStoreFormModel
    {
        [Required]
        [StringLength(StoreNameMaxLength, MinimumLength = StoreNameMinLength)]
        public string Name { get; init; }
    }
}
