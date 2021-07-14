namespace EateryPOSSystem.Models.Storekeeper
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;
    public class AddWarehouseFormModel
    {
        [Required]
        [StringLength(WarehouseNameMaxLength, MinimumLength = WarehouseNameMinLength)]
        public string Name { get; init; }
    }
}
